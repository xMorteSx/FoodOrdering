using Azure.Core;
using Azure.Identity;
using Microsoft.Graph.Models;
using Microsoft.Graph;
using Microsoft.TeamsFx;

namespace FoodOrdering.Services
{
    public class GraphService
    {
        private readonly IConfiguration _configuration;
        private readonly TeamsUserCredential _teamsUserCredential;
        private readonly string[] _scope = ["User.Read", "User.ReadBasic.All", "Chat.ReadWrite", "Chat.Create"];

        public GraphService(IConfiguration configuration, TeamsUserCredential teamsUserCredential)
        {
            _configuration = configuration;
            _teamsUserCredential = teamsUserCredential;
        }

        public async Task<User> GetCurrentUserProfileAsync()
        {
            if (!await HasPermission(_scope))
                throw new UnauthorizedAccessException("Insufficient permissions");

            var tokenCredential = await GetOnBehalfOfCredentialAsync();
            var graph = GetGraphServiceClient(tokenCredential);
            return await graph.Me.GetAsync();
        }

        public async Task<User> GetUserByUserMail(string userMail)
        {
            if (!await HasPermission(_scope))
                throw new UnauthorizedAccessException("Insufficient permissions");

            try
            {
                var tokenCredential = await GetOnBehalfOfCredentialAsync();
                var graph = GetGraphServiceClient(tokenCredential);
                return await graph.Users[userMail].GetAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            if (!await HasPermission(_scope))
                throw new UnauthorizedAccessException("Insufficient permissions");

            var tokenCredential = await GetOnBehalfOfCredentialAsync();
            var graph = GetGraphServiceClient(tokenCredential);
            var result = await graph.Users.GetAsync();
            return result.Value.ToList();
        }

        public async Task<bool> HasPermission(string[] scope)
        {
            try
            {
                var tokenCredential = await GetOnBehalfOfCredentialAsync();
                await tokenCredential.GetTokenAsync(new TokenRequestContext(scope), new CancellationToken());
                return true;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("AADSTS65001"))
                {
                    return false;
                }
                throw;
            }
        }

        private async Task<OnBehalfOfCredential> GetOnBehalfOfCredentialAsync()
        {
            var config = _configuration.Get<ConfigOptions>();
            var tenantId = config.TeamsFx.Authentication.OAuthAuthority.Remove(0, "https://login.microsoftonline.com/".Length);
            AccessToken ssoToken = await _teamsUserCredential.GetTokenAsync(new TokenRequestContext(null), new CancellationToken());
            return new OnBehalfOfCredential(
                tenantId,
                config.TeamsFx.Authentication.ClientId,
                config.TeamsFx.Authentication.ClientSecret,
                ssoToken.Token
            );
        }

        private GraphServiceClient GetGraphServiceClient(TokenCredential tokenCredential)
        {
            return new GraphServiceClient(tokenCredential, _scope);
        }
    }
}

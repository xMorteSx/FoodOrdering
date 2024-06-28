namespace FoodOrdering.Models.Pagination
{
    public class PaginatedList<T>(List<T> items, int pageIndex, int totalPages)
    {
        public List<T> Items { get; set; } = items;
        public int PageIndex { get; set; } = pageIndex;
        public int TotalPages { get; set; } = totalPages;
    }
}

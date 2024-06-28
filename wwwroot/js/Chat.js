function OpenChat() {
    var chat_conversation = document.querySelector(".chat_conversation")

    if (chat_conversation.style.display == "none") {
        chat_conversation.style.display = "flex";
    }
    else {
        chat_conversation.style.display = "none";
    }
}
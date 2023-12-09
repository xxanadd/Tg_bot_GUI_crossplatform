using System;

namespace Tg_bot_GUI_crossplatform.Models;

public class Chat
{
    public string? ChatName { get; set; }
    public string? ChatId { get; }
    public Boolean isChecked { get; set; }

    public Chat(string? chatName, string? chatId)
    {
        ChatName = chatName;
        ChatId = chatId;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Chat otherChat = (Chat)obj;
        return ChatId == otherChat.ChatId;
    }

    public override int GetHashCode()
    {
        return ChatId.GetHashCode();
    }
}
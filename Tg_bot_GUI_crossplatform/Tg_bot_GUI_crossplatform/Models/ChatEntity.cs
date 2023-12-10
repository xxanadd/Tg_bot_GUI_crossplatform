using SQLite;

namespace Tg_bot_GUI_crossplatform.Models;

public class ChatEntity
{
    public string ChatName { get; set; }
    [PrimaryKey]
    public string ChatId { get; set; }

    public string BotId { get; set; }
}
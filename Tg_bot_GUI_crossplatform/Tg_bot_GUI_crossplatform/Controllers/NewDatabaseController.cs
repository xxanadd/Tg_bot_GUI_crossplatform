using System;
using System.IO;
using System.Threading.Tasks;
using DynamicData.Binding;
using Tg_bot_GUI_crossplatform.Models;


namespace Tg_bot_GUI_crossplatform.Controllers;
using SQLite;

public class NewDatabaseController
{
    private readonly string _dbPath;
    public ObservableCollectionExtended<Chat> Source { get; }

    public NewDatabaseController(long botId)
    {
        Source = new ObservableCollectionExtended<Chat>();
        _dbPath = Path.Combine (
            Environment.GetFolderPath (Environment.SpecialFolder.Personal),
            "database.db3");
        var db = new SQLiteConnection(_dbPath);
        db.CreateTable<ChatEntity>();
        var query = db.Table<ChatEntity>().Where(entity => entity.BotId.Equals(botId));
        foreach (var chat in query)
        {
            Source.Add(new Chat(chat.ChatName, chat.ChatId));
        }
    }
    public async Task Add(Chat chat, long botId)
    {
        if (Source.Contains(chat)) return;
        Source.Add(chat);
        var chatEntity = new ChatEntity()
        {
            BotId = botId.ToString(),
            ChatId = chat.ChatId!,
            ChatName = chat.ChatName!
        };
        var db = new SQLiteAsyncConnection(_dbPath);
        await db.InsertAsync(chatEntity);
    }

    public async Task Remove(Chat chat, long botId)
    {
        if (Source.Contains(chat))
        {
            Source.Remove(chat);
            var chatEntity = new ChatEntity()
            {
                BotId = botId.ToString(),
                ChatId = chat.ChatId!,
                ChatName = chat.ChatName!
            };
            var db = new SQLiteAsyncConnection(_dbPath);
            await db.DeleteAsync(chatEntity);
        }
    }
}
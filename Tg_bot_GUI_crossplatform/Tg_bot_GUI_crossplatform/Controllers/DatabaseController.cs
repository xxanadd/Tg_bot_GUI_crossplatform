using System.Data.Entity;
using System.Data.SQLite;
using SQLitePCL;
using DynamicData.Binding;
using Tg_bot_GUI_crossplatform.Models;

namespace Tg_bot_GUI_crossplatform.Controllers;

public class DatabaseController
{
    public ObservableCollectionExtended<Chat> Source { get; }

    public DatabaseController(long botId)
    {
        Source = new ObservableCollectionExtended<Chat>();
        using (SQLiteConnection connection = new("DataSource=chats.db;Version=3;"))
        {
            connection.Open();
            using (SQLiteCommand command = new (connection))
            {
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS chats (chat_name VARCHAR(255), chat_id VARCHAR(255), bot_id VARCHAR(255))";
                command.ExecuteNonQuery();
            }
            using (SQLiteCommand command = new (connection))
            {
                command.CommandText = "SELECT chat_name, chat_id FROM chats WHERE bot_id = @BotId";
                command.Parameters.AddWithValue("@BotId", botId.ToString());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var chatName = reader["chat_name"].ToString();
                        var chatId = reader["chat_id"].ToString();
                        if (chatName != null && chatId != null) 
                            Source.Add(new Chat(chatName, chatId));
                    }
                }
            }
        }
    }

    public void Add(Chat chat, long botId)
    {
        if (Source.Contains(chat)) return;
        using (SQLiteConnection connection = new("DataSource=chats.db;Version=3;"))
        {
            connection.Open();
            using (SQLiteCommand command = new(connection))
            {
                command.CommandText =
                    "INSERT INTO chats (chat_name, chat_id, bot_id) VALUES(@ChatName, @ChatId, @BotId)";
                command.Parameters.AddWithValue("@ChatName", chat.ChatName);
                command.Parameters.AddWithValue("@ChatId", chat.ChatId);
                command.Parameters.AddWithValue("@BotId", botId.ToString());
                command.ExecuteNonQuery();
            }
        }
        Source.Add(chat);
    }

    public void Remove(Chat chat, long botId)
    {
        if (Source.Contains(chat))
        {
            Source.Remove(chat);
            using (SQLiteConnection connection = new("DataSource=chats.db;Version=3;"))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = "DELETE FROM chats WHERE bot_id = @BotId AND chat_id = @ChatId";
                    command.Parameters.AddWithValue("@ChatId", chat.ChatId);
                    command.Parameters.AddWithValue("@BotId", botId.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
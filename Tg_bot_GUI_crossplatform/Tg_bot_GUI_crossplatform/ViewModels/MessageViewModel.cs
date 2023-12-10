using System;
using System.Reactive;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.Controllers;

namespace Tg_bot_GUI_crossplatform.ViewModels;

public class MessageViewModel: ReactiveObject, IRoutableViewModel
{
    private readonly RoutingState _router;
    private TelegramBotController _bot;
    private string? _message;
    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }
    
    private string? _watermark;
    public string? Watermark
    {
        get => _watermark;
        set => this.RaiseAndSetIfChanged(ref _watermark, value);
    }
    
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack => _router.NavigateBack;
    private NewDatabaseController _source;
    public MessageViewModel(IScreen hostScreen, NewDatabaseController databaseController, RoutingState router,
        TelegramBotController bot)
    {
        _bot = bot;
        _router = router;
        _source = databaseController;
        HostScreen = hostScreen;
        
    }

    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }

    public void SendMessage()
    {
        if (string.IsNullOrEmpty(Message))
        {
            Watermark = "Empty message";
            return;
        }
        Watermark = "";
        foreach (var chat in _source.Source)
        {
            if (chat.isChecked)
            {
                _bot.SendMessage(chat.ChatId, Message);
            }
        }
        Message = "";
    }
}
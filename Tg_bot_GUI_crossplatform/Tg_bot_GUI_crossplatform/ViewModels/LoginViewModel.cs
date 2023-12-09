using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.Controllers;

namespace Tg_bot_GUI_crossplatform.ViewModels;

public class LoginViewModel: ReactiveObject, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    private string _token;
    public string Token
    {
        get => _token;
        set => this.RaiseAndSetIfChanged(ref _token, value);
    }

    private string _watermark;
    public string Watermark
    {
        get => _watermark;
        set => this.RaiseAndSetIfChanged(ref _watermark, value);
    }
    public IScreen HostScreen { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

    public LoginViewModel(RoutingState router, IScreen hostScreen)
    {
        _watermark = "";
        HostScreen = hostScreen;
        GoNext = ReactiveCommand.CreateFromObservable(
            () =>
            {
                try
                {
                    var telegramBotController = new TelegramBotController(_token);
                    return router.Navigate.Execute(new ChatViewModel(HostScreen, router, telegramBotController));
                }
                catch (Exception e)
                {
                    Watermark = e.Message;
                    Token = "";
                    return Observable.Return<IRoutableViewModel>(this);
                }
            }
        );
    }
}
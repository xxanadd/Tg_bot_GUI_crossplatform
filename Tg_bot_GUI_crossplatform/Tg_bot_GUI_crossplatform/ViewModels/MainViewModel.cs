using ReactiveUI;

namespace Tg_bot_GUI_crossplatform.ViewModels;

public class MainViewModel: ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new ();

    public MainViewModel()
    {
        var screen = new LoginViewModel(Router, this);
        Router.Navigate.Execute(screen);
    }
}
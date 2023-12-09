using ReactiveUI;

namespace Tg_bot_GUI_crossplatform.ViewModels;

public class MainWindowViewModel: ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new ();

    public MainWindowViewModel()
    {
        var screen = new LoginViewModel(Router, this);
        Router.Navigate.Execute(screen);
    }
}
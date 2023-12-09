using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.ViewModels;
using Tg_bot_GUI_crossplatform.Views;

namespace Tg_bot_GUI_crossplatform;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            desktop.MainWindow.DataContext = new MainWindowViewModel();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new LoginView()
            {
                DataContext = new LoginViewModel(new RoutingState(), new MainWindowViewModel())
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
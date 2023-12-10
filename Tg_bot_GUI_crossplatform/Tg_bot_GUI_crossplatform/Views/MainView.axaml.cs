using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.ViewModels;

namespace Tg_bot_GUI_crossplatform.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    private ContentControl _mainContent;
    public MainView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}
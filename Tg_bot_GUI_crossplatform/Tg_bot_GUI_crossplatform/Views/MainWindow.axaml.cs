using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.ViewModels;


namespace Tg_bot_GUI_crossplatform.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    private ContentControl _mainContent;
    
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}
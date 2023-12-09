using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.ViewModels;

namespace Tg_bot_GUI_crossplatform.Views;

public partial class MessageView : ReactiveUserControl<MessageViewModel>
{
    public MessageView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}
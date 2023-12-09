using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Tg_bot_GUI_crossplatform.ViewModels;

namespace Tg_bot_GUI_crossplatform.Views;

public partial class ChatView : ReactiveUserControl<ChatViewModel>
{
    public ChatView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
        
    }
}
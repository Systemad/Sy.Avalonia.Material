using CommunityToolkit.Mvvm.Input;
using Sy.Avalonia.Material.Themes;

namespace MaterialDemo.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    private readonly MaterialTheme _theme;

    public MainViewModel()
    {
        _theme = MaterialTheme.GetInstance();
    }

    [RelayCommand]
    public void ChangeTheme()
    {
        _theme.ChangeTheme2("ee0b5f");
    }
}

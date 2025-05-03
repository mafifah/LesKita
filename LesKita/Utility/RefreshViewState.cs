using System.ComponentModel;
using static LesKita.modApplication;
namespace LesKita;


public class RefreshViewState : INotifyPropertyChanged
{
    private bool _isRefreshing;
    private bool _isEnabled = true;
    private bool _isKeyboardAktif = false;

    public event PropertyChangedEventHandler PropertyChanged;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            if (_isRefreshing != value)
            {
                _isRefreshing = value;
                if (IsBlazorHybrid())
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing))));
                }
            }
        }
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;
                if (IsBlazorHybrid())
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled))));
                }
            }
        }
    }

    public bool IsKeyboardAktif
    {
        get => _isKeyboardAktif;
        set
        {
            if (_isKeyboardAktif != value)
            {
                _isKeyboardAktif = value;
            }
        }
    }
}
namespace LesKita;
using Microsoft.Maui.Platform;
public interface ISvcStatusBar
{
    void SetStatusBarColor(Color color, bool? darkStatusBarText);
}

public class svcStatusBar : ISvcStatusBar
{
    public void SetStatusBarColor(Color color, bool? darkStatusBarText)
    {
        #if ANDROID
                var activity = Platform.CurrentActivity;
                var window = activity.Window;
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                window.SetStatusBarColor(color.ToPlatform());

                // Set status bar text color (light or dark)
                if ((bool)darkStatusBarText)
                {
                    window.DecorView.SystemUiFlags &= ~(Android.Views.SystemUiFlags.LightStatusBar);
                }
                else
                {
                    window.DecorView.SystemUiFlags |= Android.Views.SystemUiFlags.LightStatusBar;
                }
        #elif IOS
                // iOS status bar color is managed through the UIView
                var uiWindow = UIKit.UIApplication.SharedApplication.KeyWindow;
                var statusBar = new UIKit.UIView(uiWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.BackgroundColor = color.ToPlatform();
                uiWindow.AddSubview(statusBar);
        #endif
    }
}

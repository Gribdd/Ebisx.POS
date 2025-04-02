using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Ebisx.POS.Presentation;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public override void OnUserInteraction()
    {
        base.OnUserInteraction();
        (App.Current as App).ResetIdleTimer();
    }
}

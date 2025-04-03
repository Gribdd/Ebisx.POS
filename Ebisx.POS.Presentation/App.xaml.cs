using System.Diagnostics;
using Ebisx.POS.Presentation.Services.Interface;
using Timer = System.Timers.Timer;


namespace Ebisx.POS.Presentation;

public partial class App : Application
{
    private readonly Timer _idleTimer = new Timer(60 * 5000);  //each 5 minute
    private readonly INavigationService _navigationService;

    public App(AppShell appShell,INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
        MainPage = appShell;

        _idleTimer.Elapsed += IdleTimer_Elapsed;
        Debug.WriteLine(":::Started");
        _idleTimer.Start();

    }

    

    async void IdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        Debug.WriteLine(":::Elapsed");

        if (MainThread.IsMainThread)
        {
            bool confirmLogout = await Shell.Current.DisplayAlert(
                "Session Timeout",
                "You have been idle for too long. Do you want to continue your session?",
                "Yes",
                "Logout"
            );

            if (!confirmLogout)
                await _navigationService.Logout();
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                bool confirmLogout = await Shell.Current.DisplayAlert(
                    "Session Timeout",
                    "You have been idle for too long. Do you want to continue your session?",
                    "Yes",
                    "Logout"
                );

                if (!confirmLogout)
                    await _navigationService.Logout();
            });
        }
    }

    public void ResetIdleTimer()
    {
        _idleTimer.Stop();
        _idleTimer.Start();
        Debug.WriteLine(":::Reset");    
    }
}   
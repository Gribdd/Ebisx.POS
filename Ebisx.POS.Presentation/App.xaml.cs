using System.Diagnostics;
using Ebisx.POS.Presentation.Services.Interface;
using Timer = System.Timers.Timer;


namespace Ebisx.POS.Presentation;

public partial class App : Application
{
    private readonly Timer _idleTimer = new Timer(60 * 1000);  //each 1 minute
    private readonly INavigationService _navigationService;
    private int _elapsedSeconds = 0;  // Tracks elapsed time
    public App(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();


        _idleTimer.Elapsed += IdleTimer_Elapsed;
        Debug.WriteLine(":::Started");
        _idleTimer.Start();

    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    async void IdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        _elapsedSeconds += 60; // Increment elapsed time
        Debug.WriteLine($"::: Elapsed time: {_elapsedSeconds}(s)");
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
        _elapsedSeconds = 0; // Reset elapsed time when user interacts
        _idleTimer.Stop();
        _idleTimer.Start();
        Debug.WriteLine(":::Reset");    
    }
}
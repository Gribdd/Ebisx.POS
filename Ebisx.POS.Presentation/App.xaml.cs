using System.Diagnostics;
using Timer = System.Timers.Timer;


namespace Ebisx.POS.Presentation;

public partial class App : Application
{
    private readonly Timer _idleTimer = new Timer(60 * 1000);  //each 1 minute
    private int _elapsedSeconds = 0;  // Tracks elapsed time
    public App()
    {
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
        _elapsedSeconds+=60 ; // Increment elapsed time
        Debug.WriteLine($"::: Elapsed time: {_elapsedSeconds}(s)");
        Debug.WriteLine(":::Elapsed");
        if (MainThread.IsMainThread)
            await Shell.Current.GoToAsync("///mainpage");
        else
            MainThread.BeginInvokeOnMainThread(async () => 
                await Shell.Current.GoToAsync("///mainpage"));
    }

    public void ResetIdleTimer()
    {
        _elapsedSeconds = 0; // Reset elapsed time when user interacts
        _idleTimer.Stop();
        _idleTimer.Start();
        Debug.WriteLine(":::Reset");    
    }
}
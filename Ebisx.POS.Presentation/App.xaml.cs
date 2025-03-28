using Ebisx.POS.Presentation.Services.Interface;
using Ebisx.POS.Presentation.Views.Manager;

namespace Ebisx.POS.Presentation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
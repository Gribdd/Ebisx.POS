namespace Ebisx.POS.Presentation
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;
        public MainPage(MainPageViewModel _vm)
        {
            InitializeComponent();
            BindingContext = _vm;
            vm = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadAccounts();
        }
    }

}

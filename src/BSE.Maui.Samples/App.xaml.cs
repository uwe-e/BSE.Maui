namespace BSE.Maui.Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BSE.Maui.Samples.Pages.TabbedPage.MainPage();
        }
    }
}

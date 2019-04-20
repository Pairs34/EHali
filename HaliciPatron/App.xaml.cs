using Xamarin.Forms;

namespace HaliciPatron
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new Startup();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
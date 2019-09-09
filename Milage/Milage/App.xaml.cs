using Milage.Data;
using Milage.Pages;
using System;
using System.IO;
using Xamarin.Forms;

namespace Milage
{
    public partial class App : Application
    {

        static TripsDatabase db;

        public static TripsDatabase Database
        {
            get
            {
                if (db == null)
                {
                    db = new TripsDatabase(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TripsSQLite.db3"));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TripsPage())
            {
                BarTextColor = Color.FromHex("#ffffff"),
                BarBackgroundColor = Color.FromHex("#2962ff"),
                IconImageSource = "@drawable/motowheel.png"
            };
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

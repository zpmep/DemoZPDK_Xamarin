using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DemoZPDK_Xamarin.Services;
using DemoZPDK_Xamarin.Views;
using DemoZPDK_Xamarin.Data;

namespace DemoZPDK_Xamarin
{
    public partial class App : Application
    {
        static OrderDatabase database;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<ZaloPayService>();


            MainPage = new NavigationPage(new ListOrderPage());
            //MainPage = new MainPage();
        }

        public static OrderDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new OrderDatabase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

}

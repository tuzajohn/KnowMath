using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KnowMath.Services;
using KnowMath.Views;
using KnowMath.Helpers;
using Xamarin.Essentials;

namespace KnowMath
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            if (!string.IsNullOrEmpty(SharedStorageHelper.Get("location")))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync(SharedStorageHelper.Get("location"));
                });
            }
        }
    }
}

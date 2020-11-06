using System;
using KnowMath.Helpers;
using KnowMath.Views;
using Xamarin.Forms;

namespace KnowMath
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(LevelPage), typeof(LevelPage));
            Routing.RegisterRoute(nameof(LevelPage) + "/" + nameof(ChallengePage), typeof(ChallengePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//AccountPage");
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            if (args.Source == ShellNavigationSource.Push)
            {
                if (Current.CurrentState.Location != null)
                {
                    var location = Current.CurrentState.Location.ToString();
                    SharedStorageHelper.Store("location", location);
                }
            }
            base.OnNavigated(args);
        }
    }
}

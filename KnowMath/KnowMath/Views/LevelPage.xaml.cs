using Acr.UserDialogs;
using KnowMath.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KnowMath.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelPage : ContentPage
    {
        public LevelPage()
        {
            InitializeComponent();
        }

        private async void StartGameBtton_Clicked(object sender, EventArgs e)
        {

            var level = LevelSelection.SelectedItem.ToString();
            var arithmetic = ArithmeticSelection.SelectedItem.ToString();


            SharedStorageHelper.Store("level", level.ToLower());
            SharedStorageHelper.Store("arithmetic", arithmetic.ToLower());

            Dispatcher.BeginInvokeOnMainThread(() => { UserDialogs.Instance.ShowLoading("Getting things set...", MaskType.Clear); });
            await Shell.Current.GoToAsync($"{nameof(LevelPage)}/{nameof(ChallengePage)}");
        }
    }
}
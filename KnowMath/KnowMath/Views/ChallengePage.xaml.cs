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
    public partial class ChallengePage : ContentPage
    {
        public ChallengePage()
        {
            InitializeComponent();
            Dispatcher.BeginInvokeOnMainThread(() => { UserDialogs.Instance.HideLoading(); });

            var t = SharedStorageHelper.Get("level");

            SharedStorageHelper.Store("current_score", "0");
        }

        protected override bool OnBackButtonPressed()
        {
            bool backStatus = true;
            var location = Shell.Current.CurrentState.Location.ToString();
            if (location.Contains(nameof(ChallengePage)))
            {
                if (!string.IsNullOrEmpty(SharedStorageHelper.Get("current_score")))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var response = await Application.Current.MainPage.DisplayAlert("Exit", "You wish to quit? Your current progress shall not be saved", "Ok", "Cancel");
                        if (response)
                        {
                            SharedStorageHelper.Remove("current_score");
                            backStatus = true;
                            await Shell.Current.GoToAsync("..");
                        }
                        else
                        {
                            backStatus = false;
                        }
                        
                    });
                }
                else { backStatus = true; }
            }
            return backStatus;
        }

    }
}
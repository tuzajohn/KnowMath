using KnowMath.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KnowMath.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string selectedOptions;

        public string SelectedOptions
        {
            get => selectedOptions;
            set => SetProperty(ref selectedOptions, value);
        }

        public Command ToLevelPageCommad { get; }
        public Command ToScorePageCommand { get; }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(selectedOptions);
        }

        public HomeViewModel()
        {
            ToLevelPageCommad = new Command(SelectLevelAsync, ValidateSave);
        }

        private async void SelectLevelAsync()
        {
            

        }


    }
}

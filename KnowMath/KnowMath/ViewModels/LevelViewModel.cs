using KnowMath.Helpers;
using KnowMath.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KnowMath.ViewModels
{
    public class LevelViewModel : BaseViewModel
    {
        private string selectedLevel;
        private string selectedArithmetic;
        private List<string> levels;
        private List<string> arithmetics;

        public string SelectedLevel
        {
            get { return selectedLevel; }
            set { SetProperty(ref selectedLevel, value); }
        }

        public string SelectedArithmetic
        {
            get { return selectedArithmetic; }
            set { SetProperty(ref selectedArithmetic, value); }
        }

        public List<string> Levels
        {
            get { return levels; }
            set { SetProperty(ref levels, value); }
        }

        public List<string> Arithmetics
        {
            get { return arithmetics; }
            set { SetProperty(ref arithmetics, value); }
        }

        public Command startGameCommand { get; set; }
        public LevelViewModel()
        {
            Levels = KernelHelper.Level;

            Arithmetics = KernelHelper.Arithmetic
                .Select(x => x.Key)
                .ToList();

            startGameCommand = new Command(GameStart);

        }

        private async void GameStart()
        {
            if (string.IsNullOrEmpty(selectedLevel) || string.IsNullOrEmpty(selectedArithmetic))
            {
                string message = string.Empty;

                if (string.IsNullOrEmpty(selectedLevel))
                    message += "a level";

                if (string.IsNullOrEmpty(selectedArithmetic))
                    message += $"{(string.IsNullOrEmpty(selectedLevel) ? " and " : "")} an arithmetic option";

                await Application.Current.MainPage.DisplayAlert("Alert", $"Please select {message}", "OK");

                return;
            }

            var _operator = KernelHelper.GetOperator(selectedArithmetic);


            SharedStorageHelper.Store("level", selectedLevel.ToLower());
            SharedStorageHelper.Store("arithmetic", selectedArithmetic.ToLower());

            //Dispatcher.BeginInvokeOnMainThread(() => { UserDialogs.Instance.ShowLoading("Getting things set...", MaskType.Clear); });
            await Shell.Current.GoToAsync($"{nameof(LevelPage)}/{nameof(ChallengePage)}?Operator={_operator}");
        }
    }
}

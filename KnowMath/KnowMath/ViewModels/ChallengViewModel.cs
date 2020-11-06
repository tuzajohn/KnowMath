using KnowMath.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KnowMath.ViewModels
{
    [QueryProperty("Operator", "Operator")]
    public class ChallengViewModel : BaseViewModel
    {
        private string text;
        private string calculation;
        private string _operator;
        private string leftNumer;
        private string rightNumer;
        private string score;
        private string time;
        private Color answerColor;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Calculation
        {
            get => calculation;
            set => SetProperty(ref calculation, value);
        }

        public string Operator
        {
            get => _operator;
            set => SetProperty(ref _operator, Uri.UnescapeDataString(value));
        }

        public string LeftNumer
        {
            get => leftNumer;
            set => SetProperty(ref leftNumer, value);
        }

        public string RightNumer
        {
            get => rightNumer;
            set => SetProperty(ref rightNumer, value);
        }

        public string Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }

        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public Color AnswerColor
        {
            get => answerColor;
            set => SetProperty(ref answerColor, value);
        }

        public Command AppendNumberCommand { get; }
        public Command DeleteNumberCommand { get; }
        public Command AnswerCommand { get; }
        public Command ClearNumberCommand { get; }


        public ChallengViewModel()
        {
            AppendNumberCommand = new Command<string>(Append);
            DeleteNumberCommand = new Command(Delete);
            ClearNumberCommand = new Command(Clear);
            AnswerCommand = new Command(Answer);
            AnswerColor = Color.Black;
            Score = "0";
            RightNumer = "0";

            var t = Operator;

            switch (SharedStorageHelper.Get("arithmetic"))
            {
                case "addition": Operator = "+"; break;
                case "multplication": Operator = "*"; break;
                case "subtraction": Operator = "-"; break;
                case "divistion": Operator = "/"; break;
                default: break;
            }

            LeftNumer = "0";
            SetNumbers();
            Calculation = $"{LeftNumer} {Operator} {RightNumer}";
        }

        private void Append(string number)
        {
            AnswerColor = Color.Black;

            if (string.IsNullOrEmpty(number))
                return;

            Text += number;
        }

        private void Delete()
        {
            if (Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }
        }

        private void Clear()
        {
            Text = string.Empty;
        }

        private async void Answer()
        {
            if (!int.TryParse(Text, out int answer))
                return;

            var _left = int.Parse(LeftNumer);
            var _right = int.Parse(RightNumer);


            int systemAnswer;
            switch (Operator)
            {
                case "+": systemAnswer = _left + _right; break;
                case "*": systemAnswer = _left * _right; break;
                case "-":systemAnswer = _left - _right; break;
                case "/": systemAnswer = _left / _right; break;
                default: systemAnswer = 0; break;
            }

            if (systemAnswer == answer)
            {
                Score = (int.Parse(Score) + 5).ToString();
                AnswerColor = Color.Green;

                await Task.Delay(2000);

                Text = string.Empty;
                AnswerColor = Color.Black;
                SetNumbers();
            }
            else
            {
                AnswerColor = Color.Red;
            }

        }

        private void SetNumbers()
        {
            var level = SharedStorageHelper.Get("level");

            var random = new Random();
            switch (level)
            {
                case "beginer": LeftNumer = random.Next(0,99).ToString(); RightNumer = random.Next(0,99).ToString();
                    SharedStorageHelper.Store("time", "10"); break;

                case "intermidiate": LeftNumer = random.Next(100, 999).ToString(); RightNumer = random.Next(0, 99).ToString();
                    SharedStorageHelper.Store("time", "15"); break;

                case "advanced": LeftNumer = random.Next(1000, 9999).ToString(); RightNumer = random.Next(0, 99).ToString();
                    SharedStorageHelper.Store("time", "20"); break;

                default: LeftNumer = random.Next(0, 9).ToString(); RightNumer = random.Next(0, 99).ToString();
                    SharedStorageHelper.Store("time", "10"); break;
            }

            Time = SharedStorageHelper.Get("time");

            Calculation = $"{LeftNumer} {Operator} {RightNumer}";
            TimerFunction();
        }

        private async Task TimerAsync(int interval, CancellationToken token)
        {
            var time = int.Parse(Time);

            if (time == default)
                return;

            while (token.IsCancellationRequested)
            {
                Time = (time--).ToString();
                await Task.Delay(interval, token);
            }
        }

        private async void TimerFunction()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(int.Parse(SharedStorageHelper.Get("time")) * 1000);
            await TimerAsync(1000, cts.Token);
        }
    }
}

using BatoBatoPick.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia.Controls;

namespace BatoBatoPick.ViewModels
{
    public partial class Game1ViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<MoveViewModel> myMoves = new();

        private readonly MainWindowViewModel _mainWindowViewModel;
        public Game1ViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            InitializeMoves();
        }

        private MoveViewModel _moveViewModel;
        public Game1ViewModel(MoveViewModel moveViewVodel){ _moveViewModel = moveViewVodel; InitializeMoves(); }

        private GameOverViewModel _gameOverViewModel;
        public Game1ViewModel(GameOverViewModel gameOverViewModel) { _gameOverViewModel = gameOverViewModel; InitializeMoves(); }

       
        public Game1ViewModel() { InitializeMoves(); }

        private void InitializeMoves()
        {
            MyMoves = new ObservableCollection<MoveViewModel>
            {
                new MoveViewModel(this) { MyMove = "Rock" },
                new MoveViewModel(this) { MyMove = "Paper" },
                new MoveViewModel(this) { MyMove = "Scissor" },
            };
            ImageUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Questionmark.png"));
            ImageCompUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Questionmark_Inverted.png"));

        }





        [ObservableProperty]
        private int compMove;

        [ObservableProperty]
        private int playerMove;

        [ObservableProperty]
        private int playerScore;

        [ObservableProperty]
        private int compScore;

        [ObservableProperty]
        private string borderBG = "CornflowerBlue";
        [ObservableProperty]
        private string borderBG2 = "#c72c2c";

        [ObservableProperty]
        public string btnBG = "CornflowerBlue";

        [ObservableProperty]
        private string btnBG2 = "#c72c2c";

        [ObservableProperty]
        private bool isEnabled2 = true;

        [ObservableProperty]
        private bool isEnabled = true;

        [ObservableProperty]
        private Bitmap imageUri;

        [ObservableProperty]
        private Bitmap imageCompUri;

        [ObservableProperty]
        private Geometry currentIcon = (Geometry)App.Current.FindResource("pauseregular");





        public async void Start() { RandomMove();  ImageChange();  CompImageChange(); }

        [ObservableProperty]
        private bool executed = false;

        public int RandomMove()
        {
            Random random = new Random();
            CompMove = random.Next(1, 4);

            return CompMove;
        }

        public async Task ImageChange()
        {
            ImageUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Questionmark.png"));
            await Task.Delay(1200);

            switch (PlayerMove)
            {
                case 1: ImageUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Rock.png")); break;
                case 2: ImageUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Paper.png")); break;
                case 3: ImageUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Scissors.png")); break;
                default: break;
            }
            Calculation();
        }

        public async Task CompImageChange()
        {
            ImageCompUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Questionmark_Inverted.png"));
            await Task.Delay(1100);
            switch (CompMove)
            {
                case 1: ImageCompUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Rock.png")); break;
                case 2: ImageCompUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Paper.png")); break;
                case 3: ImageCompUri = ImageHelper.LoadFromResource(new Uri("avares://BatoBatoPick/Assets/Scissors.png")); break;
                default: break;
            }
        }

        public void Calculation()
        {
            //Instead a long of if-else, use this
            var outcomes = new Dictionary<(int playerMove, int compMove), Action>
                {
                  {(1, 1), () => { /* Tie */ }},
                  {(1, 2), () => { CompScore++; }},
                  {(1, 3), () => { PlayerScore++; }},
                  {(2, 1), () => { PlayerScore++; }},
                  {(2, 2), () => { /* Tie */ }},
                  {(2, 3), () => { CompScore++; }},
                  {(3, 1), () => { CompScore++; }},
                  {(3, 2), () => { PlayerScore++; }},
                  {(3, 3), () => { /* Tie */ }},
                };

            if (outcomes.TryGetValue((PlayerMove, CompMove), out var action))
            {
                action.Invoke();
            }

            var  ToCall_GameOverMethod = new GameOverViewModel(this);
             ToCall_GameOverMethod.GameOver();
        }

        private void ChangeBorders() { if (!IsEnabled) { BorderBG = "Gray"; BorderBG2 = "Gray"; } else { BorderBG = "CornflowerBlue"; BorderBG2 = "#c72c2c"; } }
        private void ChangePauseIcon() { }
        [RelayCommand]
        public void ResetCommand() { PlayerScore = 0; CompScore = 0; }
        
        [RelayCommand]
        public void PauseCommand() { IsEnabled2 = !IsEnabled2; IsEnabled = !IsEnabled; foreach (var move in MyMoves)
            {
                move.PauseVals = IsEnabled;
            }
            CurrentIcon = IsEnabled ? (Geometry)App.Current.FindResource("pauseregular") : (Geometry)App.Current.FindResource("playregular");

            ChangeBorders();
        }


        [RelayCommand]
        public void ContinueToGameCommand()
        {
            _mainWindowViewModel.CurrentPage = new ContinueToGameViewModel(this._mainWindowViewModel);
        }

      

    }
}

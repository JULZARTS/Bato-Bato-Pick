using BatoBatoPick.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatoBatoPick.ViewModels
{
    public partial class MoveViewModel: ViewModelBase
    {

        public MoveViewModel() { PauseVals = IsEnabled; }

        private Game1ViewModel _game1ViewModel;
        public MoveViewModel(Game1ViewModel game1ViewModel) {  _game1ViewModel = game1ViewModel; }

        public MoveViewModel(Move move){ MyMove = move.MyMove; }

        [ObservableProperty]
        private string? myMove;

        public string BtnBG => _game1ViewModel.BtnBG; 
        public bool IsEnabled => _game1ViewModel.IsEnabled;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExecuteMoveCommand))]
        private bool pauseVals = true;

        [RelayCommand (CanExecute = nameof(PauseVals))]
        public async void ExecuteMove() 
        {
            // Update the PlayerMove property in MainWindowViewModel
            _game1ViewModel.PlayerMove = MyMove switch
            {
                "Rock" => 1, 
                "Paper" => 2,
                "Scissor" => 3,
                _ => _game1ViewModel.PlayerMove
            };
            _game1ViewModel.Start();
        }
    }
}

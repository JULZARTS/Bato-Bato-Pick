using Avalonia.Controls;
using HarfBuzzSharp;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using System.Drawing;
using System.Threading.Tasks;

namespace BatoBatoPick.ViewModels
{
    public partial class GameOverViewModel
    {
        private Game1ViewModel _game1ViewModel;

        public GameOverViewModel(Game1ViewModel game1ViewModel) { _game1ViewModel = game1ViewModel; }

        private int Playerscore => _game1ViewModel.PlayerScore;
        private int CompScore => _game1ViewModel.CompScore;
        public async Task GameOver()
        {
            if (Playerscore >= 5 || CompScore >= 5) 
            {
                _game1ViewModel.PauseCommand();
                await Task.Delay(800);

                var box = MessageBoxManager.GetMessageBoxStandard(Winner() + " wins!", "play again?", ButtonEnum.YesNo);

                var result = await box.ShowAsync();
                if(result == ButtonResult.Yes) { _game1ViewModel.ResetCommand(); _game1ViewModel.PauseCommand(); } else { _game1ViewModel.ContinueToGameCommand(); }
            }
        }

        private string Winner() { if (Playerscore > CompScore) { return "Player"; } else { return "Computer"; } }
    }
}

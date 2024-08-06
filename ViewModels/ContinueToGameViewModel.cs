using CommunityToolkit.Mvvm.Input;

namespace BatoBatoPick.ViewModels
{
    public partial class ContinueToGameViewModel : ViewModelBase
    {
        public readonly MainWindowViewModel _mainWindowViewModel;

        public ContinueToGameViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        [RelayCommand]
        public void ContinueToGameCommand()
        {
            _mainWindowViewModel.CurrentPage = new Game1ViewModel(this._mainWindowViewModel);
        }

    }
}

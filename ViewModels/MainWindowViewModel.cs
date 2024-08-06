using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BatoBatoPick.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            CurrentPage = new ContinueToGameViewModel(this);
        }

        [ObservableProperty]
        public ViewModelBase currentPage;

        

    }
}

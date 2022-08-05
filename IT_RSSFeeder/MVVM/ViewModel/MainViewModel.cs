using IT_RSSFeeder.Core;
using System;

namespace IT_RSSFeeder.MVVM.ViewModel
{
    internal sealed class MainViewModel: ObservableObject
    {
        public static event Action ViewChanged;
        private ViewModel _homeViewModel { get; set; }
        private ViewModel _settingsViewModel { get; set; }
        private ViewModel _startViewModel { get; set; }
        public RelayCommand HomeViewCommand { get; private set; }
        public RelayCommand SettingsViewCommand { get; private set; }
        public RelayCommand StartViewCommand { get; private set; }
        private ViewModel _currentView;
        public ViewModel CurrentView
        {
            get { return  _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _settingsViewModel = new SettingsViewModel();
            _startViewModel = new StartViewModel();
            CurrentView = _startViewModel;

            HomeViewCommand = new RelayCommand(vm =>
            {
                _homeViewModel = new HomeViewModel();
                CurrentView = _homeViewModel;
                ViewChanged?.Invoke();
            });
            SettingsViewCommand = new RelayCommand(vm =>
            {
                CurrentView = _settingsViewModel;
                ViewChanged?.Invoke();
            });
            StartViewCommand = new RelayCommand(vm =>
            {
                CurrentView = _startViewModel;
                ViewChanged?.Invoke();
            });
        }
    }
}

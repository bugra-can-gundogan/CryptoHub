using CryptoHub.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoHub.MVVM.ViewModel
{
    class MainViewModel:ObservableObject
    {

        public RelayCommand WalletViewCommand { get; set; }

        public RelayCommand MarketViewCommand { get; set; }

        public RelayCommand WatchListViewCommand { get; set; }

        public RelayCommand CryptoBotViewCommand { get; set; } 

        public WalletViewModel WalletVm { get; set; }
        public MarketViewModel MarketVm { get; set; }
        public CryptoBotViewModel CryptoBotVm { get; set; }
        public WatchListViewModel WatchListVm { get; set; }

        private object _currentView;
        
        public object CurrentView
        {
            get { return _currentView; }    
            set 
            {   
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {

            WalletVm = new WalletViewModel();
            MarketVm = new MarketViewModel();
            WatchListVm = new WatchListViewModel();
            CryptoBotVm = new CryptoBotViewModel();
            CurrentView = MarketVm;

            WalletViewCommand = new RelayCommand(o =>
            {
                CurrentView = WalletVm;
            });

            MarketViewCommand = new RelayCommand(o =>
            {
                CurrentView = MarketVm;
            });

            WatchListViewCommand = new RelayCommand(o => { 
                CurrentView = WatchListVm;
            });

            CryptoBotViewCommand = new RelayCommand(o => {
                CurrentView = CryptoBotVm;
            });
        }
    }
}

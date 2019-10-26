using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Refit;
using RefitDemo.API;
using RefitDemo.Models;
using RefitDemo.Services;
using Xamarin.Forms;

namespace RefitDemo.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private List<Coin> _Coins;

        public List<Coin> Coins
        {
            get { return _Coins; }

            set { SetProperty(ref _Coins, value); }
        }

        private string _Search;

        public string Search
        {
            get { return _Search; }

            set { SetProperty(ref _Search, value); }
        }

        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }




        public ICommand LoadCoinsCommand { get; set; }

        private readonly ICoinsRepository coinsRepository;

        public MainPageViewModel()
        {
            var httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri("https://api.coingecko.com") };
            coinsRepository = RestService.For<ICoinsRepository>(httpClient);

            Coins = new List<Coin>();

            LoadCoinsCommand = new Command(async () => await LoadCoins());

            IsBusy = false;
        }

        async Task LoadCoins()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    if (string.IsNullOrEmpty(Search))
                    {
                        Coins = await coinsRepository.GetCoins();
                    }
                    else
                    {
                        var coin = await coinsRepository.GetCoin(Search);
                        Coins = new List<Coin> { coin };
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        #region "INotifChanged"

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
        #endregion

    }
}

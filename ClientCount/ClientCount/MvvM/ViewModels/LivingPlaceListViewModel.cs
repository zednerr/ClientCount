using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    public class LivingPlaceListViewModel:ViewModelBase
    {
        private List<LivingPlace> livingPlaces = new List<LivingPlace>();
        private readonly Client CurrentClient;
        public List<LivingPlace> LivingPlaces
        {
            set
            {
                if (livingPlaces.Count != value.Count)
                {
                    livingPlaces = value;
                    OnPropertyChanged("LivingPlaces");
                }

            }
            get
            {
                return livingPlaces;
            }
        }
        private LivingPlace _selectedItem;
        public LivingPlace SelectedItem
        {
            set
            {
                _selectedItem = value;
                if (_selectedItem == null)
                    return;

               navigatetolivingplacedetails(_selectedItem);

                _selectedItem = null;
                OnPropertyChanged("SelectedItem");
            }
            get
            {
                return _selectedItem;
            }
        }
        public void navigatetolivingplacedetails(LivingPlace lPlace)
        {
            App.Navigation.PushAsync(new NavigationPage(new LivingPlaceDetailPage(lPlace)));
        }

        public ICommand AddLivingPlaceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Navigation.PushAsync(new NavigationPage(new AddLivingPlacePage(CurrentClient)));
                });
            }
        }

        public LivingPlaceListViewModel(Client client)
        {
            CurrentClient = client;
            var livingService = new LivingPlaceService();
            livingPlaces = livingService.ReadById(CurrentClient.Id);
        }




    }
}

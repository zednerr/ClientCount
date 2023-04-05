using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    public class ClientsListViewModel : ViewModelBase
    {
        private List<Client> clients = new List<Client>();
        public List<Client> Clients
        {
            set
            {
                if (clients.Count != value.Count)
                {
                    clients = value;
                    OnPropertyChanged("Clients");
                }

            }
            get
            {
                return clients;
            }
        }
        private Client _selectedItem;
        public Client SelectedItem
        {
            set
            {
                _selectedItem = value;
                if (_selectedItem == null)
                    return;
                NavigateToClientDetails(_selectedItem);
                 
                _selectedItem = null;
                OnPropertyChanged("SelectedItem");
            }
            get
            {
                return _selectedItem;
            }
        }
     
        public ICommand AddClientCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    await App.Navigation.PushAsync(new NavigationPage(new AddClientPage()));
                });
            }
        }
        public ICommand ActionsViewCommand
        {
            get
            {
                return new Command(async () =>
                {
                   
                    await App.Navigation.PushAsync(new NavigationPage(new ActionViewPage()));
                });
            }
        }
        public ICommand EmployeeListCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await App.Navigation.PushAsync(new NavigationPage(new ListEmployeePage()));
                });
            }
        }
        public ICommand ReportCommand
        {
            get
            {
                return new Command(() =>
                {
                   App.Navigation.ShowPopup(new ReportDetailPage());
                });
            }
        }

        public ICommand ListClientCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Navigation.PopToRootAsync();
                });
            }
        }
        public  void NavigateToClientDetails(Client client)
        {
            App.Navigation.PushAsync(new NavigationPage(new ClientDetailPage(client)));
        }

        public ClientsListViewModel()
        {
            var clientService = new ClientService();
            Clients = clientService.ReadAllClientsforview();
        }

    }
}

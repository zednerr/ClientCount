using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using DocumentFormat.OpenXml.Office2016.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
namespace ClientCount.MvvM.ViewModels
{
    public class ClientDetailViewModel : ViewModelBase
    {
        private readonly ClientService clientService;
        private readonly Client CurrentClient;
        public ClientDetailViewModel(Client currentClient)
        {
            clientService = new ClientService();
            var employeeService = new EmployeeService();
            CurrentClient = clientService.Get(currentClient.Id);
            var clientemployee = employeeService.ReadById(CurrentClient.Employee_id);
            employee = clientemployee.LastName;
            FirstName = CurrentClient.FirstName;
            LastName = CurrentClient.LastName;
            Patronymic = CurrentClient.Patronymic;
            PhoneNumber = CurrentClient.PhoneNumber;
            HphoneNumber = CurrentClient.HphoneNumber;
        }
        private string firstName;
        private string lastName;
        private string patronymic;
        private string phoneNumber;
        private string hphoneNumber;
        private string employee;
        public string Employee
        {
            set
            {
                if (employee != value)
                {
                    employee = value;
                    OnPropertyChanged("Employee");
                }
            }
            get
            {
                return employee;
            }
        }
        public string FirstName
        {
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
            get
            {
                return firstName;
            }
        }
        public string LastName
        {
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
            get
            {
                return lastName;
            }
        }
        public string Patronymic
        {
            set
            {
                if (patronymic != value)
                {
                    patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
            get
            {
                return patronymic;
            }
        }
        public string PhoneNumber
        {
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
            get
            {
                return phoneNumber;
            }
        }
        public string HphoneNumber
        {
            set
            {
                if (hphoneNumber != value)
                {
                    hphoneNumber = value;
                    OnPropertyChanged("HphoneNumber");
                }
            }
            get
            {
                return hphoneNumber;
            }
        }

        private string message;
        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
            get { return message; }


        }
        public ICommand UpdateClientCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        CurrentClient.FirstName = firstName;
                        CurrentClient.LastName = lastName;
                        CurrentClient.Patronymic = patronymic;
                        CurrentClient.PhoneNumber = phoneNumber;
                        CurrentClient.HphoneNumber = hphoneNumber;

                        int result = clientService.UpdateClient(CurrentClient);

                        if (result > 0)
                        {
                            var option = new ToastView("Data updated successfully!");
                            MessagingCenter.Send<string>("ClientUpdate", "Updated");
                            await Application.Current.MainPage.DisplayToastAsync(option.ToastOptions());
                        }
                      

                    }
                    catch (NullReferenceException) 
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                    catch (SQLite.NotNullConstraintViolationException)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                });

            }
        }
        public ICommand LivingPlaceListCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Navigation.PushAsync(new NavigationPage(new LivingPlaceListPage(CurrentClient)));
                });
            }
        }
        public ICommand DeleteClientCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                   bool accepted = await Application.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete the client?","Yes","No");
                    if (accepted)
                    {
                        LivingPlaceService lpservice = new LivingPlaceService();
                        ActionService actionService = new ActionService();
                        var conn = App.DataBase.Connection;
                        var idexses = conn.Query<LivingPlace>($"SELECT Id FROM LivingPlace WHERE Client_id = {CurrentClient.Id}");
                        foreach (var i in idexses)
                        {
                            actionService.DeleteActionsById(i.Id);
                        }
                        lpservice.DeleteById(CurrentClient.Id);
                        int result = clientService.DeleteClient(CurrentClient);
                        if (result > 0)
                        {
                            MessagingCenter.Send("Delete", "Update", "Success");
                            MessagingCenter.Send("Delete", "UpdateListView", "Success");
                            await App.Navigation.PopToRootAsync();
                        }
                    }

                });
            }
        }
    }
}

using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    public class AddClientViewModel : ViewModelBase
    {
        private readonly ClientService clientService;
        public AddClientViewModel()
        {
            clientService = new ClientService();
             EmployeeService employeeService = new EmployeeService();
            Employees = employeeService.ReadAll();
        }
        private Employee _selectedemployee;
        public Employee SelectedEmployee
        {
            set
            {
                _selectedemployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
            get
            {
                return _selectedemployee;

            }
        }
        public List<Employee> Employees
        {
            get; set;
        }
        private string firstName;
        private string lastName;
        private string patronymic;
        private string phoneNumber;
        private string hphoneNumber;
        public bool CanFormData(params string[]n)
        {
           for (int i =0; i < n.Length; i++)
            {
                if (string.IsNullOrEmpty(n[i]))
                {
                    return true;
                }
            }
           return false;
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
        public ICommand AddClientCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (CanFormData(new string[] {firstName,lastName,phoneNumber, SelectedEmployee.Id.ToString() })==true)
                        {
                            throw new NullReferenceException();
                        }
                        int result = clientService.CreateClient(new Client
                        {
                            
                            FirstName = firstName,
                            LastName = lastName,
                            Patronymic = patronymic,
                            PhoneNumber = phoneNumber,
                            HphoneNumber = hphoneNumber,
                            Employee_id = SelectedEmployee.Id

                        });

                        if (result > 0)
                        {
                            var option = new ToastView("Client successfully created!");
                            MessagingCenter.Send("AddClient", "UpdateListView", "Success");
                            await App.Navigation.PopAsync();
                            await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
                        }
                    }
                    catch (NullReferenceException )
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");

                    }
                    catch (SQLite.NotNullConstraintViolationException )
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                });

            }
        }
    }
}

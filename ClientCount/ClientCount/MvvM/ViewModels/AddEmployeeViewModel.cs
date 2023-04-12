using ClientCount.Models;
using ClientCount.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly EmployeeService employeeService;
        public AddEmployeeViewModel()
        {
            employeeService = new EmployeeService();
        }
        private string firstName;
        private string lastName;
        private string patronymic;
        private string phoneNumber;
        public bool CanFormData(params string[] n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                if (string.IsNullOrEmpty(n[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public string FirstName {
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
            get { return firstName; }
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
        public ICommand AddEmployeeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (CanFormData(new string[] { firstName, lastName, phoneNumber}) == true)
                        {
                            throw new NullReferenceException();
                        }
                        int result = employeeService.AddEmployee(new Employee
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Patronymic = patronymic,
                            PhoneNumber = phoneNumber
                        });
                        if (result > 0)
                        {
                            MessagingCenter.Send("AddEmployee", "ListView", "Success");
                            await App.Navigation.PopAsync();
                            var option = new ToastView("Employee added!");
                            await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
                        }
                    }
                    catch (NullReferenceException)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");

                    }
                    catch (SQLite.NotNullConstraintViolationException)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                });

            }
        }

    }
}

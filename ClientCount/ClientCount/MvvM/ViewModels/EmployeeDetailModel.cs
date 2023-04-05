using ClientCount.Models;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    internal class EmployeeDetailModel: ViewModelBase
    {
        private readonly EmployeeService employeeService;
        private readonly Employee CurrentEmployee;
        public EmployeeDetailModel(Employee currentEmployee) {
        
            employeeService = new EmployeeService();
            CurrentEmployee = employeeService.ReadById(currentEmployee.Id);
            FirstName = CurrentEmployee.FirstName;
            LastName = CurrentEmployee.LastName;
            Patronymic = CurrentEmployee.Patronymic;
            PhoneNumber = CurrentEmployee.PhoneNumber;
           
        }
        private string firstName;
        public string FirstName
        {
            set {
            if(firstName!=value)
                    firstName = value;
            OnPropertyChanged("FirstName");
            }
            get { return firstName; }
        }

        private string lastName;
        public string LastName
        {
            set
            {
                if (lastName != value)
                    lastName = value;
                OnPropertyChanged("LastName");
            }
            get { return lastName; }
        }

        private string patronymic;
        public string Patronymic
        {
            set
            {
                if (patronymic != value)
                    patronymic = value;
                OnPropertyChanged("Patronymic");
            }
            get { return patronymic; }
        }

        private string phonenumber;
        public string PhoneNumber
        {
            set
            {
                if (phonenumber != value)
                    phonenumber = value;
                OnPropertyChanged("PhoneNumber");
            }
            get { return phonenumber; }
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
        public ICommand UpdateEmployeeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        CurrentEmployee.FirstName = firstName;
                        CurrentEmployee.LastName = lastName;
                        CurrentEmployee.Patronymic = patronymic;
                        CurrentEmployee.PhoneNumber = phonenumber;
                        int result = employeeService.UpdateEmployee(CurrentEmployee);
                        if(result > 0)
                        {
                            var option = new ToastView("Data updated successfully!");
                            MessagingCenter.Send("EmployeeDetails", "UpdateListView", "Success");
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
        public ICommand DeleteEmployeeCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var accepted = await Application.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete the employee?", "Yes", "No");
                    if (accepted)
                    {

                        var conn = App.DataBase.Connection;
                       
                        int result = employeeService.DeleteEmployee(CurrentEmployee);
                        if (result > 0)
                        {

                            MessagingCenter.Send("EmployeeDetails", "UpdateListView", "Success");
                            await App.Navigation.PopAsync();
                        }
                    }

                });
            }
        }
    }
}

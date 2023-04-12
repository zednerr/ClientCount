using ClientCount.Models;
using ClientCount.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using static SQLite.SQLite3;

namespace ClientCount.MvvM.ViewModels
{
    public class AddActionsViewModel:ViewModelBase
    {
        private readonly ActionService actionsService;
        private readonly LivingPlace Current_LivingPlace;
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
        private string actionType;
        private string dateAction;
        private int livingplace_id;
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
        public string ActionType
        {
            set
            {
                if (actionType != value)
                {
                    actionType = value;
                    OnPropertyChanged("ActionType");
                }
            }
            get
            {
                return actionType;
            }
        }
        public string DateAction
        {
            set
            {
                if (dateAction != value)
                {
                    dateAction = value;
                    OnPropertyChanged("DateAction");
                }
            }
            get
            {
                if (dateAction == null)
                {
                    dateAction = DateTime.Today.ToString();
                    OnPropertyChanged("DateAction");
                }
                return dateAction;
            }
        }
        //public string Employee_Lastname
        //{
        //    set
        //    {
        //        if (employee_Lastname != value)
        //        {
        //            employee_Lastname = value;
        //            OnPropertyChanged("Employee_Lastname");
        //        }
        //    }
        //    get
        //    {
        //        return employee_Lastname;
        //    }
        //}
        
        public int Livingplace_id
        {
            set
            {
                if (livingplace_id != value)
                {
                    livingplace_id = value;
                    OnPropertyChanged("Livingplace_id");
                }
            }
            get
            {
                return livingplace_id;
            }
        }
        public ICommand AddActionsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (CanFormData(new string[] { actionType, dateAction, SelectedEmployee.Id.ToString(), livingplace_id.ToString() }) == true)
                        {
                            throw new NullReferenceException();
                        }
                        int result = actionsService.CreateAction(new Actions
                        {
                            ActionType = actionType,
                            DateAction = Convert.ToDateTime(dateAction).ToString("yyyy-MM-dd"),
                            Employee_id = SelectedEmployee.Id,
                            LivingPlace_id = livingplace_id

                        });
                        if (result > 0)
                        {
                            var option = new ToastView("Task successfully created!");
                            MessagingCenter.Send("AddActions", "UpdateListView", "Success");
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
        public AddActionsViewModel(LivingPlace Lplace)
        {
            actionsService = new ActionService();
            Current_LivingPlace = Lplace;
            Livingplace_id = Current_LivingPlace.Id;
            EmployeeService employeeService = new EmployeeService();
            Employees = employeeService.ReadAll();
        }

    }
}

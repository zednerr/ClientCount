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
    public class ListEmployeeViewModel:ViewModelBase
    {
        private List<Employee> employees = new List<Employee>();
        public List<Employee> Employees {
            set {
                if (employees.Count != value.Count)
                {
                    employees = value;
                    OnPropertyChanged("Employees");
                }
            }
            get { return employees; }
        }
        private Employee _selectedItem;
        public Employee SelectedItem
        {
            set
            {
                _selectedItem = value;
                if (_selectedItem == null)
                {
                    return;
                }
                NavigateToEmployeesDetails(_selectedItem);
                _selectedItem = null;
                OnPropertyChanged("SelectedItem");
            }
        }
        public void NavigateToEmployeesDetails(Employee employee)
        {
            App.Navigation.PushAsync(new NavigationPage(new EmployeeDetailPage(employee)));
        }
        public ICommand AddEmployeeCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await App.Navigation.PushAsync(new NavigationPage(new AddEmployeePage()));
                });
            }
        }
        public ListEmployeeViewModel()
        {
            var employeeService = new EmployeeService();
            Employees = employeeService.ReadAllEmployeeforview();
        }
    }
}

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
        private List<Brand> brands = new List<Brand>();
        private List<Model> models = new List<Model>();
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

        public List<Brand> Brands
        {
            set
            {
                if (brands != value)
                {
                    brands = value;
                    OnPropertyChanged("Brands");
                }
            }
            get { return brands; }
        }

        public List<Model> Models
        {
            set
            {
                if (models != value)
                {
                    models = value;
                    OnPropertyChanged("Models");
                }
            }
            get { return models; }
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
            var modelService = new ModelService();
            var brandService = new BrandService();
            Employees = employeeService.ReadAllEmployeeforview();
            Models = modelService.GetAllModels();
            Brands = brandService.GetAllBrands();
        }
    }
}

using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    public class ActionsListViewModel : ViewModelBase
    {
        private EmployeeService employeeService = new EmployeeService();
        
        private string firstName;
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
        private string lastName;
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
        private string patronymic;
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
        private string phoneNumber;
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
        //private List<Actions> actionslist = new List<Actions>();
        //public List<Actions> Actions
        //{
        //    set
        //    {
        //        if (actionslist.Count != value.Count)
        //        {
        //            actionslist = value;
        //            OnPropertyChanged("Actions");
        //        }

        //    }
        //    get
        //    {
        //        return actionslist;
        //    }
        //}
        private List<DopActions> actionsdoplist = new List<DopActions>();
        public List<DopActions> ActionsAdd
        {
            set
            {
                if (actionsdoplist.Count != value.Count)
                {
                    actionsdoplist = value;
                    OnPropertyChanged("ActionsAdd");
                }

            }
            get
            {
                return actionsdoplist;
            }
        }

        public ActionsListViewModel()
        {
            var actionsService = new ActionService();
            //actionslist = actionsService.ReadAll();
            List<DopActions> new_list = new List<DopActions>();
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            var conn = App.DataBase.Connection;
            var list = from client in conn.Table<Client>()
                       from livingPlace in conn.Table<LivingPlace>()
                       from actions in conn.Table<Actions>()
                       from employee in conn.Table<Employee>()
                       where client.Id == livingPlace.Client_id
                       where livingPlace.Id == actions.LivingPlace_id
                       where actions.Employee_id == employee.Id
                       where actions.DateAction == date
                       where actions.Result ==null
                       select new
                       {
                           id = actions.Id,
                           actiontype = actions.ActionType,
                           dataaction = actions.DateAction,
                           result = actions.Result,
                           fullname = client.LastName+" "+client.FirstName+" "+client.Patronymic,
                           street = livingPlace.Street,
                           employee_lastname = employee.LastName
                       };
            foreach (var i in list)
            {
                new_list.Add(new DopActions { FullName = i.fullname, EmployeeLastName = i.employee_lastname, Street = i.street,ActionType=i.actiontype,DateAction = i.dataaction,Result=i.result,Id = i.id });
            }
            actionsdoplist = new_list;
        }
       

    }
}

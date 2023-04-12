using ClientCount.Models;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientCount.MvvM.ViewModels
{
    public class AnalyticsViewModel : ViewModelBase
    {
        private string employeeList;
        public string EmployeeList
        {
            get { return employeeList; }
            set
            {
                if (employeeList != value)
                {
                    employeeList = value;
                }
            }
        }
        public AnalyticsViewModel(string employerNumber){

            var conn = App.DataBase.Connection;
            var list = from actions in conn.Table<Actions>()
                       from employee in conn.Table<Employee>()
                       where actions.Employee_id == employee.Id

                       select new
                       {
                           id = actions.Id,
                           actiontype = actions.ActionType,
                           dataaction = actions.DateAction,
  
                       };
        }
    }
}

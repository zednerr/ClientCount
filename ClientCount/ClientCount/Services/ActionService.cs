using ClientCount.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClientCount.Services
{
    public class ActionService
    {
        public int CreateAction(Actions action)
        {
            var conn = App.DataBase.Connection;
            return conn.Insert(action);
        }

        public List<Actions> ReadAll()
        {
            List<Actions> list = new List<Actions>();
            var conn = App.DataBase.Connection;
            string date = DateTime.Today.ToString();
            return conn.Query<Actions>("SELECT * FROM Actions WHERE DateAction = ?",new string[1] { date });
            
        }

        public List<DopActions> ReadAddAll()
        {
            List<DopActions> new_list= new List<DopActions>();
            string date = DateTime.Today.ToString();
            var conn = App.DataBase.Connection;
            var list = from client in conn.Table<Client>()
                       from livingPlace in conn.Table<LivingPlace>()
                       from actions in conn.Table<Actions>()
                       from employee in conn.Table<Employee>()
                       where client.Id == livingPlace.Client_id
                       where livingPlace.Id == actions.LivingPlace_id
                       where actions.Employee_id == employee.Id
                       where actions.DateAction == date
                       select new
                       {
                           fullname = client.LastName,
                           street = livingPlace.Street,
                           employee_lastname = employee.LastName
                       };
            foreach(var i in list)
            {
               new_list.Add(new DopActions {FullName = i.fullname,EmployeeLastName = i.employee_lastname, Street = i.street });
            }
         return new_list.ToList();
        }
        public Actions ReadById(int id)
        {
            var conn = App.DataBase.Connection;
           
            return conn.Query<Actions>($"SELECT * FROM Actions WHERE Id = {id}").First();
        }
        public void DeleteActionsById(int id)
        {
            var conn = App.DataBase.Connection;
            conn.Query<Actions>($"DELETE FROM Actions WHERE LivingPlace_id ={id}");
        }

    }
}

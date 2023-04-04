using ClientCount.Models;
using System.Collections.Generic;

namespace ClientCount.Services
{
    public class EmployeeService

    {

        public int AddEmployee(Employee employee)
        {
            var conn = App.DataBase.Connection;

            return conn.Insert(employee);
        }
        public int AddEmployees(List<Employee> employees)
        {
            var conn = App.DataBase.Connection;

            return conn.InsertAll(employees);
        }
        public int UpdateEmployee(Employee employee)
        {
            var conn = App.DataBase.Connection;
            return conn.Update(employee);
        }
        public int DeleteEmployee(Employee employee)
        {
            var conn = App.DataBase.Connection;
            return conn.Delete<Employee>(employee.Id);
        }
        public List<Employee> ReadAll()
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();

            return conn.Query<Employee>("SELECT * FROM employee");
        }
        public List<Employee> ReadAllEmployeeforview()
        {
            var conn = App.DataBase.Connection;
            return conn.Query<Employee>("SELECT id,FirstName||' '||LastName as firstname,phonenumber from Employee");
        }
        public Employee ReadById(int id)
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();

            return conn.Get<Employee>(id);
        }
    }
}

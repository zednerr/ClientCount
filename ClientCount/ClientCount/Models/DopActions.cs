using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Models
{
    public class DopActions
    {
        public int Id { get; set; }
       
        public string ActionType { get; set; }
   
        public string DateAction { get; set; }
       
        public string Result { get; set; }
        public string FullName { get; set; }
        public string Street { get; set; }
        public string EmployeeLastName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Models
{
    public class SearchResult
    {
        public List<Client> ReturnData_Client { get; set; }
        public List<Employee> ReturnData_Employee { get; set; }
        public List<LivingPlace> ReturnData_LivingPlace { get; set; }
        public List<DopActions> ReturnData_Action { get; set;}
    }
}

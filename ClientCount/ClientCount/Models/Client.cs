using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClientCount.Models
{
    public class Client
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }
        [NotNull] public string FirstName { get; set; }
        [NotNull] public string LastName { get; set; }
        public string Patronymic { get; set; }
        [NotNull] public string PhoneNumber { get; set; }
        public string HphoneNumber { get; set; }
        [NotNull] public int Employee_id { get; set; }
    }
}

//
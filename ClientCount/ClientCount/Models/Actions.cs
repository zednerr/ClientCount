using DocumentFormat.OpenXml.Bibliography;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Models
{
    public class Actions
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string ActionType { get; set; }//Сервис ГТО Звонок ГТО Сервис ТО Звонок ТО
        [NotNull]
        public string DateAction { get; set; }
        [NotNull]
        public int Employee_id { get; set; }
        public string Result { get; set; }
        [NotNull]
        public int LivingPlace_id { get; set; }
    }
}

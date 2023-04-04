using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Models
{
    public class LivingPlace
    {
        //LivingPlace
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string City { get; set; }
        [NotNull]
        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }
        [NotNull]
        public string Region { get; set; }
        [NotNull]
        public string Street { get; set; }
        ////Equipment
        [NotNull]
        public string BrandName { get; set; }
        public string DateSold { get; set; } = DateTime.Now.ToString("D");
        [NotNull]
        public string DateStartExp { get; set; } = DateTime.Now.ToString("D");
        [Unique]
        public string GuaranteeNumber { get; set; }
        [NotNull]
        public string ModelName { get; set; }
        [NotNull][Unique]
        public string SerialNumber { get; set; }
        [NotNull]
        public string TypeModel { get; set; }
        [NotNull]
        public int Client_id { get; set; }
    }
}

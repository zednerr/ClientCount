using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Models
{
    public class Brand
    {
        [PrimaryKey][AutoIncrement] public int Id { get; set; }
        [NotNull] [Unique] public string BrandName { get; set; }
    }
}

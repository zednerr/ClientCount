using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Models
{
    public class Model
    {
        [PrimaryKey][AutoIncrement] public int Id { get; set; }
        [NotNull][Unique] public string ModelName { get; set; }
    }
}

using SQLite;
using System;

namespace Milage.Models
{
    public class Trips
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Kilometers { get; set; }
        public string Month { get; set; }
    }
}

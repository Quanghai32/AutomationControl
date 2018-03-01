using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismTest
{
    public class History
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Date { get; set; }

        public string Shift { get; set; }
        public double AvailabilityRate { get; set; }
        public double Performance { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSearchesServer
{
    class Car:WorkSQL
    {       
        public string Search { get; set; }
        public string GearBox { get; set; }
        public string Currency { get; set; }
        public string Speed { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public void Clean()
        {
            this.Search = "";
            this.GearBox = "";
            this.Currency = "";
            this.Speed = "";
            this.Price = "";
            this.Description = "";
            this.Brand = "";
            this.Name = "";           
            this.VIN = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSearchesServer
{
    class Employee:Client
    {
        public Employee()
        {
            Password = "";
            Access = "";
        }

        //public string Login { get; set; }
        public string Password { get; set; }
        public string Access { get; set; }

        public override void Clean()
        {
            base.Clean();
            this.Password = "";
            this.Access = "";
            this.Login = "";
        }
    }
}

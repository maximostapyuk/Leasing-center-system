using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSearchesServer
{
    class LogIn
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public void Clean()
        {
            this.Password = "";
            this.Login = "";
        }

    }
}

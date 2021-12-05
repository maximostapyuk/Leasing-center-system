using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSearchesServer
{
    class Client:WorkSQL
    {
        public Client()
        {
            Search = "";
            Name = "";
            Surname = "";
            Thirdname = "";
            Gender = "";
            Email = "";
            Adress = "";
            PhoneCode = "";
            Phone = "";
            Age = "";
            Login = "";
        }

        public string Search { get; set; }
        //public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Thirdname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string PhoneCode { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public virtual void Clean()
        {
            this.Search = "";
            this.ID = "";
            this.Name = "";
            this.Surname = "";
            this.Thirdname = "";
            this.Gender = "";
            this.Email = "";
            this.Adress = "";
            this.PhoneCode = "";
            this.Phone = "";
            this.Age = "";
            this.ID = "";
        }
    }
}

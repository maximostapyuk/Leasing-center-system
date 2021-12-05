using Aspose.Cells.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSearchesServer
{
    class Expert: WorkSQL
    {
        public string Mark1_2 { get; set; }
        public string Mark1_3 { get; set; }
        public string Mark1_4 { get; set; }
        public string Mark2_3 { get; set; }
        public string Mark2_4 { get; set; }
        public string Mark3_4 { get; set; }
        public void Clean()
        {
            this.Mark1_2 = "";
            this.Mark1_3 = "";
            this.Mark1_4 = "";
            this.Mark2_3 = "";
            this.Mark2_4 = "";
            this.Mark3_4 = "";
            this.ExpertNum = "";
        }


    }
}

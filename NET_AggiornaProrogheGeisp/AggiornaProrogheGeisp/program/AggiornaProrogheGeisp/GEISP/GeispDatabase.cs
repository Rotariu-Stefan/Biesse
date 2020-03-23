using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AggiornaProrogheGeisp.GEISP
{
    class GeispDatabase
    {
        public string name
        { get; set; }
        public string mdb
        { get; set; }
        public string excel
        { get; set; }
        public string mdb2
        { get; set; }
        public GeispDatabase() { }

        public GeispDatabase(string name, string mdb, string excel, string mdb2) 
        {
            this.name = name;
            this.mdb = mdb;
            this.mdb2 = mdb2;
            this.excel = excel;
        }
    }
}

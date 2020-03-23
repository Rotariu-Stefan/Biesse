using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InserimentoNoteGEISP.GEISP
{
    class GeispDatabase
    {
        public string name
        { get; set; }
        public string path
        { get; set; }
        public string codBas
        { get; set; }
        public string societatId
        { get; set; }
        public GeispDatabase() { }

        public GeispDatabase(string name, string codBas, string path, string societatId) 
        {
            this.name = name;
            this.path = path;
            this.codBas = codBas;
            this.societatId = societatId;
        }
        public string getSocietaName()
        {
            return GeispUtils.getSocietaName(this.societatId);
        }
    }
}

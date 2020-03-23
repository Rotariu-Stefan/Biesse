using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace InserimentoNoteGEISP.GEISP
{
    class GeispUtils
    {
        static string directory = @"D:\NET_InserimentoNoteGeisp\InserimentoNoteGEISP\";
        static XElement doc = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "GEISP\\GEISP.xml");

        public static List<GeispDatabase> getDatabases() 
        {
            List<GeispDatabase> asocs = new List<GeispDatabase>();

            foreach (var a in doc.Elements("asoc")) 
            {
                var db = new GeispDatabase(a.Element("name").Value, a.Element("mdb").Value,
                    a.Element("excel").Value, a.Element("mdb2").Value);
                asocs.Add(db);
            }
            return asocs;
        }

        public static GeispDatabase getDatabase(string name)
        {
            var databases = getDatabases();
            return databases.Where(d => d.name == name).FirstOrDefault();
        }
        public static List<string> getDatabasesNames()
        {
            List<GeispDatabase> databases = getDatabases();
            List<string> names = new List<string>();
            foreach (var db in databases)
            {
                names.Add(db.name);
            }
            return names;
        }

        public static void addDatabase(GeispDatabase db)
        {
            XElement asocs = doc;
            XElement xmlDb = new XElement("asoc");
            xmlDb.SetAttributeValue("name", db.name);
            xmlDb.SetElementValue("name", db.name);
            xmlDb.SetElementValue("mdb", db.mdb);
            xmlDb.SetElementValue("excel", db.excel);
            xmlDb.SetElementValue("mdb2", db.mdb2);
            asocs.Add(xmlDb);
            doc.Save(directory + "GEISP\\GEISP.xml");
        }
        public static void modifyDatabase(string name, GeispDatabase db)
        {
            XElement xmlDb = doc.Elements("asoc").Where(d => d.Attribute("name").Value.ToString() == name).FirstOrDefault();
            xmlDb.Attribute("name").SetValue(db.name);
            xmlDb.Element("name").SetValue(db.name);
            xmlDb.Element("mdb").SetValue(db.mdb);
            xmlDb.Element("excel").SetValue(db.excel);
            xmlDb.Element("mdb2").SetValue(db.mdb2);
            doc.Save(directory + "GEISP\\GEISP.xml");
        }

        public static void removeDatabase(string name) 
        {
            XElement xmlDb = doc.Elements("asoc").Where(d => d.Attribute("name").Value.ToString() == name).FirstOrDefault();
            xmlDb.Remove();
            doc.Save(directory + "GEISP\\GEISP.xml");
        }

        public static void removeAllDatabases()
        {
            doc.Elements("asoc").Remove();
            doc.Save(directory + "GEISP\\GEISP.xml");
        }

        #region irrelevent
        public static string getSocietaName(string societaId)
        {
            List<string> names = new List<string>();
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");
            XElement firm = doc.Elements("firms").FirstOrDefault().Elements("firm").Where(f => f.Attribute("Id").Value.ToString() == societaId).FirstOrDefault();
            return firm.Attribute("name").Value.ToString();
        }

        public static List<string> getSocietaNames()
        {
            List<string> names = new List<string>();
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");

            foreach (var d in doc.Elements("firms").FirstOrDefault().Elements("firm")) 
            {
                names.Add(d.Attribute("name").Value.ToString());
            }
            return names;
        }

        public static string getSocietaId(string name)
        {
            List<string> names = new List<string>();
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");
            XElement firm = doc.Elements("firms").FirstOrDefault().Elements("firm").Where(f => f.Attribute("name").Value.ToString() == name).FirstOrDefault();
            return firm.Attribute("Id").Value.ToString();
        }
        #endregion
    }

}

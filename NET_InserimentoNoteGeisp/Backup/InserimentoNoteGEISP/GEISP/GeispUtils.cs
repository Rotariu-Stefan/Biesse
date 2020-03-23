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
        public static List<GeispDatabase> getDatabases() 
        {
            List<GeispDatabase> databases = new List<GeispDatabase>();
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");

            foreach (var d in doc.Elements("databases").FirstOrDefault().Elements("database")) 
            {
                var db = new GeispDatabase(d.Attribute("name").Value.ToString(), 
                    d.Attribute("codBas").Value.ToString(), 
                    d.Attribute("path").Value.ToString(),d.Attribute("societaId").Value.ToString());
                databases.Add(db);
            }
            return databases;
        } //gg
        public static GeispDatabase getDatbase(string name)
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

        public static void modifyDatabase(string name, GeispDatabase db)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");
            XElement xmlDb = doc.Elements("databases").FirstOrDefault().Elements("database").Where(d => d.Attribute("name").Value.ToString() == name).FirstOrDefault();
            xmlDb.SetAttributeValue("name", db.name);
            xmlDb.SetAttributeValue("path", db.path);
            xmlDb.SetAttributeValue("codBas", db.codBas);
            xmlDb.SetAttributeValue("societaId", db.societatId);
            doc.Save(directory + "GEISP\\GEISP.xml");
        }
        public static void removeDatabase(string name) 
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");
            XElement xmlDb = doc.Elements("databases").FirstOrDefault().Elements("database").Where(d => d.Attribute("name").Value.ToString() == name).FirstOrDefault();
            xmlDb.Remove();
            doc.Save(directory + "GEISP\\GEISP.xml");
        }
        public static void addDatabase(GeispDatabase db)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");
            XElement databases = doc.Elements("databases").FirstOrDefault();
            XElement xmlDb = new XElement("database");
            xmlDb.SetAttributeValue("name", db.name);
            xmlDb.SetAttributeValue("path", db.path);
            xmlDb.SetAttributeValue("codBas", db.codBas);
            xmlDb.SetAttributeValue("societaId", db.societatId);
            databases.Add(xmlDb);
            doc.Save(directory + "GEISP\\GEISP.xml");
        }
        public static void removeAllDatabases()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            XElement doc = XElement.Load(directory + "GEISP\\GEISP.xml");
            doc.Elements("databases").FirstOrDefault().Elements("database").Remove();
            doc.Save(directory + "GEISP\\GEISP.xml");
        }

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
    }

}

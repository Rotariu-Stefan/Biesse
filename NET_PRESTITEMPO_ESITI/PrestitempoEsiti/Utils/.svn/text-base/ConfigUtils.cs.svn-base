using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFG = System.Configuration;
using System.Configuration;

namespace PrestitempoInserimento.Utils
{
    class ConfigUtils
    {
        public CFG.Configuration cfg = CFG.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

        public static string GetConnectionString(bool forceTrusted)
        {
            string connectionString = string.Empty;
            if (forceTrusted)
            {
                connectionString = "Server=" + CFG.ConfigurationManager.AppSettings["SQLServer"] + ";Database=" + CFG.ConfigurationManager.AppSettings["SQLDatabase"] + ";Trusted_Connection=True;";

            }
            else
            {
                if (!GetTrustedConnection())
                {
                    connectionString = "Data Source=" + CFG.ConfigurationManager.AppSettings["SQLServer"]
                       + ";Initial Catalog=" + CFG.ConfigurationManager.AppSettings["SQLDatabase"]
                       + ";User Id=" + CFG.ConfigurationManager.AppSettings["SQLUser"]
                       + ";Password=" + CFG.ConfigurationManager.AppSettings["SQLPassword"];
                }
                else
                {
                    connectionString = "Server=" + CFG.ConfigurationManager.AppSettings["SQLServer"] + ";Database=" + CFG.ConfigurationManager.AppSettings["SQLDatabase"] + ";Trusted_Connection=True;";
                }
            }

            return connectionString;
        }
        #region General
        public static List<string> GetScheduledDays()
        {
            var days = new List<string>();
            string d = CFG.ConfigurationManager.AppSettings["Days"];
            days = d.Split(';').ToList<string>();
            return days;
        }
        public void SetDays(List<string> days) 
        {
            string dayStr = "";
            foreach (var d in days)
            {
                dayStr += d;
                if (days.IndexOf(d) != days.Count - 1)
                {
                    dayStr += ";";
                }
            }
            
            cfg.AppSettings.Settings["Days"].Value = dayStr;
        }
        public static string GetHours()
        {
            return CFG.ConfigurationManager.AppSettings["Hours"];
        }
        public static string GetMinutes()
        {
            return CFG.ConfigurationManager.AppSettings["Minutes"];

        }
        //public static string GetDay()
        //{
        //    return CFG.ConfigurationManager.AppSettings["Day"];
        //}

        //public static string GetEnglishDay()
        //{
        //    string day = GetDay();
        //    switch (day)
        //    {
        //        case "Lunedi": return "Monday";
        //        case "Martedi": return "Tuesday";
        //        case "Mercoledi": return "Wednesday";
        //        case "Giovedi": return "Thursday";
        //        case "Venerdi": return "Friday";
        //        case "Sabato": return "Saturday";
        //        case "Domenica": return "Sunday";
        //    }
        //    return "";
        //}
        public static string GetLogPath()
        {
            return CFG.ConfigurationManager.AppSettings["LogPath"];
        }
        public static string GetExcelPath()
        {
            return CFG.ConfigurationManager.AppSettings["ExcelPath"];
        }
        public void SetExcelPath(string value)
        {
            cfg.AppSettings.Settings["ExcelPath"].Value = value;
        }
        public void SetHours(string value)
        {
            cfg.AppSettings.Settings["Hours"].Value = value;
        }
        public void SetMinutes(string value)
        {
            cfg.AppSettings.Settings["Minutes"].Value = value;
        }
        //public void SetDay(string value)
        //{
        //    cfg.AppSettings.Settings["Day"].Value = value;
        //}
        public void SetLogPath(string value)
        {
            cfg.AppSettings.Settings["LogPath"].Value = value;
        }
        #endregion
        #region WebSite
        public static string GetWebSiteUser()
        {
            return CFG.ConfigurationManager.AppSettings["WebSiteUser"];
        }
        public static string GetWebSitePassword()
        {
            return CFG.ConfigurationManager.AppSettings["WebSitePassword"];
        }
        #endregion
        public void SetWebSiteUser(string value)
        {
            cfg.AppSettings.Settings["WebSiteUser"].Value = value;
        }
        public void SetWebSitePassword(string value)
        {
            cfg.AppSettings.Settings["WebSitePassword"].Value = value;
        }
        #region Mail
        public static string GetMailServer()
        {
            return CFG.ConfigurationManager.AppSettings["MailServer"];
        }
        public static string GetCaricoAddress()
        {
            return CFG.ConfigurationManager.AppSettings["CaricoAddress"];
        }
        public static string GetPostAddress()
        {
            return CFG.ConfigurationManager.AppSettings["PostAddress"];
        }
        public static string GetServerPort()
        {
            return CFG.ConfigurationManager.AppSettings["ServerPort"];
        }
        public static bool GetServerSecurity()
        {
            return CFG.ConfigurationManager.AppSettings["ServerSecurity"].ToLower() == "true" ? true : false;
        }
        public static string GetSenderAddress()
        {
            return CFG.ConfigurationManager.AppSettings["SenderAddress"];
        }
        public static string GetSenderPassword()
        {
            return CFG.ConfigurationManager.AppSettings["SenderPassword"];
        }

        public void SetMailServer(string value)
        {
            cfg.AppSettings.Settings["MailServer"].Value = value;
        }
        public void SetCaricoAddress(string value)
        {
            cfg.AppSettings.Settings["CaricoAddress"].Value = value;
        }
        public void SetPostAddress(string value)
        {
            cfg.AppSettings.Settings["PostAddress"].Value = value;
        }
        public void SetServerPort(string value)
        {
            cfg.AppSettings.Settings["ServerPort"].Value = value;
        }
        public void SetServerSecurity(bool value)
        {
            cfg.AppSettings.Settings["ServerSecurity"].Value = value.ToString();
        }
        public void SetSenderAddress(string value)
        {
            cfg.AppSettings.Settings["SenderAddress"].Value = value;
        }
        public void SetSenderPassword(string value)
        {
            cfg.AppSettings.Settings["SenderPassword"].Value = value;
        }
        #endregion

        #region SqlServer
        public static bool GetTrustedConnection()
        {
            return CFG.ConfigurationManager.AppSettings["TrustedConnection"].ToLower() == "true" ? true : false;

        }
        public static string GetSQLServer()
        {
            return CFG.ConfigurationManager.AppSettings["SQLServer"];
        }
        public static string GetSQLDataBase()
        {
            return CFG.ConfigurationManager.AppSettings["SQLDatabase"];
        }
        public static string GetSQLUser()
        {
            return CFG.ConfigurationManager.AppSettings["SQLUser"];
        }
        public static string GetSQLPassword()
        {
            return CFG.ConfigurationManager.AppSettings["SQLPassword"];
        }

        public void SetTrustedConnection(bool value)
        {
            cfg.AppSettings.Settings["TrustedConnection"].Value = value.ToString();
        }
        public void SetSQLServer(string value)
        {
            cfg.AppSettings.Settings["SQLServer"].Value = value;
        }
        public void SetSQLDataBase(string value)
        {
            cfg.AppSettings.Settings["SQLDataBase"].Value = value;
        }
        public void SetSQLUser(string value)
        {
            cfg.AppSettings.Settings["SQLUser"].Value = value;
        }
        public void SetSQLPassword(string value)
        {
            cfg.AppSettings.Settings["SQLPassword"].Value = value;
        }
        #endregion

        public static string GetMailBody()
        {
            return CFG.ConfigurationManager.AppSettings["MailBody"];
        }
        public static string GetMailSubject()
        {
            return CFG.ConfigurationManager.AppSettings["MailSubject"];
        }
    }
}

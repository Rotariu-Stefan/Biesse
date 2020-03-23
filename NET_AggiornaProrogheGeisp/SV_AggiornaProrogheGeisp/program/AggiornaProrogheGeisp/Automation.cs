using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace AggiornaProrogheGeisp
{
    public class Automation
    {
        public static string cdir = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
        public static bool running;
        private static List<List<string>> databases;

        public static void loadDatabases(XmlDocument doc)
        {
            databases = new List<List<string>>();
            databases.Clear();

            if(doc==null)
            {
                doc = new XmlDocument();
                doc.Load(Path.Combine(cdir, "associations.xml"));
            }
            XmlNode root = doc.SelectSingleNode("Root");
            foreach (XmlNode asoc in root.ChildNodes)
            {
                string[] a = { asoc.ChildNodes[1].InnerText, asoc.ChildNodes[2].InnerText, asoc.ChildNodes[0].InnerText, asoc.ChildNodes[3].InnerText };
                databases.Add(new List<string>(a));
            }
        }

        public static string execute()
        {
            running = true;
            System.Threading.Thread.Sleep(5000);
            running = false;
            return "Not Yet!";

            DateTime dataProroga = new DateTime(2013, 09, 30);
            string proroga = string.Format("{0:dd/MM/yyyy}", dataProroga);
            DataTable anomalies = new DataTable();
            anomalies.Columns.Add("Velina");
            anomalies.Columns.Add("Contratto");
            anomalies.Columns.Add("StatoAgente");
            anomalies.Columns.Add("Banca");

            DataTable lavorate = new DataTable();
            lavorate.Columns.Add("Velina");
            lavorate.Columns.Add("Contratto");
            lavorate.Columns.Add("StatoAgente");
            lavorate.Columns.Add("Banca");

            DataTable nonlavorateNonEsistenti = new DataTable();
            nonlavorateNonEsistenti.Columns.Add("Velina");
            nonlavorateNonEsistenti.Columns.Add("Contratto");
            nonlavorateNonEsistenti.Columns.Add("StatoAgente");
            nonlavorateNonEsistenti.Columns.Add("Banca");

            DataTable nonlavorateChiuse = new DataTable();
            nonlavorateChiuse.Columns.Add("Velina");
            nonlavorateChiuse.Columns.Add("Contratto");
            nonlavorateChiuse.Columns.Add("StatoAgente");
            nonlavorateChiuse.Columns.Add("Banca");

            DataTable giaLavorate = new DataTable();
            giaLavorate.Columns.Add("Velina");
            giaLavorate.Columns.Add("Contratto");
            giaLavorate.Columns.Add("StatoAgente");
            giaLavorate.Columns.Add("Banca");

            //DateTime proro = Convert.ToDateTime(proroga);
            //MessageBox.Show(dataProroga.Date.ToString());
            string findError = string.Empty;
            for (int i = 0; i < databases.Count; i++)
            {
                try
                {
                    DataTable exceltable = ExcelUtil.Import(databases[i][2]);
                    AccesUtil accobj = new AccesUtil();
                    accobj.InitConnection(databases[i][1]);
                    DataTable accestable = new DataTable();

                    foreach (DataRow excelrow in exceltable.Rows)
                    {
                        //bool found = false;
                        accestable = accobj.ExtractValues(string.Format("Select NumeroRapporto, DataChiusuraIncarico, PCSocieta, PC, DataProrogaRichiestaDaSocieta from Rapporti where NumeroRapporto like \"%{0}\"", excelrow[1].ToString().Trim().Substring(excelrow[1].ToString().Trim().Length - 7, 7)));
                        //if (excelrow[1].ToString().Substring(excelrow[1].ToString().Length - 7, 7).CompareTo(accesrow[0].ToString().Substring(accesrow[0].ToString().Length - 7, 7)) == 0)
                        if (accestable.Rows.Count > 0)
                        {
                            DataRow accesrow = accestable.Rows[0];
                            for (int r = 0; r < accestable.Rows.Count; r++)
                            {
                                if (string.IsNullOrEmpty(accestable.Rows[r][1].ToString()))
                                {
                                    accesrow = accestable.Rows[r];
                                    break;
                                }
                            }
                            findError = "Error is in 1st part";
                            //found = true;
                            if (string.IsNullOrEmpty(accesrow[1].ToString()))
                            {
                                findError = "ceva nu a mers la extragerea statopratica " + excelrow[0].ToString();
                                DataTable statoPratica = accobj.ExtractValues(string.Format("Select stato from pratiche where cod_vel={0}", excelrow[0].ToString()));
                                string notizia = string.Empty;
                                if (statoPratica.Rows.Count > 0)
                                {
                                    notizia = getNota(excelrow[2].ToString().Trim(), statoPratica.Rows[0][0].ToString());
                                }
                                else
                                {
                                    notizia = getNota(excelrow[2].ToString().Trim(), string.Empty);
                                }
                                if (!string.IsNullOrEmpty(notizia))
                                {

                                    findError = "ceva nu a mers la comparatia prorogei " + excelrow[0].ToString();
                                    if (accesrow[4].ToString().CompareTo(dataProroga.ToString()) != 0)
                                    {
                                        //accobj.ExecuteNonQuery(string.Format("Update NotiziaIncaricoPC set LunghezzaNota='{0}', Nota='{1}' where PCSocieta='{2}' and PCCliente='{3}' and DataNotizia=date()", getNota(excelrow[3].ToString().Trim(), statoPratica.Rows[0][0].ToString()).Length.ToString("D3"), getNota(excelrow[3].ToString().Trim(), statoPratica.Rows[0][0].ToString()), accesrow[2].ToString(), accesrow[3].ToString()));

                                        if ((notizia == "220") || (notizia == "222") || (notizia == "568") || (notizia == "297"))
                                        {
                                            notizia = "il pdr e'' irregolare stiamo cercando rientrare dell''esposizione";
                                            accobj.ExecuteNonQuery(string.Format("Insert into NotiziaIncaricoPC (PCSocieta,PCCliente,DataNotizia,LunghezzaNota,Nota,Data_Ultima_Modifica_Soc,Flag_DS,Flag_Old_New) values ('{0}','{1}',date(),'{2}','{3}',date(),'{4}','{5}')", accesrow[2], accesrow[3], notizia.Length.ToString("D3"), notizia, "S", "NEW"));
                                            lavorate.ImportRow(excelrow);
                                        }
                                        else
                                        {
                                            accobj.ExecuteNonQuery(string.Format("Insert into Rapporti_Log Select * from Rapporti where NumeroRapporto='{0}'", accesrow[0].ToString()));
                                            accobj.ExecuteNonQuery(string.Format("Update Rapporti_Log set DataInserimentoGruppo=now() where NumeroRapporto='{0}' and DataProrogaRichiestaDaSocieta is null", accesrow[0].ToString()));
                                            accobj.ExecuteNonQuery(string.Format("Update Rapporti set Flag_DS='{0}', CodiceComunicazione='{1}', DataProrogaRichiestaDaSocieta=DateValue('{2}'), Data_Ultima_Modifica_Soc=date() where NumeroRapporto='{3}'", "S", "B", proroga, accesrow[0].ToString()));
                                            accobj.ExecuteNonQuery(string.Format("Insert into NotiziaIncaricoPC (PCSocieta,PCCliente,DataNotizia,LunghezzaNota,Nota,Data_Ultima_Modifica_Soc,Flag_DS,Flag_Old_New) values ('{0}','{1}',date(),'{2}','{3}',date(),'{4}','{5}')", accesrow[2], accesrow[3], notizia.Length.ToString("D3"), notizia, "S", "NEW"));
                                            lavorate.ImportRow(excelrow);
                                        }
                                        //break;
                                    }
                                    else
                                    {
                                        giaLavorate.ImportRow(excelrow);
                                        //break;
                                    }
                                }
                                else
                                {
                                    anomalies.ImportRow(excelrow);
                                    //break;
                                }
                            }
                            else
                            {
                                nonlavorateChiuse.ImportRow(excelrow);
                                //break;
                            }
                        }
                        else
                        {
                            //findError="error is in second part";
                            AccesUtil accgetnew = new AccesUtil();
                            DataTable ContrattoNuovo = new DataTable();
                            string contratto = string.Empty;
                            accgetnew.InitConnection(databases[i][3]);
                            ContrattoNuovo = accgetnew.ExtractValues(string.Format("Select NumeroRapportoNew, NumeroRapportoOld from buffer_conversione_rapporti where NumeroRapportoOld like \"%{0}\"", excelrow[1].ToString().Substring(excelrow[1].ToString().Length - 7, 7)));
                            if (ContrattoNuovo.Rows.Count > 0)
                            {
                                contratto = ContrattoNuovo.Rows[0][0].ToString();
                            }
                            accgetnew.CloseConnection();
                            DataTable accesnuovo = accobj.ExtractValues(string.Format("Select NumeroRapporto, DataChiusuraIncarico, PCSocieta, PC, DataProrogaRichiestaDaSocieta from Rapporti where NumeroRapporto='{0}'", contratto));
                            if (accesnuovo.Rows.Count > 0)
                            {
                                if (string.IsNullOrEmpty(accesnuovo.Rows[0][1].ToString()))
                                {
                                    DataTable statoPraticanuovo = accobj.ExtractValues(string.Format("Select stato from pratiche where cod_vel={0}", excelrow[0].ToString()));
                                    string notizia = string.Empty;
                                    if (statoPraticanuovo.Rows.Count > 0)
                                    {
                                        notizia = getNota(excelrow[2].ToString().Trim(), statoPraticanuovo.Rows[0][0].ToString());
                                    }
                                    else
                                    {
                                        notizia = getNota(excelrow[2].ToString().Trim(), string.Empty);
                                    }
                                    if (!string.IsNullOrEmpty(notizia))
                                    {

                                        if (accesnuovo.Rows[0][4].ToString().CompareTo(dataProroga.ToString()) != 0)
                                        {
                                            //MessageBox.Show(accesnuovo.Rows[0][0].ToString() + "  " + accesnuovo.Rows[0][2].ToString() + "  " + accesnuovo.Rows[0][3].ToString());
                                            //accobj.ExecuteNonQuery(string.Format("Update NotiziaIncaricoPC set LunghezzaNota='{0}', Nota='{1}' where PCSocieta='{2}' and PCCliente='{3}' and DataNotizia=date()", getNota(excelrow[3].ToString().Trim(), statoPratica.Rows[0][0].ToString()).Length.ToString("D3"), getNota(excelrow[3].ToString().Trim(), statoPratica.Rows[0][0].ToString()), accesrow[2].ToString(), accesrow[3].ToString()));
                                            if ((notizia == "220") || (notizia == "222") || (notizia == "568") || (notizia == "297"))
                                            {
                                                notizia = "il pdr e'' irregolare stiamo cercando rientrare dell''esposizione";
                                                accobj.ExecuteNonQuery(string.Format("Insert into NotiziaIncaricoPC (PCSocieta,PCCliente,DataNotizia,LunghezzaNota,Nota,Data_Ultima_Modifica_Soc,Flag_DS,Flag_Old_New) values ('{0}','{1}',date(),'{2}','{3}',date(),'{4}','{5}')", accesnuovo.Rows[0][2], accesnuovo.Rows[0][3], notizia.Length.ToString("D3"), notizia, "S", "NEW"));
                                                lavorate.ImportRow(excelrow);
                                            }
                                            else
                                            {
                                                accobj.ExecuteNonQuery(string.Format("Insert into Rapporti_Log Select * from Rapporti where NumeroRapporto='{0}'", accesnuovo.Rows[0][0].ToString()));
                                                accobj.ExecuteNonQuery(string.Format("Update Rapporti_Log set DataInserimentoGruppo=now() where NumeroRapporto='{0}' and DataProrogaRichiestaDaSocieta is null", accesnuovo.Rows[0][0].ToString()));
                                                accobj.ExecuteNonQuery(string.Format("Update Rapporti set Flag_DS='{0}', CodiceComunicazione='{1}', DataProrogaRichiestaDaSocieta=DateValue('{2}'), Data_Ultima_Modifica_Soc=date() where NumeroRapporto='{3}'", "S", "B", proroga, accesnuovo.Rows[0][0].ToString()));
                                                accobj.ExecuteNonQuery(string.Format("Insert into NotiziaIncaricoPC (PCSocieta,PCCliente,DataNotizia,LunghezzaNota,Nota,Data_Ultima_Modifica_Soc,Flag_DS,Flag_Old_New) values ('{0}','{1}',date(),'{2}','{3}',date(),'{4}','{5}')", accesnuovo.Rows[0][2], accesnuovo.Rows[0][3], notizia.Length.ToString("D3"), notizia, "S", "NEW"));
                                                lavorate.ImportRow(excelrow);
                                                //break;
                                            }
                                        }
                                        else
                                        {
                                            giaLavorate.ImportRow(excelrow);
                                        }
                                    }
                                    else
                                    {
                                        anomalies.ImportRow(excelrow);
                                    }
                                }
                                else
                                {
                                    nonlavorateChiuse.ImportRow(excelrow);
                                }
                            }
                            else
                            {
                                nonlavorateNonEsistenti.ImportRow(excelrow);
                            }
                        }
                    }
                    accobj.CloseConnection();
                }
                catch
                {
                    return "Error !!!" + findError;
                }

            }
            ExcelUtil.Export(@"D:\OCTAV\pentru svn\NET_AggiornaProrogheGeisp\anomalies\Anomalie.xlsx", anomalies);
            ExcelUtil.Export(@"D:\OCTAV\pentru svn\NET_AggiornaProrogheGeisp\anomalies\Lavorate.xlsx", lavorate);
            ExcelUtil.Export(@"D:\OCTAV\pentru svn\NET_AggiornaProrogheGeisp\anomalies\NonLavorateNonEsistono.xlsx", nonlavorateNonEsistenti);
            ExcelUtil.Export(@"D:\OCTAV\pentru svn\NET_AggiornaProrogheGeisp\anomalies\NonLavorateChiuse.xlsx", nonlavorateChiuse);
            ExcelUtil.Export(@"D:\OCTAV\pentru svn\NET_AggiornaProrogheGeisp\anomalies\GiaLavorate.xlsx", giaLavorate);
            return "Done !!!";

        }

        private static string getNota(string stato_agente, string stato_pratica)
        {
            string nota = string.Empty;
            switch (stato_agente)
            {
                case "214": nota = "confermiamo i dati anagrafici da voi forniti: il cliente non è stato contattato in quanto assente"; break;
                case "292": nota = "stiamo cercando di addivenire ad un accordo"; break;
                case "294": nota = "il cliente ha serie intenzioni di definire il suo insoluto"; break;
                case "295": nota = "stiamo effettuando dei controlli poiche all indirizzo fornito il cliente è irreperibile"; break;
                case "296": nota = "confermiamo i dati anagrafici da voi forniti: il cliente non è stato contattato in quanto assente"; break;
                case "298": nota = "confermiamo i dati anagrafici da voi forniti: abbiamo lasciato messaggio per essere ricontattati"; break;
                case "299": nota = "confermiamo i dati anagrafici da voi forniti: abbiamo lasciato messaggio per essere ricontattati"; break;
                case "300": nota = "esposta la situazione debioria, il clietne sta effettuando delle verifiche"; break;
                case "384": nota = "confermiamo i dati anagrafici da voi forniti: il cliente non è stato contattato in quanto assente"; break;
                case "293": nota = "esposta la situazione debioria, stiamo cercando di superare le contestazione avanzate dal cliente"; break;
                case "206": nota = "stiamo effettuando dei controlli poiche all indirizzo fornito il cliente è irreperibile "; break;
                case "451": nota = "il cliente richiede estratto conto della sua posizione"; break;
                case "205": nota = "il cliente promette di di definire il suo insoluto"; break;
                case "203": nota = "il cliente ha versato un acconto, stiamo cercando una soluzione per definire pdr"; break;
                case "448": nota = "stiamo cercando di addivenire ad un accordo"; break;
                case "488": nota = "il cliente ha serie intenzioni di definire il suo insoluto"; break;
                //case "297": nota = "il cliente ha versato un acconto, stiamo cercando una soluzione per definire pdr"; break;
                case "455": nota = "abbiamo ricevuto riscontro alla ns richiesta di documentazione, che abbiamo sottoposto al cliente"; break;
                case "631": nota = "confermiamo i dati anagrafici da voi forniti: il cliente sta verificando le sue possibilità"; break;
                case "661": nota = "stiamo effettuando dei controlli poiche all indirizzo fornito il cliente è irreperibile"; break;
                case "682": nota = "stiamo effettuando dei controlli poiche all indirizzo fornito il cliente è irreperibile"; break;
                case "683": nota = "stiamo effettuando dei controlli poiche all indirizzo fornito il cliente è irreperibile"; break;
                case "497": nota = "il cliente promette la definizione a breve. Si richiede proroga"; break;
                case "220": nota = "220"; break;
                case "222": nota = "222"; break;
                case "297": nota = "297"; break;
                default: break;
            }

            if (string.IsNullOrEmpty(nota))
            {
                switch (stato_pratica)
                {
                    case "313":
                        nota = "stiamo cercando di addivenire ad un accordo";
                        break;
                    case "351":
                        nota = "il cliente ha serie intenzioni di definire il suo insoluto";
                        break;
                    case "537":
                        nota = "confermiamo i dati anagrafici da voi forniti: il cliente non è stato contattato in quanto assente";
                        break;
                    case "206":
                        nota = "stiamo effettuando dei controlli poiche all indirizzo fornito il cliente è irreperibile";
                        break;
                    case "399":
                        nota = "il cliente ha serie intenzioni di definire il suo insoluto riformulando una nuova proposta";
                        break;
                    case "201":
                        nota = "esposta la situazione debioria, stiamo cercando di trovare una soluzione alla posizione del cliente";
                        break;
                    case "255":
                        nota = "esposta la situazione debioria, stiamo cercando di trovare una soluzione alla posizione del cliente";
                        break;
                    case "466":
                        nota = "il cliente ha serie intenzioni di definire il suo insoluto riformulando una nuova proposta";
                        break;
                    case "464":
                        nota = "A seguito di vs. risposta stiamo definendo gli accordi con il cliente. Richiediamo proroga.";
                        break;
                    case "383":
                        nota = "confermiamo i dati anagrafici da voi forniti: abbiamo lasciato messaggio per essere ricontattati";
                        break;
                    case "623":
                        nota = "confermiamo i dati anagrafici da voi forniti: abbiamo lasciato messaggio per essere ricontattati";
                        break;
                    case "350":
                        nota = "confermiamo i dati anagrafici da voi forniti: abbiamo lasciato messaggio per essere ricontattati";
                        break;
                    case "400":
                        nota = "il cliente ha serie intenzioni di definire il suo insoluto riformulando una nuova proposta";
                        break;
                    case "314":
                        nota = "abbiamo effettuato delle ricerche interne in quanto il clente non è reperibile all indirizzo fornitoci";
                        break;
                    case "218":
                        nota = "stiamo cercando di addivenire ad un accordo";
                        break;
                    case "461":
                        nota = "il cliente ha serie intenzioni di definire il suo insoluto";
                        break;
                    case "462":
                        nota = "il cliente ha serie intenzioni di definire il suo insoluto";
                        break;
                    case "186": nota = "siamo in attesa di ricevere vs. risposta alla proposta di rientro presentata"; break;
                    case "243": nota = "il cliente ha dato disposizione di bb permanente per il quale attendiamo conferma del pagamento"; break;
                    case "378": nota = "siamo in attesa di ricevere vs. risposta alla proposta di stralcio presentata"; break;
                    case "460": nota = "siamo in attesa di ricevere vs. risposta alla richiesta di verifica del pagamento"; break;
                    case "463": nota = "siamo in attesa di ricevere vs. risposta alla richiesta inoltrata"; break;
                    case "495": nota = "il cliente ha presentato richiesta di rinegoziazione. Si richiede proroga."; break;
                    case "496": nota = "a seguito dell''avvenuta rinegoziazione stiamo prendendo accordi. Si richiede proroga."; break;
                    case "516": nota = "siamo in attesa di ricevere vs. risposta alla richiesta di riaffidamento"; break;
                    case "520": nota = "a seguito del riaffidamento stiamo prendendo accordi con il cliente. Si richiede proroga."; break;
                    case "192": nota = "siamo in attesa di ricevere vs. risposta alla proposta di rientro presentata"; break;
                    case "259": nota = "il cliente ha dato disposizione di bb permanente per il quale attendiamo conferma del pagamento"; break;
                    case "384": nota = "abbiamo effettuato delle ricerche interne in quanto il clente non è reperibile all indirizzo fornitoci"; break;
                    case "300": nota = "esposta la situazione debioria, il clietne sta effettuando delle verifiche"; break;
                    case "465": nota = "A seguito di vs. risposta stiamo definendo gli accordi con il cliente. Richiediamo proroga."; break;
                    case "467": nota = "il cliente ha serie intenzioni di definire il suo insoluto"; break;
                    case "386": nota = "il cliente ha serie intenzioni di definire il suo insoluto riformulando una nuova proposta"; break;
                    case "923": nota = "confermiamo i dati anagrafici da voi forniti: abbiamo lasciato messaggio per essere ricontattati"; break;
                    case "203": nota = "il cliente ha versato un acconto, stiamo cercando una soluzione per definire pdr"; break;
                    case "220": nota = "220"; break;
                    case "222": nota = "222"; break;
                    case "568": nota = "568"; break;
                    case "297": nota = "297"; break;
                    default: break;
                }
            }
            return nota;
        }
    }
}

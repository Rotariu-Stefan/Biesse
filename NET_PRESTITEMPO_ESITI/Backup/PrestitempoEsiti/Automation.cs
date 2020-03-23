using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrestitempoInserimento.Utils;
using System.Data;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Net;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;


namespace PrestitempoInserimento
{
    public class Automation
    {
        public BackgroundWorker fatherWorker
        { get; set; }
        private CookieContainer _cookieJar;
        private string _dateTime;
        private string _password = string.Empty;
        private string _postData;
        private string _raspuns;
        private HttpWebRequest _request;
        private HttpWebResponse _response;
        private SQLUtil _sqlU;
        private string _timeStamp;
        private string _username = string.Empty;
        private string _windowID = string.Empty;
        private string valueContract;
        private int ContatoSenzaContor = 0;
        private int ContatoSenzaDescripzione = 1;
        private int NonVuolePagareContor = 0;
        private int NonVuolePagareDescripzione = 1;
        private System.Data.DataTable outcomes = new System.Data.DataTable();
        private System.Data.DataTable otherStato = new System.Data.DataTable();
        private System.Data.DataTable ctrNotOnSite = new System.Data.DataTable();
        private int i = 0;

        public Automation(string u, string p, string d,BackgroundWorker father)
        {
            this._username = u;
            this._password = p;
            this._dateTime = d;
            this.fatherWorker = father;
            InitOutcomesDataTable();
            InitOtherStatoDataTable();
            InitCtrNotOnSiteDataTable();
        }
        public void Run()
        {
            try
            {
                this._sqlU = new SQLUtil();
                if (this._sqlU.InitConnection(ConfigUtils.GetSQLServer(), ConfigUtils.GetSQLUser(), ConfigUtils.GetSQLPassword(), ConfigUtils.GetSQLDataBase(), ConfigUtils.GetTrustedConnection().ToString().ToLower()) == "OK")
                {
                    LoginAndGoToGestioneMandati();

                    if (SalvaPagamentoParziale() == "Cancel") { return; }
                    if (SalvaRicevutoPagamento() == "Cancel") { return; }
                    if (SalvaTheOther() == "Cancel") { return; }

                    string excelFile = MakeLavoroExcel();
                    var files = new List<string>();
                    files.Add(excelFile);
                    SendMail(files);
                   
                }
                else
                {
                    Logger.Log("non poteva connettersi al database", LogType.Error);
                    MailUtils.Send("Non poteva connettersi al database.",
                                                "PRESTITEMPO ESITI: errori nell’inserimento degli esiti su sito committente",
                                                new List<string>(),
                                                ConfigUtils.GetCaricoAddress(), ConfigUtils.GetPostAddress());
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
                this._sqlU.CloseConnection();

               // Console.WriteLine("Exception");
            }
            finally
            {
                this._sqlU.CloseConnection();
              //  Console.WriteLine("finish");
            }
        }

        private void SendMail(List<string> files)
        {
            try
            {
                MailUtils.Send("PRESTITEMPO ESITI: pratiche lavorate su sito committente",
                    "PRESTITEMPO ESITI: pratiche lavorate su sito committente",
                    files,
                    ConfigUtils.GetCaricoAddress(), ConfigUtils.GetPostAddress());
            }
            catch
            {
                try
                {
                    MailUtils.Send("IImpossibile inviare la posta. È possibile trovare il file excel all'indirizzo: " + files.FirstOrDefault(),
                                        "PRESTITEMPO ESITI: errori nell’inserimento degli esiti su sito committente",
                                        new List<string>(),
                                        ConfigUtils.GetCaricoAddress(), ConfigUtils.GetPostAddress());
                }
                catch { }
                Logger.Log("Impossibile inviare la posta. È possibile che le credenziali di posta non sono buone. È possibile trovare il file excel all'indirizzo: " + files.FirstOrDefault(), LogType.Error);
            }
        }
        private string GetIdEsitoRilevamento(string esito)
        {
            switch (esito)
            { 
                case "Pagamento Parziale" :
                    return "22";
                case "Ricevuto Pagamento":
                    return "24";
                case "Promessa Pagamento":
                    return "03";
                case "Contatto senza esito":
                    return "20";
                case "Non vuole pagare":
                    return "02";
                default: return "";


            }
        }
        private void UpdateEsitazione(string cod_vel, string esito_rilevamento)
        {
            var sp = new SQLUtil.StoredProcedure("AggiornaEsitazioneDiPrestitemo_4090");
            sp.AddParameter(new SQLUtil.Parameter("COD_VEL", cod_vel));
            sp.AddParameter(new SQLUtil.Parameter("ESITO_RILEVAMENTO", esito_rilevamento));
            _sqlU.ExecStoredProcedure(sp);
        }

        private void LoginAndGoToGestioneMandati()
        {
            this._raspuns = this.LoginStep();

            if (this._raspuns.Contains("reject.html"))
            {
                MailUtils.Send("Impossibile connettersi o loggarsi sul sito web.", "PRESTITEMPO ESITI: errori nell’inserimento degli esiti su sito committente", null, ConfigUtils.GetCaricoAddress(), ConfigUtils.GetPostAddress());
                   Logger.Log("Credenziali errate", LogType.Error);
            }
            else if (this._raspuns.Contains("WEBarte/ArTe.jsp"))
            {
                this._raspuns = this.LoginSuccedStep1();
                this._raspuns = this.LoginSuccedStep2();
                this._raspuns = this.LoginSuccedStep3();
                this._raspuns = this.GoToGestioneMandati();
            }
        }

        private string MakeLavoroExcel()
        {
            var dataTables = new List<System.Data.DataTable>();
            var sheetsNames = new List<string>();   

            
            dataTables.Add(outcomes);
            sheetsNames.Add("Pratiche lavorate");
            dataTables.Add(otherStato);
            sheetsNames.Add("Pratiche non lavorate");
            dataTables.Add(ctrNotOnSite);
            sheetsNames.Add("CONTRATTI NON PRESENTE SU SITO");
            string date = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string outcomesPath = ExcelUtils.WriteExcel(date + "_PrestitempoLavorate_01", dataTables, sheetsNames);
            return outcomesPath;
        }

        private void InitOutcomesDataTable()
        {
            outcomes.Columns.Add("COD_VEL");
            outcomes.Columns.Add("CONTRATTO");
            outcomes.Columns.Add("NOME DEBITORE");
            outcomes.Columns.Add("ESITO RILEVAMENTO");
            outcomes.Columns.Add("STATO_AGENTE");
            outcomes.Columns.Add("DESCRIZIONE RILEVAMENTO");
        }
        private void InitOtherStatoDataTable()
        {
            otherStato.Columns.Add("COD_VEL");
            otherStato.Columns.Add("CONTRATTO");
            otherStato.Columns.Add("NOME DEBITORE");
            otherStato.Columns.Add("ESITO AGENTE");
        }
        private void InitCtrNotOnSiteDataTable()
        {
            ctrNotOnSite.Columns.Add("COD_VEL");
            ctrNotOnSite.Columns.Add("CONTRATTO");
            ctrNotOnSite.Columns.Add("NOME DEBITORE");
        
        }

       
        private string SalvaPagamentoParziale() 
        {
            string query = "SELECT P.COD_VEL,P.CONTRATTO, D.NOM_DEB FROM PRATICHE P " +
                            "JOIN DEBITORI D " +
                            "ON P.COD_DEB = D.COD_DEB " +
                            "WHERE P.COD_BAS=32 and (P.COD_GRP=76 OR P.COD_GRP=77 OR P.COD_GRP=88 OR P.COD_GRP=89 OR P.COD_GRP=90 OR P.COD_GRP=91) " +
                            "AND P.CAP_REC_EURO>0 and P.CAP_REC_EURO<P.CAP_AFF_EURO AND P.CHIUSA IS NULL ";

            foreach (DataRow r in this._sqlU.ExtractValues(query).Rows)
            {
                this.valueContract = r[1].ToString();
                this._raspuns = this.GotoSpecificValue(this.valueContract);
                string[] addwidgS = this._raspuns.Split(new string[] { "addWidget" }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;

                foreach (string s in addwidgS)
                {
                    if (s.Contains(this.valueContract))
                    {
                        count++;
                        if (count == 2)
                        {
                            break;
                        }
                    }
                }
                if (count >= 2)
                {
                    this._raspuns = this.GoToVisualizza();
                    SalvaOnSite("Pagamento Parziale");
                    UpdateEsitazione(r[0].ToString(), "Pagamento Parziale");
                    outcomes.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), "Pagamento Parziale", "", "");
                    this._raspuns = this.GoToIndietro();
                }
                else
                {
                    ctrNotOnSite.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString());
                }
                if (fatherWorker.CancellationPending == true)
                {
                    string excelFile = MakeLavoroExcel();
                    var files = new List<string>();
                    files.Add(excelFile);
                    SendMail(files);
                    return "Cancel";
                }
            }
            return "Finished";
        }
        private string SalvaRicevutoPagamento()
        {
            string query = "SELECT P.COD_VEL,P.CONTRATTO, D.NOM_DEB FROM PRATICHE P " +
                            "JOIN DEBITORI D " +
                            "ON P.COD_DEB = D.COD_DEB " +
                            "WHERE P.COD_BAS=32 and (P.COD_GRP=76 OR P.COD_GRP=77 OR P.COD_GRP=88 OR P.COD_GRP=89 OR P.COD_GRP=90 OR P.COD_GRP=91) " +
                            "AND CAP_REC_EURO>=CAP_AFF_EURO AND P.CHIUSA IS NULL";
            foreach (DataRow r in this._sqlU.ExtractValues(query).Rows)
            {
                this.valueContract = r[1].ToString();
                this._raspuns = this.GotoSpecificValue(this.valueContract);
                string[] addwidgS = this._raspuns.Split(new string[] { "addWidget" }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;
                foreach (string s in addwidgS)
                {
                    if (s.Contains(this.valueContract))
                    {
                        count++;
                        if (count == 2)
                        {
                            break;
                        }
                    }
                }
                if (count >= 2)
                {
                    this._raspuns = this.GoToVisualizza();
                    SalvaOnSite("Ricevuto Pagamento");
                    UpdateEsitazione(r[0].ToString(), "Ricevuto Pagamento");
                    outcomes.Rows.Add(r[0].ToString(),r[1].ToString(),r[2].ToString(), "Ricevuto Pagamento","", "");
                    this._raspuns = this.GoToIndietro();

                }
                else
                {
                    ctrNotOnSite.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString());
                }
                if (fatherWorker.CancellationPending == true)
                {
                    string excelFile = MakeLavoroExcel();
                    var files = new List<string>();
                    files.Add(excelFile);
                    SendMail(files);
                    return "Cancel";
                }
            }
            return "Finished";       
        }
        private string SalvaTheOther()
        {
            string query = "SELECT DISTINCT P.COD_VEL, P.CONTRATTO, D.NOM_DEB, M.STATO_AGENTE "+
				            "FROM PRATICHE P JOIN DEBITORI D ON P.COD_DEB = D.COD_DEB "+
                                "LEFT OUTER JOIN MANDATI M ON P.COD_VEL = M.COD_VEL "+
                                "WHERE P.COD_BAS=32 and (P.COD_GRP=76 OR P.COD_GRP=77 OR P.COD_GRP=88 OR P.COD_GRP=89 OR P.COD_GRP=90 OR P.COD_GRP=91) " +
								"AND P.CAP_REC_EURO=0 AND P.CHIUSA IS NULL "+
								"AND (M.DATA_INIZIO = (SELECT MAX(M1.DATA_INIZIO) FROM MANDATI M1 "+
						                              "  WHERE M1.COD_VEL = M.COD_VEL "+
                                                            "AND (M1.TIP_AGENTE<>'P') " +
															"GROUP BY M1.COD_VEL ) "+
						              "OR M.COD_VEL IS NULL)";
            foreach (DataRow r in this._sqlU.ExtractValues(query).Rows)
            {

                if (i % 101 == 0)
                {
                    LoginAndGoToGestioneMandati();
                }
                i++;

                this.valueContract = r[1].ToString();
                this._raspuns = this.GotoSpecificValue(this.valueContract);
                string[] addwidgS = this._raspuns.Split(new string[] { "addWidget" }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;
                foreach (string s in addwidgS)
                {
                    if (s.Contains(this.valueContract))
                    {
                        count++;
                        if (count == 2)
                        {
                            break;
                        }
                    }
                }
                if (count >= 2)
                {
                    this._raspuns = this.GoToVisualizza();
                    string esitoRilevamento = GetEsitoRilevamento(r[3].ToString().Trim());
                    string descrizione = GetDescrizione(r[3].ToString().Trim());
                    if (descrizione != "")
                    {
                        //aici
                        SalvaOnSite(esitoRilevamento, descrizione);
                        UpdateEsitazione(r[0].ToString(),esitoRilevamento);
                        outcomes.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), esitoRilevamento,r[3].ToString(), descrizione);
                    }
                    else
                    {
                        string stato_agente = r[3].ToString().Trim();
                        if (stato_agente == "")
                        {
                            otherStato.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), "ASSENTE");
                        }
                        else
                        {
                            otherStato.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString());
                        }
                    }

                    //SALVEAZA

                    this._raspuns = this.GoToIndietro();

                }
                else
                {
                    ctrNotOnSite.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString());
                }
                if (fatherWorker.CancellationPending == true) 
                {
                    string excelFile = MakeLavoroExcel();
                    var files = new List<string>();
                    files.Add(excelFile);
                    SendMail(files);
                    return "Cancel";
                }
            }
            return "Finished";              
        
        }

        
        private string SalvaOnSite(string esito)
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
              //  this._postData = "ATDataTransferField=ActionArea%3Dtrue%25A7true%25A7true%25A7true%25A7true%25A7true%25A7true%25A7false%25A7true%25A7true%25A7%26RisultatoRicerca%3Dtrue%25A7false%25A7%25A70%25A70&ATUserEvent=Visualizza&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                this._postData = "ATDataTransferField=Rilevamento%3Dtrue%25A7true%25A7false%25A7true%25A7false%25A7%25A7%25A7%25A7" + GetIdEsitoRilevamento(esito) + "%26NewToponimo%3Dtrue%25A7true%25A7true%25A7true%25A7true%25A7%25A7%25A7%25A7&ATUserEvent=Salva&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }
        private string SalvaOnSite(string esito, string descrizione)
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
                string d = descrizione.Replace(" ", "%2520");
                this._postData = "ATDataTransferField=DMMandatoInput%3Dtrue%26Rilevamento%3Dtrue%25A7true%25A7false%25A7true%25A7false%25A7%25A7%25A7%25A7" + GetIdEsitoRilevamento(esito) + "%26RilevamentoDesc%3Dtrue%25A7true%25A7false%25A7true%25A7false%25A7" +
                            d + "%26NewToponimo%3Dtrue%25A7true%25A7true%25A7true%25A7true%25A7%25A7%25A7%25A7%26DMMandatoIntestazione%3Dfalse%26currPage%3Dfalse%25A7true%25A7false%25A7true%25A7false%25A72%26ImDM0On%3Dfalse%25A7true%25A7dm%2Fa%2FMandato%2FImDM0On.gif%26ImDM2On%3Dtrue%25A7true%25A7dm%2Fa%2FMandato%2FImDM2On.gif&ATUserEvent=Salva&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }

        private string GetEsitoRilevamento(string stato_agente)
        {
            switch (stato_agente)
            {
                //SalvaPagamentoParziale
                case "203":    //Acconto
                    return "Promessa Pagamento";
                case "202":     //Recuperata 
                    return "Promessa Pagamento";
                case "294":     //Promessa di Pagamento
                    return "Promessa Pagamento";
                case "222":     //Piano Dil.Pass.Mens. 
                    return "Promessa Pagamento";

                //Contatto senza esito
                case "214":     //Assente
                    return "Contatto senza esito";
                case "296":     //Assente provv.
                    return "Contatto senza esito";
                case "298":     //Lasciato Messaggio
                    return "Contatto senza esito";
                case "295":     //Irreperibile provv. 
                    return "Contatto senza esito";
                case "682":     //Debitore si rende irreperibile  
                    return "Contatto senza esito";
                case "299":     //Richiamare in giornata
                    return "Contatto senza esito";

                //Non vuole pagare
                case "205":     //Rifiuto pagamento
                    return "Non vuole pagare";
                case "292":     //Rifiuto pag. provv.
                    return "Non vuole pagare";

                default:
                    return "";
            }
        }
        private string GetDescrizione(string stato_agente)
        {
            switch (stato_agente) 
            { 
                //SalvaPagamentoParziale
                case "203" :    //Acconto
                    return "IL CLIENTE HA PROMESSO DI EFFETTUARE UN PAGAMENTO IN ACCONTO SULLA POSIZIONE. ATTENDIAMO A BREVE COPIA DELL’AVVENUTO PAGAMENTO.";
                case "202":     //Recuperata 
                    return "IL CLIENTE HA PROMESSO DI EFFETTUARE UN PAGAMENTO A COPERTURA DELL’INSOLUTO IN NOSTRO AFFIDAMENTO. ATTENDIAMO A BREVE COPIA DELL’AVVENUTO PAGAMENTO.";
                case "294":     //Promessa di Pagamento
                    return "IL CLIENTE HA PROMESSO DI EFFETTUARE UN PAGAMENTO IN ACCONTO SULLA POSIZIONE. ATTENDIAMO A BREVE COPIA DELL’AVVENUTO PAGAMENTO.";
                case "222":     //Piano Dil.Pass.Mens. 
                    return "IL CLIENTE E’ IN DIFFICOLTA’ ECONOMICHE PER CUI HA PROMESSO DI EFFETTUARE IL PAGAMENTO DELL’INSOLUTO VERSANDO PICCOLI ACCONTI ENTRO IL NOSTRO MANDATO. ATTENDIAMO A BREVE COPIA DELL’AVVENUTO PAGAMENTO.";

                //Contatto senza esito
                case "214":     //Assente
                    return GetContatoSenzaDescrizioneForContor();
                case "296":     //Assente provv.
                    return GetContatoSenzaDescrizioneForContor();
                case "298":     //Lasciato Messaggio
                    return GetContatoSenzaDescrizioneForContor();
                case "295":     //Irreperibile provv. 
                    return GetContatoSenzaDescrizioneForContor();
                case "682":     //Debitore si rende irreperibile  
                    return GetContatoSenzaDescrizioneForContor();
                case "299":     //Richiamare in giornata
                    return GetContatoSenzaDescrizioneForContor();

                //Non vuole pagare
                case "205":     //Rifiuto pagamento
                    return GetNonVuolePagareDescrizioneForContor();
                case "292":     //Rifiuto pag. provv.
                    return GetNonVuolePagareDescrizioneForContor();

                default:
                    return "";
            }
        }
                
        private string GetContatoSenzaDescrizione(int desc)
        {
            switch (desc)
            { 
                case 1:
                    return "IL CLIENTE E' RISULTATO SEMPRE ASSENTE AI NOSTRI PASSAGGI IN LOCO NE' SIAMO STATI RICONTATTATI IN RISPOSTA AI VARI AVVISI CARTACEI LASCIATI IN POSTA.";
                case 2:
                    return "CI SIAMO RECATI SUL POSTO MA IL CLIENTE NON E' MAI RISULTATO PRESENTE. LASCIATI AVVISI CARTACEI MA NON SIAMO STATI RICONTATTATI.";
                case 3:
                    return "ABBIAMO TENTATO IN OGNI MODO DI CONFERIRE CON IL RESPONSABILE DELLA POSIZIONE DEBITORIA MA LO STESSO E' RISULTATO SEMPRE ASSENTE SIA ALLA NOSTRA ESAZIONE DOMICILIARE SIA AI TENTATIVI DI CONTATTO TELEFONICO.";
                case 4:
                    return "I NOSTRI TENTATIVI DI CONFERIRE CON IL DEBITORE HANNO TUTTI DATO ESITO NEGATIVO. LA STESSA E' RISULTATA ASSENTE IN LOCO E AL TELEFONO NON HA MAI RISPOSTO PUR AVENDO EFFETTUATO RIPETUTI TENTATIVI IN GIORNI ED ORARI DIFFERENTI.";
                case 5:
                    return "NONOSTANTE L'UTILIZZO DI TUTTI I MEZZI E TUTTE LE INFORMAZIONI A NOSTRA DISPOSIZIONE NON CI E' STATO POSSIBILE CONFERIRE CON L'OBBLIGATA LA QUALE E' RISULTATA ASSENTE A TUTTI I NOSTRI TENTATIVI DI RINTRACCIO.";
                case 6:
                    return "IL NOSTRO AGENTE ESTERNO HA EFFETTUATO SOPRALLUOGHI IN GIORNI ED ORARI DIFFERENTI. IL CLIENTE R' RISULTATO SEMPRE ASSENTE. LASCIATI AVVISI CARTACEI MA IL CLIENTE NON CI HA RICONTATTATI. TENTATO CONTATTO TELEFONICO MA ANCH'ESSO CON ESITO NEGATIVO.";
                case 7:
                    return "IL NOSTRO AGENTE ESTERNO HA EFFETTUATO NUMEROSI SOPRALLUOGHI MA IL DEBITORE NON E' RISULTATO MAI PRESENTE AI VARI PASSAGGI. ABBIAMO LASCIATO AVVISI CARTACEI IN POSTA MA NON SIAMO STATI RICONTATTATI. AL NUMERO TELEFONICO NON HA MAI RISPOSTO NESSUNO";
                case 8:
                    return "IL NOSTRO AGENTE ESTERNO SI E' RECATO AL DOMICILIO IN GIORNI ED ORARI DIFFERENTI. IL CLIENTE E' PERO' RISULTATO SEMPRE ASSENTE. GLI AVVISI CARTACEI LASCIATI IN POSTA NON HANNO SORTITO ALCUN EFFETTO. FATTE RICERCHE INTERNE PER REPERIRE UTENZE UTILI AL CONTATTO MA SENZA ESITO.";
                case 9:
                    return "IL CLIENTE E' RISULTATO SEMPRE ASSENTE AL DOMICILIO E NON CI HA RICONTATTATO IN RISPOSTA AGLI AVVISI CARTACEI LASCIATI IN POSTA. AL RECAPITO IN NOSTRO POSSESSO SI AVVIA SEMPRE LA SEGRETERIA SU CUI ABBIAMO LASCIATO MOLTEPLICI MESSAGGI AI QUALI NON ABBIAMO RICEVUTO ALCUN TIPO DI RISPOSTA DAL CLIENTE.";
                case 10:
                    return "ALL'INDIRIZZO LA CLIENTE E' RISULTATA SEMPRE ASSENTE. CHIESTE INFORMAZIONI AI VICINI MA NESSUNO HA SAPUTO FORNIRCI UTILI INDICAZIONI. ABBIAMO LASCIATO AVVISI CARTACEI IN POSTA MA NON SIAMO STATI RICONTATTATI. FATTE RICERCHE PER REPERIRE UN RECAPITO VALIDO PRESSO CUI CONTATTARE LA CLIENTE MA CON ESITO NEGATIVO.";
                case 11:
                    return "IL NOSTRO AGENTE ESTERNO SI E' RECATO SUL POSTO NUMEROSE VOLTE, MA PUR AVENDO EFFETTUATO PASSAGGI IN GIORNI ED ORARI DIFFERENTI NON E' RIUSCITO A CONFERIRE CON L'OBBLIGATO RISULTANDO QUESTO ASSENTE NELLE VARIE OCCASIONI.";
                default:
                    return "";

            }
        }
        private string GetContatoSenzaDescrizioneForContor()
        {
            ContatoSenzaContor++;
            if (ContatoSenzaContor == 6)
            {
                ContatoSenzaContor = 1;
                ContatoSenzaDescripzione++;
                if (ContatoSenzaDescripzione == 12)
                {
                    ContatoSenzaDescripzione = 1;
                }
            }
            return GetContatoSenzaDescrizione(ContatoSenzaDescripzione);
        }
        
        private string GetNonVuolePagareDescrizione(int desc) 
        {
            switch (desc)
            {
                case 1:
                    return "CI SIAMO RECATI IN LOCO E CONFERITO CON IL CLIENTE, IL QUALE INIZIALMENTE SEMBRAVA DISPONIBILE AL PAGAMENTO, MA IN SEGUITO LO STESSO NON HA SALDATO LA POSIZIONE E SI E' INOLTRE RESO NON REPERIBILE AI NOSTRI TENTATIVI DI CONTATTO.";
                case 2:
                    return "CI SIAMO RECATI IN LOCO ED ABBIAMO CONFERITO CON IL CLIENTE, IL QUALE HA RIFIUTATO IL PAGAMENTO, ASSERENDO DI AVERE GRAVI DIFFICOLTA' ECONOMICHE. INUTILI I NOSTRI SOLLECITI, IL CLIENTE RIBADISCE L'IMPOSSIBILITA' AL PAGAMENTO.";
                case 3:
                    return "CI SIAMO RECATI SUL POSTO E CONFERITO CON IL CLIENTE, IL QUALE HA RIFIUTATO IL PAGAMENTO ASSERENDO CHE AL MOMENTO NON PUO' SOSTENERE ALCUN VERSAMENTO A CAUSA DI DIFFICOLTA' ECONOMICHE.";
                case 4:
                    return "CI SIAMO RECATI IN LOCO ED ABBIAMO CONFERITO CON IL CLIENTE. QUEST'ULTIMO, SOLLECITATO AL PAGAMENTO, HA LAMENTATO GRAVI DIFFICOLTA' ECONOMICHE PER LE QUALI SI DICHIARA IMPOSSIBILITATO AD EFFETTUARE ALCUN VERSAMENTO.";
                case 5:
                    return "CI SIAMO RECATI SUL POSTO E CONFERITO CON IL CLIENTE, IL QUALE HA DICHIARATO CHE AL MOMENTO NON PUO' IN ALCUN MODO FAR FRONTE AL PAGAMENTO DI QUANTO INSOLUTO, POICHE' VERSA IN GRAVI DIFFICOLTA' ECONOMICHE.";
                case 6:
                    return "ABBIAMO CONFERITO CON IL CLIENTE, IL QUALE HA FIN DA SUBITO DIMOSTRATO VOLONTA' NEL SANARE LA POSIZIONE, MA HA LAMENTATO GRAVI DIFFICOLTA' ECONOMICHE. ABBIAMO PIU' VOLTE SOLLECITATO LO STESSO, MA IL RISULTATO E' STATO SEMPRE LO STESSO.";
                case 7:
                    return "ABBIAMO CONFERITO CON IL DEBITRICE IL QUALE PROMETTEVA IL PAGAMENTO DI QUANTO DOVUTO ENTRO BREVE TEMPO. SOLLECITATO PIU’ VOLTE INFINE SI E' RESA ASSENTE.";
                case 8:
                    return "IL NOSTRO FUNZIONARIO HA CONFERITO CON IL CLIENTE VARI VOLTE IN GIORNI ED ORARI DIVERSI MA LO STESSO RIFERISCE DI NON RIUSCIRE A RIENTRARE DEL DEBITO NONOSTANTE LE VARIE FORME DI RIENTRO PROSPETTATE.";
                default:
                    return "";

            }
        }
        private string GetNonVuolePagareDescrizioneForContor()
        {
            NonVuolePagareContor++;
            if (NonVuolePagareContor == 6)
            {
                NonVuolePagareContor = 1;
                NonVuolePagareDescripzione++;
                if (NonVuolePagareDescripzione == 9)
                {
                    NonVuolePagareDescripzione = 1;
                }
            }
            return GetNonVuolePagareDescrizione(NonVuolePagareDescripzione);
        }

        
        private string GotoSpecificValue(string value)
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
                this._postData = "ATDataTransferField=NumeroContratto%3Dtrue%25A7true%25A7false%25A7true%25A7false%25A7" + value + "&ATUserEvent=Cerca&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }
        private string GoToGestioneMandati()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;

                this._postData = "ATDataTransferField=MenuConversazioni%3Dtrue%25A7false%25A7%25BFGestione%2520Mandati&ATUserEvent=Launch&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }
        private string GoToVisualizza()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
                this._postData = "ATDataTransferField=ActionArea%3Dtrue%25A7true%25A7true%25A7true%25A7true%25A7true%25A7true%25A7false%25A7true%25A7true%25A7%26RisultatoRicerca%3Dtrue%25A7false%25A7%25A70%25A70&ATUserEvent=Visualizza&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }

        private string GoToIndietro()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
                this._postData = "ATDataTransferField=NewToponimo%3Dtrue%25A7true%25A7true%25A7true%25A7true%25A7%25A7%25A7%25A7&ATUserEvent=Cancel&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }
        private string GoToIndietro2()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection whc = new WebHeaderCollection();
                whc.Add("Accept-Encoding", "gzip,defalte");
                whc.Add("Accept-Language", "en-us");
                whc.Add("Cache-Control", "no-cache");
                this._request.Headers = whc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.ContentType = "application/x-www-form-urlencoded";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
                this._postData = "ATDataTransferField=&ATUserEvent=Cancel&ATUserEventType=ACT&ATCurrentPageTimestamp=" + this._timeStamp + "&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=0";
                byte[] data = Encoding.ASCII.GetBytes(this._postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = UnZip(this._response.GetResponseStream());
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return ret;
        }

        private string LoginStep()
        {
            string ret = string.Empty;
            try
            {
                this._cookieJar = new CookieContainer();
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/siteminderagent/forms/login.fcc");
                this._request.AllowAutoRedirect = false;
                this._request.ServicePoint.Expect100Continue = false;
                this._request.Method = "POST";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection wc = new WebHeaderCollection();
                wc.Add("Accept-Language", "en-us");
                wc.Add("Accept-Encoding", "gzip,deflate");
                wc.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                this._request.Headers = wc;
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                this._request.KeepAlive = true;
                this._request.Accept = "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, */*";
                this._request.ContentType = "application/x-www-form-urlencoded";
                string postData = "USER=" + this._username + "&PASSWORD=" + this._password + "&target=%2FWEBarte%2FArTe.jsp&smauthreason=0";
                byte[] data = Encoding.ASCII.GetBytes(postData);
                Stream streamData = this._request.GetRequestStream();
                streamData.Write(data, 0, data.Length);
                streamData.Flush();
                streamData.Close();
                this._response = (HttpWebResponse)this._request.GetResponse();
                ret = new StreamReader(this._response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }
        public string LoginSuccedStep1()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/ArTe.jsp");
                this._request.AllowAutoRedirect = true;
                this._request.Method = "GET";
                this._request.CookieContainer = this._cookieJar;
                WebHeaderCollection wc = new WebHeaderCollection();
                wc.Add("Accept-Language", "en-us");
                wc.Add("Accept-Encoding", "gzip,deflate");
                wc.Add("Cache-Control", "no-cache");
                this._request.Headers = wc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.KeepAlive = true;
                this._request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                this._response = (HttpWebResponse)this._request.GetResponse();
                Stream str = this._response.GetResponseStream();
                new StreamReader(str);
                ret = UnZip(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }
        public string LoginSuccedStep2()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR");
                this._request.Method = "GET";
                this._request.AllowAutoRedirect = true;
                this._request.CookieContainer = this._cookieJar;
                this._request.KeepAlive = true;
                WebHeaderCollection wc = new WebHeaderCollection();
                wc.Add("Accept-Encoding", "gzip,deflate");
                wc.Add("Accept-Language", "en-us");
                this._request.Headers = wc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe.jsp";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                this._response = (HttpWebResponse)this._request.GetResponse();
                Stream str = this._response.GetResponseStream();
                new StreamReader(str);
                ret = UnZip(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }
        public string LoginSuccedStep3()
        {
            string ret = string.Empty;
            try
            {
                this._request = null;
                TimeSpan ts = (TimeSpan)(DateTime.Now - DateTime.ParseExact("01/01/1970", "dd/MM/yyyy", CultureInfo.InvariantCulture));
                this._windowID = ts.TotalMilliseconds.ToString().Contains(".") ? ts.TotalMilliseconds.ToString().Substring(0, ts.TotalMilliseconds.ToString().IndexOf(".")) : ts.TotalMilliseconds.ToString();
                this._request = (HttpWebRequest)WebRequest.Create("https://www.new-pronto.prestitempo.it/WEBarte/servlet/com.accenture.atservices.servlets.ATDispatcher?ATDataTransferField=&ATUserEvent=&ATUserEventType=&ATCurrentPageTimestamp=&Browser=ie4&Attributes=true&WindowID=" + this._windowID + "&ATDisplayCurrency=");
                this._request.Method = "GET";
                this._request.AllowAutoRedirect = true;
                this._request.CookieContainer = this._cookieJar;
                this._request.KeepAlive = true;
                WebHeaderCollection wc = new WebHeaderCollection();
                wc.Add("Accept-Encoding", "gzip,deflate");
                wc.Add("Accept-Language", "en-us");
                this._request.Headers = wc;
                this._request.Referer = "https://www.new-pronto.prestitempo.it/WEBarte/ArTe/jsp/ie4/ATServices.jsp?webappPath=/WEBarte/&screenResolution=HR";
                this._request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; InfoPath.3; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                this._request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                this._response = (HttpWebResponse)this._request.GetResponse();
                Stream str = this._response.GetResponseStream();
                new StreamReader(str);
                ret = UnZip(str);
                this._timeStamp = this.GetTimeStamp(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private static string UnZip(Stream str)
        {
            StringBuilder sB = new StringBuilder("");
            try
            {
                byte[] byteArray = new byte[0x989680];
                GZipStream sr = new GZipStream(str, CompressionMode.Decompress);
                int rByte = 0;
                try
                {
                    rByte = sr.Read(byteArray, 0, byteArray.Length - 1);
                }
                catch (Exception gzError)
                {
                    throw gzError;
                }
                sB = new StringBuilder(rByte);
                for (int i = 0; i < rByte; i++)
                {
                    sB.Append((char)byteArray[i]);
                }
                sr.Close();
                sr.Dispose();
                str.Close();
                str.Dispose();
            }
            catch
            {
            }
            return sB.ToString();
        }
        private string GetTimeStamp(string response)
        {
            string ret = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(response))
                {
                    int start = response.IndexOf("ATTStamp=\"") + "ATTStamp=\"".Length;
                    ret = response.Substring(start, response.Substring(start, 100).IndexOf("\";"));
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Time Stamp extract failed! Reason: " + ex.Message, LogType.Error);
            }
            return ret;
        }
        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}

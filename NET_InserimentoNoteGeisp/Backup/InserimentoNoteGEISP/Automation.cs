using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InserimentoNoteGEISP.Utils;
using System.Data;
using System.IO;
using InserimentoNoteGEISP.GEISP;
namespace InserimentoNoteGEISP
{
    public class Automation
    {
        private OleDbUtil oledb;
        private SQLUtil sql;
        private DataTable report;
        private DataTable NonTrovati;
        public void Run()
        {
            report = new DataTable();
            NonTrovati = new DataTable();
            this.oledb = new OleDbUtil();
            this.sql = new SQLUtil();
            string output = string.Empty;
            string query = string.Empty;
            string nota = string.Empty;

            InitReportDataTable();

            List<GeispDatabase> databases = GeispUtils.getDatabases();

            foreach (var db in databases)
            {
                output = this.oledb.InitConnection(db.path);
                output = this.sql.InitConnection(ConfigUtils.GetSQLServer(), db.societatId, ConfigUtils.GetSQLUser(), ConfigUtils.GetSQLPassword(), ConfigUtils.GetTrustedConnection().ToString());


                query = string.Format("SELECT PAGAMENTI.BASE, PRATICHE.CONTRATTO, DEBITORI.NOM_DEB, PRATICHE.COD_VEL,"
                        + " PAGAMENTI.IMPORTO_EURO,ISNULL(PAGAMENTI.DATAVERA, PAGAMENTI.DATA2) as 'data di valuta del titolo',"
                        + " PAGAMENTI.ESTREMI, PAGAMENTI.DATA_DISTINTA_INCASSI, PAGAMENTI.COD_INT from PAGAMENTI" 
                        + " inner join PRATICHE on PRATICHE.COD_VEL = PAGAMENTI.COD_VEL"
                        + " inner join DEBITORI on DEBITORI.COD_DEB = PRATICHE.COD_DEB"
                        + " where CONVERT(nvarchar(10),PAGAMENTI.DATA_DISTINTA_INCASSI,105) = CONVERT(nvarchar(10), GETDATE(), 105)"
                        + " and PAGAMENTI.IMPORTO_EURO > 0  and PAGAMENTI.COD_INT < 500"
                        + " and NOT EXISTS (select 1 from PAGAMENTI P where P.COD_INT  =  PAGAMENTI.COD_INT + 500  and p.COD_VEL = PAGAMENTI.COD_VEL)"
                        + " and PRATICHE.COD_BAS in ({0})", db.codBas);

                Logger.Log("Estrazione dei pagamenti per il database GEISP (" + db.name + ") per la prima procedura", LogType.Operation);
                DataTable dtPagamenti = new DataTable();
                if (this.sql.Connection.State.ToString().ToUpper() == "OPEN")
                {
                    try
                    {
                        dtPagamenti = this.sql.ExtractValues(query);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LogType.Error);
                    }
                }
                nota = string.Empty;
                Logger.Log(string.Concat(new object[] { "Trovati ", dtPagamenti.Rows.Count.ToString(), " pagamenti sul database GALAXIA (", db.societatId, " cod_base ", db.codBas, ")" }), LogType.Operation);
                foreach (DataRow dr in dtPagamenti.Rows)
                {

                    string nrRapporto = this.determineNrRaport(dr.ItemArray[1].ToString());
                    if (nrRapporto != "-1")
                    {
                        nota = "In data odierna per il rapporto nr " + nrRapporto.ToString() + " e’ stato versato su conto di servizio titolo di euro " + dr.ItemArray[4].ToString() + " estremi " + dr.ItemArray[6].ToString() + ", al quale vanno aggiunti i giorni previsti da convenzione per realizzare il bonifico.";
                        output = this.InsertNewNote(nrRapporto.ToString(), nota);
                        if (output == "OK")
                        {
                            string datetime = string.Empty;
                            Logger.Log(datetime + "Contratto " + dr.ItemArray[1].ToString().Substring(dr.ItemArray[1].ToString().Length - 7) + ": inserimento di nota: \"" + nota + "\" finito con successo", LogType.Operation);
                            //string text7 = "UPDATE PAGAMENTI SET PAGAMENTI.ESIGENZA_STATO = 'NOTE GEISP' WHERE PAGAMENTI.COD_VEL = " + dr.ItemArray[3].ToString() + " and PAGAMENTI.COD_INT = " + dr.ItemArray[8].ToString();
                            report.Rows.Add(db.getSocietaName(), dr.ItemArray[3].ToString(), nota, nrRapporto.ToString());

                        }
                        else
                        {
                            string datetime = string.Empty;
                            Logger.Log(datetime + "Contratto " + dr.ItemArray[1].ToString().Substring(dr.ItemArray[1].ToString().Length - 7) + ": inserimento di nota: \"" + nota + "\" non ha finito con successo", LogType.Operation);
                        }
                    }
                    else
                    {
                        try
                        {
                            Logger.Log(string.Empty + "Contratto " + dr.ItemArray[1].ToString().Substring(dr.ItemArray[1].ToString().Length - 7) + ": non trovato nella tabella Rapporti", LogType.Operation);
                            NonTrovati.Rows.Add(db.getSocietaName(), dr.ItemArray[3].ToString(), "Non trovato nella tabella Rapporti", dr.ItemArray[1].ToString());
                        }
                        catch { }
                    }
                }

                query = string.Format("SELECT DISTINCT PRATICHE.CONTRATTO from PAGAMENTI"
                    + " inner join PRATICHE on PRATICHE.COD_VEL = PAGAMENTI.COD_VEL"
                    + " inner join DEBITORI on DEBITORI.COD_DEB = PRATICHE.COD_DEB"
                    + " where CONVERT(nvarchar(10),PAGAMENTI.DATA1,105) = CONVERT(nvarchar(10), GETDATE(), 105)"
                    //+ " and PAGAMENTI.DATA2 <> PAGAMENTI.DATA1" 
                    + " and PAGAMENTI.IMPORTO_EURO > 0  and PAGAMENTI.COD_INT < 500"
                    + " and NOT EXISTS (SELECT 1 FROM PAGAMENTI P WHERE P.COD_VEL = PAGAMENTI.COD_VEL AND P.COD_INT = 500 + PAGAMENTI.COD_INT) "
                    + " and PRATICHE.COD_BAS IN ({0})", db.codBas);

                Logger.Log("Estrazione dei pagamenti per il database GEISP (" + db.name + ") per la seconda procedura", LogType.Operation);

                DataTable dtContratto = new DataTable();

                if (this.sql.Connection.State.ToString().ToUpper() == "OPEN")
                {
                    try
                    {
                        dtContratto = this.sql.ExtractValues(query);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LogType.Error);
                    }
                }

                //query = string.Format("SELECT PAGAMENTI.BASE, PRATICHE.CONTRATTO, DEBITORI.NOM_DEB, PRATICHE.COD_VEL, PAGAMENTI.IMPORTO_EURO,"
                //+"ISNULL(PAGAMENTI.DATAVERA, PAGAMENTI.DATA2) as 'data di valuta del titolo', PAGAMENTI.ESTREMI, PAGAMENTI.DATA_DISTINTA_INCASSI,"
                //+"PAGAMENTI.DATA1,PAGAMENTI.DATA2,PAGAMENTI.DATA3, PAGAMENTI.TIPO_PAGA from PAGAMENTI inner join PRATICHE on PRATICHE.COD_VEL = PAGAMENTI.COD_VEL"
                //+" inner join DEBITORI on DEBITORI.COD_DEB = PRATICHE.COD_DEB where CONVERT(nvarchar(10),PAGAMENTI.DATA1,105) = CONVERT(nvarchar(10), GETDATE(), 105)" 
                //+" and PAGAMENTI.DATA2 <> PAGAMENTI.DATA1  and PAGAMENTI.IMPORTO_EURO > 0  and PAGAMENTI.COD_INT < 500"  
                //+" and NOT EXISTS (select 1 from PAGAMENTI P where P.COD_VEL = PAGAMENTI.COD_VEL and P.COD_INT = 500 + PAGAMENTI.COD_INT) "
                //+" and PRATICHE.COD_BAS in ({0})",db.codBas);
                //DataTable dtCountPayments = this.sql.ExtractValues(query);
                //Logger.Log(string.Concat(new object[] { "Trovati ", dtCountPayments.Rows.Count.ToString(), " pagamenti sul database GALAXIA (", db.societatId, " cod_base ", db.codBas, ")" }), LogType.Operation);

                Logger.Log(string.Concat(new object[] { "Trovati ", dtContratto.Rows.Count.ToString(), " pagamenti sul database GALAXIA (", db.societatId, " cod_base ", db.codBas, ")" }), LogType.Operation);

                foreach (DataRow dr in dtContratto.Rows)
                {
                    string entireRow = dr.ItemArray[0].ToString();
                    int rowLength = entireRow.Length;
                    string last7Row = entireRow.Substring(rowLength - 7);
                    query = string.Format("SELECT PAGAMENTI.BASE, PRATICHE.CONTRATTO, DEBITORI.NOM_DEB, PRATICHE.COD_VEL," 
                        + "PAGAMENTI.IMPORTO_EURO,ISNULL(PAGAMENTI.DATAVERA, PAGAMENTI.DATA2) as 'data di valuta del titolo'," 
                        + "PAGAMENTI.ESTREMI, PAGAMENTI.DATA_DISTINTA_INCASSI,PAGAMENTI.DATA1,PAGAMENTI.DATA2,PAGAMENTI.DATA3,"
                        + "PAGAMENTI.TIPO_PAGA, PAGAMENTI.COD_INT from PAGAMENTI"
                        + " inner join PRATICHE on PRATICHE.COD_VEL = PAGAMENTI.COD_VEL"
                        + " inner join DEBITORI on DEBITORI.COD_DEB = PRATICHE.COD_DEB"
                        + " where CONVERT(nvarchar(10),PAGAMENTI.DATA1,105) = CONVERT(nvarchar(10), GETDATE(), 105)" 
                        //+ " and PAGAMENTI.DATA2 <> PAGAMENTI.DATA1"
                        + " and RIGHT(PRATICHE.CONTRATTO,7) ='{0}' "
                        + " and PAGAMENTI.IMPORTO_EURO > 0  and PAGAMENTI.COD_INT < 500"
                        + " and NOT EXISTS (select 1 from PAGAMENTI P where P.COD_VEL = PAGAMENTI.COD_VEL and P.COD_INT = 500 + PAGAMENTI.COD_INT)"
                        + " and PRATICHE.COD_BAS in ({1})", last7Row, db.codBas);

                    DataTable dtPagamenti2 = this.sql.ExtractValues(query);

                    bool bp1 = false;
                    bool bp2 = false;
                    int nrCp = this.CountCPNumber(dtPagamenti2);
                    int nrBp1 = this.CountBPNumber1(dtPagamenti2);
                    int nrBp2 = this.CountBPNumber2(dtPagamenti2);
                    DateTime minDate2 = this.ExtractMinDate2(dtPagamenti2);
                    DateTime maxDate2 = this.ExtractMaxDate2(dtPagamenti2);
                    double totaleuro = this.CalculateTotalEuroSum(dtPagamenti2);
                    List<string[]> listPagamentiBP = this.ExtractPagamentiList1(dtPagamenti2);
                    List<string[]> listPagamentiBP2 = this.ExtractPagamentiList2(dtPagamenti2);

                    foreach (DataRow drP in dtPagamenti2.Rows)
                    {
                        string type = drP.ItemArray[11].ToString();
                        if (type.ToUpper() == "DI")
                        {
                            string nrRaport = this.determineNrRaport(drP.ItemArray[1].ToString());
                            if (nrRaport != "-1")
                            {
                                nota = "Il cliente per il rapporto NR " + nrRaport.ToString() + " ha provveduto al pagamento di euro " + drP.ItemArray[4].ToString() + " in data " + drP.ItemArray[10].ToString() + " che sar\x00e0 versato nei prossimi giorni sul conto di servizio al quale andranno aggiunti i giorni previsti da convenzione per realizzare il bonifico. ";
                                output = this.InsertNewNote(nrRaport.ToString(), nota);
                                if (output.ToLower() == "ok")
                                {
                                    string datetime = string.Empty;
                                    Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" finito con successo", LogType.Operation);
                                    //string text10 = "UPDATE PAGAMENTI SET PAGAMENTI.ESIGENZA_STATO = 'NOTE GEISP' WHERE PAGAMENTI.COD_VEL = " + drP.ItemArray[3].ToString() + " and PAGAMENTI.COD_INT = " + drP.ItemArray[12].ToString();
                                    report.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), nota, nrRaport.ToString());

                                }
                                else
                                {
                                    string datetime = string.Empty;
                                    Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" non ha finito con successo", LogType.Operation);
                                }
                            }
                            else
                            {
                                Logger.Log(string.Empty + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": non trovato nella tabella Rapporti", LogType.Operation);
                                NonTrovati.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), "Non trovato nella tabella Rapporti", drP.ItemArray[1].ToString());
                            }
                        }
                        else if (type.ToUpper() == "CP")
                        {
                            string nrRaport = this.determineNrRaport(drP.ItemArray[1].ToString());
                            if (nrRaport != "-1")
                            {
                                nota = "Il cliente per il rapporto NR " + nrRaport.ToString() + " propone rientro mediante  NR " + nrCp.ToString() + " promesse di pagamento con scadenza dal " + minDate2.ToString() + " al " + maxDate2.ToString() + " " + drP.ItemArray[4].ToString() + " in data " + drP.ItemArray[9].ToString() + " per un totale di euro " + totaleuro.ToString() + " che transiteranno sul conto corrente di servizio a copertura di quanto in nostro affidamento.";
                                output = this.InsertNewNote(nrRaport.ToString(), nota);
                                if (output.ToLower() == "ok")
                                {
                                    string datetime = string.Empty;
                                    Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" finito con successo", LogType.Operation);
                                    //string text11 = "UPDATE PAGAMENTI SET PAGAMENTI.ESIGENZA_STATO = 'NOTE GEISP' WHERE PAGAMENTI.COD_VEL = " + drP.ItemArray[3].ToString() + " and PAGAMENTI.COD_INT = " + drP.ItemArray[12].ToString();
                                    report.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), nota, nrRaport.ToString());

                                }
                                else
                                {
                                    string datetime = string.Empty;
                                    Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" non ha finito con successo", LogType.Operation);
                                }
                            }
                            else
                            {
                                Logger.Log(string.Empty + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": non trovato nella tabella Rapporti", LogType.Operation);
                                NonTrovati.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), "Non trovato nella tabella Rapporti", drP.ItemArray[1].ToString());                          
                            }
                        }
                        else if (type.ToUpper() == "BP")
                        {
                            if (((drP.ItemArray[6].ToString()[0] == 'I') || (drP.ItemArray[6].ToString()[0] == 'J')) && !bp1)
                            {
                                string nrRaport = this.determineNrRaport(drP.ItemArray[1].ToString());
                                if (nrRaport != "-1")
                                {
                                    nota = "Il cliente per il rapporto NR " + nrRaport.ToString() + " ha provveduto ha sottoscrivere piano di rientro tramite nr " + nrBp1.ToString() + " rate  aventi i seguenti importi e scadenze ";
                                    foreach (string[] s in listPagamentiBP)
                                    {
                                        nota += s[0] + "->" + s[1] + " ";
                                    }
                                    nota = nota + ". Tali pagamenti transiteranno su conto di servizio e alla scadenza dovranno essere aggiunti i giorni previsti da convenzione per il bonifico.";
                                    output =this.InsertNewNote(nrRaport.ToString(), nota);
                                    if (output.ToLower() == "ok")
                                    {
                                        string datetime = string.Empty;
                                        Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" finito con successo", LogType.Operation);
                                        report.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), nota, nrRaport.ToString());

                                    }
                                    else
                                    {
                                        string datetime = string.Empty;
                                        Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" non ha finito con successo", LogType.Operation);
                                    }
                                }
                                else
                                {
                                    Logger.Log(string.Empty + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": non trovato nella tabella Rapporti", LogType.Operation);
                                    NonTrovati.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), "Non trovato nella tabella Rapporti", drP.ItemArray[1].ToString());
                                }
                                bp1 = true;
                            }
                            if (((drP.ItemArray[6].ToString()[0] != 'I') && (drP.ItemArray[6].ToString()[0] != 'J')) && !bp2)
                            {
                                string nrRaport = this.determineNrRaport(drP.ItemArray[1].ToString());
                                if (nrRaport != "-1")
                                {
                                    nota = "Il cliente per il rapporto NR " + nrRaport.ToString() + " ha provveduto ha sottoscrivere piano di rientro tramite nr " + nrBp2.ToString() + " rate  aventi i seguenti importi e scadenze ";
                                    foreach (string[] s in listPagamentiBP2)
                                    {
                                        nota += s[0] + "->" + s[1] + " ";
                                    }
                                    nota = nota + " a copertura di quanto in nostro affidamento.";
                                    output =this.InsertNewNote(nrRaport.ToString(), nota);
                                    if (output.ToLower() == "ok")
                                    {
                                        string datetime = string.Empty;
                                        Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" finito con successo", LogType.Operation);
                                        report.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), nota, nrRaport.ToString());

                                    }
                                    else
                                    {
                                        string datetime = string.Empty;
                                        Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" non ha finito con successo", LogType.Operation);
                                    }
                                }
                                else
                                {
                                    Logger.Log(string.Empty + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": non trovato nella tabella Rapporti", LogType.Operation);
                                    NonTrovati.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), "Non trovato nella tabella Rapporti", drP.ItemArray[1].ToString());
                                }
                                bp2 = true;
                            }
                        }
                        else
                        {
                            string nrRaport = this.determineNrRaport(drP.ItemArray[1].ToString());
                            if (nrRaport != "-1")
                            {
                                nota = "Il cliente per il rapporto NR " + nrRaport.ToString() + " ha rilasciato titolo di euro " + drP.ItemArray[4].ToString() + " in data " + drP.ItemArray[8].ToString() + " che sar\x00e0 versato nei prossimi giorni sul conto di servizio al quale andranno aggiunti i giorni previsti da convenzione per realizzare il bonifico.";
                                output = this.InsertNewNote(nrRaport.ToString(), nota);
                                if (output.ToLower() == "ok")
                                {
                                    string datetime = string.Empty;
                                    Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" finito con successo", LogType.Operation);
                                    //string text12 = "UPDATE PAGAMENTI SET PAGAMENTI.ESIGENZA_STATO = 'NOTE GEISP' WHERE PAGAMENTI.COD_VEL = " + drP.ItemArray[3].ToString() + " and PAGAMENTI.COD_INT = " + drP.ItemArray[12].ToString();
                                    report.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), nota, nrRaport.ToString());

                                }
                                else
                                {
                                    string datetime = string.Empty;
                                    Logger.Log(datetime + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": inserimento di nota: \"" + nota + "\" non ha finito con successo", LogType.Operation);
                                }
                            }
                            else
                            {
                                Logger.Log(string.Empty + "Contratto " + drP.ItemArray[1].ToString().Substring(drP.ItemArray[1].ToString().Length - 8) + ": non trovato nella tabella Rapporti", LogType.Operation);
                                NonTrovati.Rows.Add(db.getSocietaName(), drP.ItemArray[3].ToString(), "Non trovato nella tabella Rapporti", drP.ItemArray[1].ToString());
                            }
                        }
                    }
                }
                if (this.oledb.Connection.State.ToString().ToUpper() == "OPEN")
                {
                    output = this.oledb.CloseConnection();
                    bool flag1 = output.ToUpper() == "OK";
                }
                if (this.sql.Connection.State.ToString().ToUpper() == "OPEN")
                {
                    output = this.sql.CloseConnection();
                    bool flag2 = output.ToUpper() == "OK";
                }

            }

            string STOP_STOP = string.Empty;

            List<string> listfiles = new List<string>();
            string today = string.Concat(new object[] { DateTime.Now.Day, "_", DateTime.Now.Month, "_", DateTime.Now.Year });
            string _loggingDirectory = ConfigUtils.GetLogPath() + "logger";
            string errorFile = "errors.txt";
            if (File.Exists(_loggingDirectory + today + @"\" + errorFile))
            {
                listfiles.Add(_loggingDirectory + today + @"\" + errorFile);
            }
            errorFile = "operations.txt";
            if (File.Exists(_loggingDirectory + today + @"\" + errorFile))
            {
                listfiles.Add(_loggingDirectory + today + @"\" + errorFile);
            }
            string excelFile = MakeExcel();

            var files = new List<string>();
            files.Add(excelFile);
            //SendMail(files);

        }
        
        private string determineNrRaport(string p)
        {
            try
            {
                string query = string.Format("select Rapporti.* from Rapporti where RIGHT(Rapporti.NumeroRapporto,7) = {0}", p.Substring(p.Length - 7));
                DataTable dtRapporti = new DataTable();
                if (this.oledb.Connection.State.ToString().ToLower() == "open")
                {
                    try
                    {
                        dtRapporti = this.oledb.ExtractValues(query);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LogType.Error);
                    }
                }

                if (dtRapporti.Rows.Count >= 1)
                {
                    return p.Substring(p.Length - 7);
                }
                else
                    return "-1";
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
                return "-1";
            }

        }

        private string InsertNewNote(string nrRapporto, string nota)
        {
            string output = "OK";
            string query = string.Empty;
            // string query = string.Concat(new object[] { "select Rapporti.PCSocieta, Rapporti.PC, Rapporti.DataIncaricoASocieta from Rapporti where RIGHT(Rapporti.NumeroRapporto,", nrRapporto.Length, ") = \"", nrRapporto, "\"" });
            query = string.Format("SELECT Rapporti.PCSocieta, Rapporti.PC, Rapporti.DataIncaricoASocieta from Rapporti"
                  + " where RIGHT(Rapporti.NumeroRapporto,{0}) = {1}", nrRapporto.Length, nrRapporto);
            DataTable dtRapporti = new DataTable();
            if (this.oledb.Connection.State.ToString().ToLower() == "open")
            {
                try
                {
                    dtRapporti = this.oledb.ExtractValues(query);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString(), LogType.Error);
                }
            }
            if (dtRapporti.Rows.Count == 1)
            {
                //query = string.Concat(new object[] { "INSERT INTO NotiziaIncaricoPC ([DataCarico], [PCSocieta], [PCCliente], [DataNotizia], [ReferenteSocieta], [TelefonoReferenteSocieta], [LunghezzaNota], [Nota], [Data_Ultima_Modifica_Soc], [Data_Ultima_Modifica_Ist], [Flag_DS], [UserName], [DataScarico], [Flag_Old_New]) VALUES (\"", DateTime.Now.ToString(), "\",\"", dtRapporti.Rows[0].ItemArray[0].ToString(), "\",\"", dtRapporti.Rows[0].ItemArray[1].ToString(), "\",\"", DateTime.Now.ToString(), "\",NULL,NULL,\"", nota.Length, "\",\"", nota.ToString(), "\",\"", DateTime.Now.ToString(), "\",NULL,\"S\",NULL,NULL,\"NEW\")" });
                query = string.Format("INSERT INTO NotiziaIncaricoPC ([DataCarico], [PCSocieta], [PCCliente], [DataNotizia],"
                    + "[ReferenteSocieta], [TelefonoReferenteSocieta], [LunghezzaNota], [Nota], [Data_Ultima_Modifica_Soc],"
                    + "[Data_Ultima_Modifica_Ist], [Flag_DS], [UserName], [DataScarico], [Flag_Old_New])"
                    + " VALUES('{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}',{9},'{10}',{11},{12},'{13}')",
                    DateTime.Now.ToString(), dtRapporti.Rows[0].ItemArray[0].ToString(), dtRapporti.Rows[0].ItemArray[1].ToString(),
                    DateTime.Now.ToString(), "NULL", "NULL", nota.Length.ToString(), nota.ToString(), DateTime.Now.ToString(),
                    "NULL", "S", "NULL", "NULL", "NEW");
                this.oledb.InsertNewRow(query);
                return output;
            }
            if (dtRapporti.Rows.Count == 0)
            {
                output = "NOK";
            }
            if (dtRapporti.Rows.Count <= 1)
            {
                return output;
            }
            int row = 0;
            int i = 0;
            DateTime dt = new DateTime(0x76c, 2, 4);
            foreach (DataRow dr in dtRapporti.Rows)
            {
                if (DateTime.Parse(dr.ItemArray[2].ToString()).CompareTo(dt) > 0)
                {
                    dt = DateTime.Parse(dr.ItemArray[2].ToString());
                    row = i;
                }
                i++;
            }

            //query = string.Concat(new object[] { "INSERT INTO NotiziaIncaricoPC ([DataCarico], [PCSocieta], [PCCliente], [DataNotizia], [ReferenteSocieta], [TelefonoReferenteSocieta], [LunghezzaNota], [Nota], [Data_Ultima_Modifica_Soc], [Data_Ultima_Modifica_Ist], [Flag_DS], [UserName], [DataScarico], [Flag_Old_New]) VALUES (\"", DateTime.Now.ToString(), "\",\"", dtRapporti.Rows[row].ItemArray[0].ToString(), "\",\"", dtRapporti.Rows[row].ItemArray[1].ToString(), "\",\"", DateTime.Now.ToString(), "\",NULL,NULL,\"", nota.Length, "\",\"", nota.ToString(), "\",\"", DateTime.Now.ToString(), "\",NULL,\"S\",NULL,NULL,\"NEW\")" });  
            query = string.Format("INSERT INTO NotiziaIncaricoPC ([DataCarico], [PCSocieta], [PCCliente], [DataNotizia],"
                + "[ReferenteSocieta], [TelefonoReferenteSocieta], [LunghezzaNota], [Nota], [Data_Ultima_Modifica_Soc],"
                + "[Data_Ultima_Modifica_Ist], [Flag_DS], [UserName], [DataScarico], [Flag_Old_New])"
                + " VALUES('{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}',{9},'{10}',{11},{12},'{13}')",
                DateTime.Now.ToString(), dtRapporti.Rows[row].ItemArray[0].ToString(), dtRapporti.Rows[row].ItemArray[1].ToString(),
                DateTime.Now.ToString(), "NULL", "NULL", nota.Length.ToString(), nota.ToString(), DateTime.Now.ToString(),
                "NULL", "S", "NULL", "NULL", "NEW");
            this.oledb.InsertNewRow(query);
            return "OK";
        }

        private double CalculateTotalEuroSum(DataTable dtPagamenti2)
        {
            double sum = 0.0;
            try
            {
                foreach (DataRow r in dtPagamenti2.Rows)
                {
                    if (r.ItemArray[11].ToString().ToUpper() == "CP")
                    {
                        string val = r.ItemArray[4].ToString();
                        double importo = 0.0;
                        if (!double.TryParse(val, out importo))
                        {
                            Logger.Log("error converting value from field \"Importo_euro\" for contract " + r.ItemArray[1].ToString(), LogType.Error);
                        }
                        else
                        {
                            sum += importo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return sum;
        }

        private int CountBPNumber1(DataTable dtPagamenti2)
        {
            int nr = 0;
            try
            {
                foreach (DataRow r in dtPagamenti2.Rows)
                {
                    if ((r.ItemArray[11].ToString().ToUpper() == "BP") && (r.ItemArray[6].ToString().Contains<char>('I') || r.ItemArray[6].ToString().Contains<char>('J')))
                    {
                        nr++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return nr;
        }

        private int CountBPNumber2(DataTable dtPagamenti2)
        {
            int nr = 0;
            try
            {
                foreach (DataRow r in dtPagamenti2.Rows)
                {
                    if (((r.ItemArray[11].ToString().ToUpper() == "BP") && !r.ItemArray[6].ToString().Contains<char>('I')) && !r.ItemArray[6].ToString().Contains<char>('J'))
                    {
                        nr++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return nr;
        }

        private int CountCPNumber(DataTable dtPagamenti2)
        {
            int nr = 0;
            try
            {
                foreach (DataRow r in dtPagamenti2.Rows)
                {
                    if (r.ItemArray[11].ToString().ToUpper() == "CP")
                    {
                        nr++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return nr;
        }
   
        private DateTime ExtractMaxDate2(DataTable dtPagamenti2)
        {
            DateTime d = new DateTime(0x76c, 1, 1);
            try
            {
                foreach (DataRow r in dtPagamenti2.Rows)
                {
                    DateTime data2Field = DateTime.Parse(r.ItemArray[9].ToString());
                    if ((data2Field.CompareTo(d) > 0) && (r.ItemArray[11].ToString().ToUpper() == "CP"))
                    {
                        d = new DateTime(data2Field.Year, data2Field.Month, data2Field.Day);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return d;
        }

        private DateTime ExtractMinDate2(DataTable dtPagamenti2)
        {
            DateTime d = new DateTime(0x834, 1, 1);
            try
            {
                foreach (DataRow r in dtPagamenti2.Rows)
                {
                    DateTime data2Field = DateTime.Parse(r.ItemArray[9].ToString());
                    if ((data2Field.CompareTo(d) < 0) && (r.ItemArray[11].ToString().ToUpper() == "CP"))
                    {
                        d = new DateTime(data2Field.Year, data2Field.Month, data2Field.Day);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return d;
        }

        private List<string[]> ExtractPagamentiList1(DataTable dtPagamenti2)
        {
            List<string[]> list = new List<string[]>();
            try
            {
                foreach (DataRow dr in dtPagamenti2.Rows)
                {
                    if ((dr.ItemArray[11].ToString().ToUpper() == "BP") && (dr.ItemArray[6].ToString().Contains<char>('I') || dr.ItemArray[6].ToString().Contains<char>('J')))
                    {
                        list.Add(new string[] { dr.ItemArray[4].ToString(), dr.ItemArray[9].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return list;
        }

        private List<string[]> ExtractPagamentiList2(DataTable dtPagamenti2)
        {
            List<string[]> list = new List<string[]>();
            try
            {
                foreach (DataRow dr in dtPagamenti2.Rows)
                {
                    if (((dr.ItemArray[11].ToString().ToUpper() == "BP") && !dr.ItemArray[6].ToString().Contains<char>('I')) && !dr.ItemArray[6].ToString().Contains<char>('J'))
                    {
                        list.Add(new string[] { dr.ItemArray[4].ToString(), dr.ItemArray[9].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return list;
        }

        private string MakeExcel()
        {
            var dataTables = new List<System.Data.DataTable>();
            var sheetsNames = new List<string>();

            dataTables.Add(report);
            sheetsNames.Add("Note");
            dataTables.Add(NonTrovati);
            sheetsNames.Add("NonTrovati");
            string date = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string outcomesPath = ExcelUtils.WriteExcel(date + "_InserimentoNoteGeisp", dataTables, sheetsNames);
            return outcomesPath;
        }

        private void InitReportDataTable()
        {
            report.Columns.Add("Societa");
            report.Columns.Add("Cod Vel");
            report.Columns.Add("Nota");
            report.Columns.Add("Nr Raporto");


            NonTrovati.Columns.Add("Societa");
            NonTrovati.Columns.Add("Cod Vel");
            NonTrovati.Columns.Add("Esito");
            NonTrovati.Columns.Add("Contratto");
        }

        private void SendMail(List<string> files)
        {
            try
            {
                MailUtils.Send("Inserimente Note Geisp.",
                    "Inserimente Note Geisp.",
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

       
    }
}

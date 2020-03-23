using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace InserimentoNoteGEISP.Utils
{
    public class AccesUtil
    {
        private OleDbConnection conn = null;

        public OleDbConnection Connection
        {
            get { return conn; }
        }

        public AccesUtil() { }

        public string InitConnection(string DBname)
        {
            string output = "OK";
            try
            {
                string connectionString = string.Empty;
                connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source= " + DBname + "; Persist Security info=false";
                //connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                //                    @"Data source= D:\program\" +
                //                    @"Transactions.accdb";
                conn = new OleDbConnection(connectionString);
                conn.Open();
            }
            catch (OleDbException olEx)
            {
                output = olEx.ToString();
            }
            catch (Exception ex)
            {
                output = ex.ToString();
            }
            return output;
        }


        public string CloseConnection()
        {
            string output = "OK";
            try
            {
                conn.Close();
            }
            catch (OleDbException olEx)
            {
                output = olEx.ToString();

            }
            catch (Exception ex)
            {
                output = ex.ToString();

            }
            return output;
        }

        public DataTable ExtractValues(string query)
        {
            DataTable table = new DataTable();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, Connection);
            OleDbCommandBuilder cmd = new OleDbCommandBuilder(dataAdapter);
            dataAdapter.Fill(table);
            //ciudat asa se face sigur?:D folosesc mai sus acelasi lucru pe dgeisp_query si merge lejer, aici nu mai vrea, is instante diferite deci ce plm
            return table;
        }

        public string ExecuteNonQuery(string query)
        {
            string output = "OK";
            try
            {
                OleDbCommand olDBCom = new OleDbCommand(query, Connection);
                int nbRowsAffected = olDBCom.ExecuteNonQuery();
                if (nbRowsAffected == 0)
                {
                    output = "Error";
                    // Logger.Log("The following query \n" + query + "\n did not modify any rows", LogType.Error);
                }
            }
            catch (Exception ex)
            {
                //Logger.Log(ex.ToString(), LogType.Error);
                output = ex.ToString();
            }
            return output;
        }

    }
}

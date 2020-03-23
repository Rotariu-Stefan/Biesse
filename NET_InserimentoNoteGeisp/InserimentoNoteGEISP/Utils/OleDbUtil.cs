namespace InserimentoNoteGEISP.Utils
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Globalization;

    public class OleDbUtil
    {
        private OleDbConnection conn;

        public string CloseConnection()
        {
            string output = "OK";
            try
            {
                this.conn.Close();
            }
            catch (OleDbException olex)
            {
                output = olex.ToString();
            }
            catch (Exception ex)
            {
                output = ex.ToString();
            }
            return output;
        }

        public DataTable ExtractValues(string query)
        {
            DataTable dt = new DataTable();
            if (this.Connection.State.ToString().ToUpper() == "OPEN")
            {
                try
                {
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter {
                        SelectCommand = new OleDbCommand(query, this.Connection)
                    };
                    dt.Locale = CultureInfo.InvariantCulture;
                    dataAdapter.Fill(dt);
                }
                catch (OleDbException olex)
                {
                    Logger.Log(olex.ToString(), LogType.Error);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString(), LogType.Error);
                }
                return dt;
            }
            Logger.Log("Error: Connection to the access database is closed", LogType.Error);
            return dt;
        }

        public string InitConnection(string pathAccessDb)
        {
            string output = "OK";
            try
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + pathAccessDb + ";Persist Security Info=False";
                this.conn = new OleDbConnection(connectionString);
                this.conn.Open();
            }
            catch (OleDbException olex)
            {
                output = olex.ToString();
            }
            catch (Exception ex)
            {
                output = ex.ToString();
            }
            return output;
        }

        public int InsertNewRow(string query)
        {
            int nbRowsAffected = -1;
            try
            {
                nbRowsAffected = new OleDbCommand(query, this.Connection).ExecuteNonQuery();
                if (nbRowsAffected == 0)
                {
                    Logger.Log("The following query could not add new rows: " + query, LogType.Error);
                }
            }
            catch (OleDbException olex)
            {
                Logger.Log(olex.ToString(), LogType.Error);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return nbRowsAffected;
        }

        public OleDbConnection Connection
        {
            get
            {
                return this.conn;
            }
        }
    }
}


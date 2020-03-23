namespace AggiornaProrogheGeisp.Utils
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    internal class SQLUtil
    {
        private SqlConnection _conn;

        public string CloseConnection()
        {
            string output = "OK";
            try
            {
                this._conn.Close();
            }
            catch (SqlException sqlex)
            {
                output = sqlex.ToString();
                Logger.Log(output, LogType.Error);
            }
            catch (Exception ex)
            {
                output = ex.ToString();
                Logger.Log(output, LogType.Error);
            }
            return output;
        }

        public string ExecuteNonQuery(string query)
        {
            string output = "OK";
            try
            {
                SqlCommand sqlCom = new SqlCommand(query, this.Connection);
                if (sqlCom.ExecuteNonQuery() == 0)
                {
                    output = "Error";
                    Logger.Log("The following query \n" + query + "\n did not modify any rows", LogType.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
                output = ex.ToString();
            }
            return output;
        }

        public DataTable ExtractValues(string query)
        {
            DataTable table = new DataTable();
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, this.Connection);
                new SqlCommandBuilder(dataAdapter);
                table.Locale = CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
            }
            catch (SqlException sex)
            {
                Logger.Log(sex.ToString(), LogType.Error);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LogType.Error);
            }
            return table;
        }

        public string InitConnection(string serverDb,string nameDb, string userDB, string passDB, string trustedc)
        {
            string output = "OK";
            try
            {
                this._conn = new SqlConnection();
                this._conn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};Trusted_Connection={4}", serverDb, nameDb, userDB, passDB, trustedc);
                this._conn.Open();
            }
            catch (SqlException sqlex)
            {
                output = sqlex.ToString();
            }
            catch (Exception ex)
            {
                output = ex.ToString();
            }
            return output;
        }

        public SqlConnection Connection
        {
            get
            {
                return this._conn;
            }
        }
    }
}


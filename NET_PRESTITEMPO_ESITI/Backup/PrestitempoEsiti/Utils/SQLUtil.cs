namespace PrestitempoInserimento.Utils
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Collections.Generic;

    public class SQLUtil
    {
        public class Parameter
        {
            public string name
            { get; set; }
            public object value
            { get; set; }
            public Parameter(string name, object value)
            { 
                this.name = name;
                this.value = value;
            }

        }
        public class StoredProcedure
        {
            public StoredProcedure(string name)
            { 
                this.name = name; 
            }
            public string name
            { get; set; }
            public List<Parameter> parameters = new List<Parameter>();
            public List<Parameter> GetParameters()
            {
                return parameters;
            }
            public void AddParameter(Parameter p)
            {
                parameters.Add(p);
            }
        }

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
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, this.Connection);
                new SqlCommandBuilder(dataAdapter);
                table.Locale = CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
            }
            catch (SqlException)
            {
            }
            catch (Exception)
            {
            }
            return table;
        }

        public string InitConnection(string urlServer, string userDB, string passDB, string nameDb, string trustedc)
        {
            string output = "OK";
            try
            {
                string connectionString = string.Empty;
                if (trustedc == "false")
                {
                    connectionString = "user id=" + userDB + ";password=" + passDB + ";server=" + urlServer + ";Trusted_Connection=no;database=" + nameDb + "; connection timeout=30";
                }
                else
                {
                    connectionString = "server=" + urlServer + ";Trusted_Connection=yes;database=" + nameDb + "; connection timeout=0";
                }
                this._conn = new SqlConnection();
                this._conn.ConnectionString = connectionString;
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

        public void ExecStoredProcedure(StoredProcedure sp)
        { 
                    SqlCommand myCmd = new SqlCommand();
                    if (_conn.State == ConnectionState.Open)
                        {
                            myCmd.Connection = _conn;
                            myCmd.CommandType = CommandType.StoredProcedure;
                            myCmd.CommandText = sp.name;
                            myCmd.CommandTimeout = 0;
                            foreach (var p in sp.parameters) 
                            {
                                myCmd.Parameters.AddWithValue("@" + p.name, p.value);
                            }
                            myCmd.ExecuteNonQuery();
                        }
                        myCmd.Dispose();
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


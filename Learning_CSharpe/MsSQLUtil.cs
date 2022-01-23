using System;
using System.Data;
using System.Data.SqlClient;

namespace Learning_CSharpe
{
    public class MsSQLUtil
    {
        private string connectString = "";
        public string server="localhost";
        public string database="master";
        public string dbuid;
        public string dbpwd;
        SqlConnection sqlCon;
        SqlTransaction sqlTran;
        SqlCommand sqlCmd;
        public MsSQLUtil()
        {
            this.connectString = string.Format("server={0};database={1};Integrated security=SSPI;Connect Timeout = 180", this.server, this.database);
        }
        public MsSQLUtil(string _server, string _database, string _dbuid, string _dbpwd)
        {
            Config(_server, _database, _dbuid, _dbpwd);
        }
        public void Config(string _server, string _database, string _dbuid, string _dbpwd)
        {
            this.server = _server;
            this.database = _database;
            this.dbuid = _dbuid;
            this.dbpwd = _dbpwd;
            this.connectString = string.Format("server={0};database={1};uid={2};pwd={3};Connect Timeout = 180", this.server, this.database, this.dbuid, this.dbpwd);
        }
        private SqlConnection Connect()
        {
            try
            {  
                this.sqlCon = new SqlConnection();
                this.sqlCon.ConnectionString = this.connectString;
                if (this.sqlCon.State == ConnectionState.Open)
                    this.sqlCon.Close();
                this.sqlCon.Open();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return this.sqlCon;
        }
        public DataTable Query(string sqlQuery)
        {
            DataTable dt = new DataTable();
            try
            {                
                this.sqlCon = this.Connect();
                this.sqlCmd = this.sqlCon.CreateCommand();
                this.sqlCmd.Connection = this.sqlCon;
                this.sqlCmd.CommandText = sqlQuery;
                this.sqlCmd.CommandTimeout = 600;

                SqlDataAdapter adapter = new SqlDataAdapter(this.sqlCmd);
                adapter.Fill(dt);
                adapter.Dispose();
                this.Close();
            }
            catch
            {

            }
            return dt;
        }
        public string NonQuery(string sqlQuery)
        {
            bool flag = false;

            this.sqlCon = this.Connect();
            this.sqlCmd = this.sqlCon.CreateCommand();
            //IsolationLevel.ReadCommitted
            this.sqlTran = this.sqlCon.BeginTransaction();
            this.sqlCmd.Connection = this.sqlCon;
            this.sqlCmd.Transaction = this.sqlTran;

            try
            {
                this.sqlCmd.CommandText = sqlQuery;
                this.sqlCmd.ExecuteNonQuery();
                this.sqlTran.Commit();
            }
            catch (Exception ex)
            {
                this.sqlTran.Rollback();
                throw (ex);
            }
            finally
            {
                this.Close();
            }

            if (flag)
                return "NonQuery successfully!";
            else
                return "NonQuery failed";
        }
        private void Close()
        {
            if (this.sqlCon.State == ConnectionState.Open)
                this.sqlCon.Close();
            this.sqlCmd.Dispose();
            this.sqlCon.Dispose();
            if (this.sqlTran!=null)
                this.sqlTran.Dispose();
        }
    }
}

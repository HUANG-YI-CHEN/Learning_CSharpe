using System;
using System.Data;
using System.Data.SqlClient;

namespace Learning_CSharpe
{
    public class MsSQLUtil
    {
        private string connectString = "Server=localhost;Integrated security=SSPI;database=master";
        SqlConnection sqlCon;
        SqlTransaction sqlTran;
        SqlCommand sqlcmd;
        
        internal SqlConnection Connect()
        {
            try
            {   
                this.sqlCon = new SqlConnection();
                this.sqlCon.ConnectionString = this.connectString;
                if (this.sqlCon.State == ConnectionState.Open)
                    this.sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return this.sqlCon;
        }

        public SqlConnection Connect(string Server, string Database, string dbuid, string dbpwd)
        {
            try
            {
                this.connectString = string.Format("server={0};database={1};uid={2};pwd={3};Connect Timeout = 180", Server, Database, dbuid, dbpwd);
                this.sqlCon = new SqlConnection();
                this.sqlCon.ConnectionString = this.connectString;
                if (this.sqlCon.State == ConnectionState.Open)
                    this.sqlCon.Close();                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return this.sqlCon;
        }
        internal DataTable Query(string sqlQuery)
        {
            DataTable dt = new DataTable();
            this.sqlCon = this.Connect();
            this.sqlcmd = this.sqlCon.CreateCommand();
            this.sqlcmd.Connection = this.sqlCon;
            this.sqlcmd.CommandText = sqlQuery;
            this.sqlcmd.CommandTimeout = 600;

            SqlDataAdapter adapter = new SqlDataAdapter(this.sqlcmd);
            adapter.Fill(dt);
            this.Close();
            adapter.Dispose();
            return dt;
        }
        public DataTable Query(string Server, string Database, string dbuid, string dbpwd, string sqlQuery)
        {
            DataTable dt = new DataTable();
            this.sqlCon = this.Connect(Server, Database, dbuid, dbpwd);
            this.sqlcmd = this.sqlCon.CreateCommand();            
            this.sqlcmd.Connection = this.sqlCon;
            this.sqlcmd.CommandText = sqlQuery;
            this.sqlcmd.CommandTimeout = 600;

            SqlDataAdapter adapter = new SqlDataAdapter(this.sqlcmd);
            adapter.Fill(dt);         
            this.Close();
            adapter.Dispose();
            return dt;
        }
        internal string NonQuery(string sqlQuery)
        {
            bool flag = false;

            this.sqlCon = this.Connect();
            this.sqlcmd = this.sqlCon.CreateCommand();
            //IsolationLevel.ReadCommitted
            this.sqlTran = this.sqlCon.BeginTransaction();
            this.sqlcmd.Connection = this.sqlCon;
            this.sqlcmd.Transaction = this.sqlTran;

            try
            {
                this.sqlcmd.CommandText = sqlQuery;
                this.sqlcmd.ExecuteNonQuery();
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
        public string NonQuery(string Server, string Database, string dbuid, string dbpwd, string sqlQuery)
        {
            bool flag = false;

            this.sqlCon = this.Connect(Server, Database, dbuid, dbpwd);
            this.sqlcmd = this.sqlCon.CreateCommand();
            //IsolationLevel.ReadCommitted
            this.sqlTran = this.sqlCon.BeginTransaction(); 
            this.sqlcmd.Connection = this.sqlCon;
            this.sqlcmd.Transaction = this.sqlTran;

            try
            {
                this.sqlcmd.CommandText = sqlQuery;
                this.sqlcmd.ExecuteNonQuery();
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

        public void Close()
        {
            if (this.sqlCon.State == ConnectionState.Open)
                this.sqlCon.Close();
            this.sqlcmd.Dispose();
            this.sqlCon.Dispose();
            if (this.sqlTran!=null)
                this.sqlTran.Dispose();
        }
    }
}

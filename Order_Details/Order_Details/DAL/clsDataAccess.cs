using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Order_Details.DAL
{
    public class clsDataAccess
    {
        static string databaseOwner = "dbo";
        static ErrorLog oErrorLog = new ErrorLog();
        public static string GetConnectionString()
        {
            string con = ConfigurationManager.ConnectionStrings["Order_Details"].ConnectionString;
            return con;
        }


        public static DataTable GetDataTable(string ProcName, SqlCommand cmd)
        {
            SqlConnection cn = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
                cn.Close();
            }
            return dt;
        }

        public static int ExecuteNonQuery(string ProcName, SqlCommand cmd)
        {
            int ResultExecuteNonQuery = 0;
            try
            {
                SqlConnection cn = new SqlConnection(GetConnectionString());
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cn.Open();
                ResultExecuteNonQuery = cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog(ex.ToString());
            }
            return ResultExecuteNonQuery;
        }

        public static DataSet GetDataSet(string procedureName, SqlCommand command)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                command.CommandText = databaseOwner + "." + procedureName;
                command.Connection = connection;

                //Mark As Stored Procedure
                command.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = command;
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return ds;
        }

        public static string ExceuteScaler(SqlCommand command)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            string ans = string.Empty;
            try
            {

                command.Connection = connection;

                //Mark As Stored Procedure
                command.CommandType = CommandType.StoredProcedure;

                object res = command.ExecuteScalar();
                ans = res.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return ans;

        }

    }
}
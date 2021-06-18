using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DropDownListTask.Models
{
    public class DeptModel
    {
        private SqlConnection con;
        private SqlCommand cmd;
        public  DeptModel()
        {
            con = new SqlConnection("data source=.;database=AdventureWorks2019;Integrated Security=true");
            cmd = new SqlCommand();
        }
        public DataTable DisplayDept()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = " select distinct Name from HumanResources.Department ";
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetEmpByDept(string DeptName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("data source=.;database=AdventureWorks2019;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetEmpByDept", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name",DeptName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }
}
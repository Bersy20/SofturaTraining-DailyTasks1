using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MVCwithADO2.Models
{
    public class CRUDModel
    {
        public DataTable DisplayBook()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("select BookId,Title,a.AuthorID,Price,AuthorName from tbl_Books b join tbl_author a on a.AuthorID=b.AuthorID", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable DisplayAuthorWithID()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("select distinct a.AuthorID,AuthorName from tbl_Books b join tbl_author a on a.AuthorID=b.AuthorID", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public int NewBook(string Title, int aid, double Price)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into tbl_Books values(@Title,select AuthorID from tb_author where)", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@AuthorID", aid);
            cmd.Parameters.AddWithValue("@Price", Price);
            return cmd.ExecuteNonQuery();
            con.Close();
        }
        public int NewBookAuthorIdName(string Title, string aname, double Price)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into tbl_Books values(@Title,(select AuthorID from tbl_author where AuthorName=@AuthorName),@Price)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@AuthorName", aname);
            cmd.Parameters.AddWithValue("@Price", Price);
            return cmd.ExecuteNonQuery();
            con.Close();
        }

        //another method using execute non scalar
        public int NewBookAuthorIdNameAnotherMethod(string Title, string aname, double Price)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into tbl_Books(Title,AuthorID,Price) values(@Title, @AuthorID, @Price)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@AuthorID", SelectAuthorByID(aname));
            cmd.Parameters.AddWithValue("@Price", Price);
            return cmd.ExecuteNonQuery();
            con.Close();
        }
        public int SelectAuthorByID(string aname)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select AuthorID from tbl_author where AuthorName=@authorName", con);
            cmd.Parameters.AddWithValue("@authorName", aname);
            string c = cmd.ExecuteScalar().ToString();
            con.Close();
            return Convert.ToInt32(c);
        }
        public DataTable DisplayAuthors()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select AuthorID,AuthorName from tbl_author",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public int InsertNewAuthor(string authorName)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_InsertAuthor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorName", authorName);
            return cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable BookByID(int bookid)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            string query = "select * from tbl_Books where BookID="+bookid;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public int UpdateBook(int Bookid,string Title,int Aid,double Price)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            string query = "update tbl_Books set Title=@title,AuthorID=@aid,Price=@price where BookID=@bookid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@bookid", Bookid);
            cmd.Parameters.AddWithValue("@title", Title);
            cmd.Parameters.AddWithValue("@aid", Aid);
            cmd.Parameters.AddWithValue("@price", Price);
            return cmd.ExecuteNonQuery();
            con.Close();
        }
        public int DeleteBook(int BookId)
        {
            SqlConnection con = new SqlConnection("data source=.;database=BooksDB;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from tbl_Books where bookid=@bkid", con);
            cmd.Parameters.AddWithValue("@bkid",BookId);
            return cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
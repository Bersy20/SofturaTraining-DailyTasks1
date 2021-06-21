using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MoviesUsingDapper.Models;
using Dapper;

namespace MoviesUsingDapper.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            List<MoviesModel> moviesList = new List<MoviesModel>();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                moviesList = dbcon.Query<MoviesModel>("select * from tbl_Movies").ToList();
            }
            return View(moviesList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MoviesModel moviesModel)
        {
            using(IDbConnection dbcon=new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                string query = "Insert into tbl_Movies(Movies) values('" + moviesModel.Movies + "')";
                int rowIns = dbcon.Execute(query);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            MoviesModel moviesModel = new MoviesModel();
            using(IDbConnection dbcon=new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                moviesModel = dbcon.Query<MoviesModel>("select * from tbl_Movies where No=" + id).SingleOrDefault();
            }
            return View(moviesModel);
        }
        [HttpPost]
        public ActionResult Edit(int id,MoviesModel moviesModel)
        {
            using(IDbConnection dbcon=new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                moviesModel = dbcon.Query<MoviesModel>("update tbl_Movies set Movies='" + moviesModel.Movies+"' where No="+id).SingleOrDefault();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            MoviesModel moviesModel = new MoviesModel();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                moviesModel = dbcon.Query<MoviesModel>("select * from tbl_Movies where No=" + id).SingleOrDefault();
            }
            return View(moviesModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, MoviesModel moviesModel)
        {
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                moviesModel = dbcon.Query<MoviesModel>("delete from tbl_Movies where No=" + id).SingleOrDefault();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            MoviesModel moviesModel = new MoviesModel();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["MoviesConStr"].ConnectionString))
            {
                moviesModel = dbcon.Query<MoviesModel>("select * from tbl_Movies where No=" + id).SingleOrDefault();
            }
            return View(moviesModel);
        }
    }
}
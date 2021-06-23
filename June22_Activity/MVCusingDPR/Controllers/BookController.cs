using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCusingDPR.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using System.Diagnostics;

namespace MVCusingDPR.Controllers
{
    [ExceptionFilter]
    public class BookController : Controller
    {
        // GET: Book
        [HandleError]
        public ActionResult Index()
        {
            List<BookModel> BKlist = new List<BookModel>();
            using(IDbConnection dbcon=new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
            {

                BKlist = dbcon.Query<BookModel>("select * from tbl_Book").ToList();
                throw new Exception("Not working");
            }
            return View(BKlist);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            BookModel bk = new BookModel();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
            {
                bk = dbcon.Query<BookModel>("select * from tbl_Books where BookId="+id,new {id}).SingleOrDefault();
            }
            return View(bk);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(BookModel bmodel)
        {
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
            {
                string query = "insert into tbl_Books(Title,AuthorID,Price) "+
                    "values('" + bmodel.Title + "'," + bmodel.AuthorID + "," + bmodel.Price + ")";
                int rowIns = dbcon.Execute(query);
            }
            return RedirectToAction("Index");
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            BookModel bookModel = new BookModel();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
            {
                bookModel = dbcon.Query<BookModel>("select * from tbl_Books where BookId=" + id).SingleOrDefault();
            }
            return View(bookModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookModel bmodel)
        {
            try
            {
                using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
                {
                    string query = "update tbl_Books " +
                        "set Title='" + bmodel.Title + "',AuthorID=" + bmodel.AuthorID + ",Price=" + bmodel.Price + " where BookId="+id;
                    int rowIns = dbcon.Execute(query);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            BookModel bookModel = new BookModel();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
            {
                bookModel = dbcon.Query<BookModel>("select * from tbl_Books where BookId=" + id).SingleOrDefault();
            }
            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BookModel bookModel)
        {
            BookModel bk = new BookModel();
            using (IDbConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["BKConStr"].ConnectionString))
            {
                bk = dbcon.Query<BookModel>("delete from tbl_Books where BookId=" + id).SingleOrDefault();
            }
            return RedirectToAction("Index");           
        }
    }
    class ActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            //After action method executes
            Trace.WriteLine("Method executed sucessfully" + DateTime.Now.ToString());
        }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Before action method executes
            var ctrlName = filterContext.RouteData.Values["controller"];
            var methodName = filterContext.RouteData.Values["action"];
            Trace.WriteLine("Method  is executing" + DateTime.Now.ToString() + " from the controller " + ctrlName.ToString() + " from the method " + methodName.ToString());
            //filterContext.Result = new RedirectResult("https://github.com/Mentor-Digi/Softura_DotNet_Apr2021/tree/master");

        }
    }
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {            
                var ctrlName = filterContext.RouteData.Values["controller"];
                var methodName = filterContext.RouteData.Values["action"];
                Trace.WriteLine("Exception occurs " + DateTime.Now.ToString() + " from the controller " + ctrlName.ToString() + " from the method " + methodName.ToString());
                filterContext.Result = new RedirectResult("https://github.com/Mentor-Digi/Softura_DotNet_Apr2021/tree/master");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCwithADO2.Models;

namespace MVCwithADO2.Controllers
{
    public class CRUDController : Controller
    {
        public ActionResult Index()
        {
            CRUDModel mdl = new CRUDModel();
            DataTable dt = mdl.DisplayBook();
            return View("Home",dt);
        }
        public ActionResult Insert()
        {
            CRUDModel mdl = new CRUDModel();
            DataTable dt = mdl.DisplayAuthorWithID();
            return View("Create",dt);
        }
        public ActionResult InsertRecord(FormCollection frm,string action)
        {
            if(action=="Submit")
            {
                CRUDModel mdl = new CRUDModel();
                string Title = frm["txtTitle"];
                string aname = frm["txtAName"];
                double price = Convert.ToDouble(frm["txtPrice"]);
                int rowIns = mdl.NewBookAuthorIdNameAnotherMethod(Title, aname, price);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
      
        public ActionResult IndexOfAuthors()
        {
            CRUDModel mdl = new CRUDModel();
            DataTable dt = mdl.DisplayAuthors();
            return View("IndexOfAuthors", dt);
        }
        public ActionResult InsertAuthorLink()
        {
            return View("CreateAuthor");
        }
        public ActionResult InsertAuthor(FormCollection form,string action)
        {
            if(action=="Submit")
            {
                CRUDModel model =new CRUDModel();
                string AuthorName = form["txtAuthorName"];
                int rowInsert = model.InsertNewAuthor(AuthorName);
                return RedirectToAction("IndexOfAuthors");
            }
            else
            {
                return RedirectToAction("IndexOfAuthors");
            }
        }
        public ActionResult Delete(int Bookid)
        {
            CRUDModel mdl = new CRUDModel();
            mdl.DeleteBook(Bookid);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int BookID)
        {
            CRUDModel mdl = new CRUDModel();
            DataTable dt = mdl.BookByID(BookID);
            return View("Edit",dt);
        }
        public ActionResult Update(FormCollection frm, string action)
        {
            if (action == "Update")
            {
                CRUDModel mdl = new CRUDModel();
                string Title = frm["txtTitle"];
                int aid = Convert.ToInt32(frm["txtAid"]);
                double price = Convert.ToDouble(frm["txtPrice"]);
                int Bookid = Convert.ToInt32(frm["txtBid"]);
                int rowIns = mdl.UpdateBook(Bookid,Title, aid, price);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
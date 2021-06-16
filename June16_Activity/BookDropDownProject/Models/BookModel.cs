using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookDropDownProject.Models
{
    public class BookModel
    {
        public List<SelectListItem> Books { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
    }
}
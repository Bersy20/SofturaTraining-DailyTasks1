using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCusingDPR.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string AuthorID { get; set; }
        public string Price { get; set; }
    }
}
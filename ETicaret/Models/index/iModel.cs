using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models.index
{
    public class iModel
    {
        public List<DB.Products> Products { get; set; }
        public DB.Categories Category { get; set; }
    }
}
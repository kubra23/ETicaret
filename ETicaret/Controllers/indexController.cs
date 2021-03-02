using ETicaret.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class indexController : Controller
    {
        ETicaretDBEntities context;
        public indexController()
        {
            context = new ETicaretDBEntities();
        }

        // GET: index
        public ActionResult Index(int? id)
        {
            IQueryable<DB.Products> products = context.Products;
            DB.Categories category = null;
            if (id.HasValue)
            {
                products = products.Where(x => x.Category_Id == id);
                category = context.Categories.FirstOrDefault(x => x.Id == id);
            }

            var viewModel = new Models.index.iModel()

            { 
             Products = products.ToList(),
             Category = category
            };

            return View(viewModel);
        }
    }
}
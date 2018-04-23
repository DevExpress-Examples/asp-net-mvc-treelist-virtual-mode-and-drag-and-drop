using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Q515371.Models;

namespace Q515371.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult TreeListPartial()
        {
            
            return PartialView("_TreeListPartial");
        }
        public ActionResult MoveNodePartial(int EmployeeID, int? ReportsTo) {

            TreeListVirtualModeHelper.MoveNode(EmployeeID, ReportsTo);
            return PartialView("_TreeListPartial");
        }


    }
}

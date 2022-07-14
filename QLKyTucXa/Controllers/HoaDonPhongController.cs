using Model.EF;
using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
{
    public class HoaDonPhongController : Controller
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        // GET: HoaDonPhong

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TaoHoaDonPhong()
        {
            return View();
        }
    }
}
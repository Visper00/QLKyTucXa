using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace QLKyTucXa.Controllers
{
    public class LichSuController : Controller
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: LichSu
        //public ActionResult Index()
        //{
            
        //    return View();
        //}

        [HttpGet]
        public JsonResult DsNhanVien(string tuKhoa, int trang)
        {
            try
            {

                var dsNhanVien = (from lichsu in db.LICH_SU
                                  join phong in db.PHONGs on lichsu.ID_PHONG equals phong.ID_PHONG
                                  join nhanvien in db.NHANVIENs on lichsu.ID_NHANVIEN equals nhanvien.ID_NHANVIEN
                                  //where (phong.TRANGTHAI == 1)
                                  where (phong.TRANGTHAI == 1)
                                  select new Model.ViewModel()
                                  {
                                      MANV = nhanvien.MANV,
                                      TENNV = nhanvien.TENNV,
                                      NGAYSINH = nhanvien.NGAYSINH,
                                      GIOITINH = nhanvien.GIOITINH,
                                      SDT = nhanvien.SDT,
                                      MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                                      MAPHONG = phong.MAPHONG
                                  }).ToList();
                //30 dòng 40 dòng chẳng hạn ...

                var pageSize = 10;

                var soTrang = dsNhanVien.Count() % pageSize == 0 ? dsNhanVien.Count() / pageSize : dsNhanVien.Count() / pageSize + 1;

                var dsnv = dsNhanVien
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dsNhanVien = dsnv, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }

        }

        [HttpGet]
        public JsonResult AllNV()
        {
            try
            {
                var allNV = (from dp in db.LICH_SU
                    select new
                    {
                        ID_PHONG = dp.ID_PHONG,
                        ID_NhanVien = dp.ID_NHANVIEN
                    }).ToList();
                return Json(new { code = 200, allNV = allNV, msg = "Load danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load danh sách thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(string manv, string maphong)
        {
            try
            {
                var ls = new LICH_SU();
                ls.PHONG.MAPHONG = maphong;
                ls.NHANVIEN.MANV = manv;
                db.LICH_SU.Add(ls);//them doi tuong day phong dc khai bao o phia tren
                db.SaveChanges();//luu vao csdl

                return Json(new { code = 200, msg = "Thêm  mới thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới thất bại. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }









        //// GET: LichSu/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LICH_SU lICH_SU = db.LICH_SU.Find(id);
        //    if (lICH_SU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lICH_SU);
        //}

        //// GET: LichSu/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV");
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG");
        //    return View();
        //}

        //// POST: LichSu/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,ID_NHANVIEN,ID_PHONG,MAPHONGCU,MAPHONGMOI,NGAYCHUYEN,DAXOA")] LICH_SU lICH_SU)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.LICH_SU.Add(lICH_SU);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV", lICH_SU.ID_NHANVIEN);
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", lICH_SU.ID_PHONG);
        //    return View(lICH_SU);
        //}

        //// GET: LichSu/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LICH_SU lICH_SU = db.LICH_SU.Find(id);
        //    if (lICH_SU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV", lICH_SU.ID_NHANVIEN);
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", lICH_SU.ID_PHONG);
        //    return View(lICH_SU);
        //}

        //// POST: LichSu/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,ID_NHANVIEN,ID_PHONG,MAPHONGCU,MAPHONGMOI,NGAYCHUYEN,DAXOA")] LICH_SU lICH_SU)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lICH_SU).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV", lICH_SU.ID_NHANVIEN);
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", lICH_SU.ID_PHONG);
        //    return View(lICH_SU);
        //}

        //// GET: LichSu/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LICH_SU lICH_SU = db.LICH_SU.Find(id);
        //    if (lICH_SU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lICH_SU);
        //}

        //// POST: LichSu/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LICH_SU lICH_SU = db.LICH_SU.Find(id);
        //    db.LICH_SU.Remove(lICH_SU);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

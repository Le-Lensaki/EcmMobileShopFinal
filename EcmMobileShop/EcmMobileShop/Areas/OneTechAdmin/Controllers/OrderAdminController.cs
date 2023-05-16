using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcmMobileShop.Models;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class OrderAdminController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/OrderAdmin
        public ActionResult Index()
        {
            var tb_HOADON = db.tb_HOADON.Include(t => t.tb_KHACHHANG).Include(t => t.tb_TINHTRANGDH);
            return View(tb_HOADON.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/OrderAdmin/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<tb_CHITIETHOADON> tb_CHITIETHOADON = db.tb_CHITIETHOADON.Where(ct => ct.IdHD == id).ToList();
            if (tb_CHITIETHOADON.Count == 0)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(tb_CHITIETHOADON);
        }
        //[Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/OrderAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_HOADON tb_HOADON = db.tb_HOADON.Find(id);
            if (tb_HOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKH = new SelectList(db.tb_KHACHHANG, "IdKH", "TenKH", tb_HOADON.IdKH);
            ViewBag.IdTinhTrangDH = new SelectList(db.tb_TINHTRANGDH, "IdTinhTrangDH", "TenTinhTrang", tb_HOADON.IdTinhTrangDH);
            return View(tb_HOADON);
        }

        // POST: OneTechAdmin/OrderAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdHD,IdTinhTrangDH,IdKH,DiaChiGiao,NgayLap")] tb_HOADON tb_HOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_HOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKH = new SelectList(db.tb_KHACHHANG, "IdKH", "TenKH", tb_HOADON.IdKH);
            ViewBag.IdTinhTrangDH = new SelectList(db.tb_TINHTRANGDH, "IdTinhTrangDH", "TenTinhTrang", tb_HOADON.IdTinhTrangDH);
            return View(tb_HOADON);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetNewOrders()
        {
            // Lấy danh sách đơn hàng mới từ cơ sở dữ liệu
            var newOrders = db.tb_HOADON.Where(o => o.IdTinhTrangDH == 1).ToList();

            return Content(newOrders.Count.ToString());
        }

    }
}

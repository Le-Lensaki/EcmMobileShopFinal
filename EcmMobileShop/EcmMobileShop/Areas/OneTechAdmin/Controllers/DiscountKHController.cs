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
    public class DiscountKHController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountKH
        public ActionResult Index()
        {
            var tb_DISCOUNTKH = db.tb_DISCOUNTKH.Include(t => t.tb_KHACHHANG);
            return View(tb_DISCOUNTKH.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountKH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_DISCOUNTKH tb_DISCOUNTKH = db.tb_DISCOUNTKH.Find(id);
            if (tb_DISCOUNTKH == null)
            {
                return HttpNotFound();
            }
            return View(tb_DISCOUNTKH);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountKH/Create
        public ActionResult Create()
        {
            ViewBag.IdKH = new SelectList(db.tb_KHACHHANG, "IdKH", "TenKH");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/DiscountKH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdDCKH,IdKH,GiaTri,NgayHH,TinhTrang")] tb_DISCOUNTKH tb_DISCOUNTKH)
        {
            if (ModelState.IsValid)
            {
                db.tb_DISCOUNTKH.Add(tb_DISCOUNTKH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKH = new SelectList(db.tb_KHACHHANG, "IdKH", "TenKH", tb_DISCOUNTKH.IdKH);
            return View(tb_DISCOUNTKH);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountKH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_DISCOUNTKH tb_DISCOUNTKH = db.tb_DISCOUNTKH.Find(id);
            if (tb_DISCOUNTKH == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKH = new SelectList(db.tb_KHACHHANG, "IdKH", "TenKH", tb_DISCOUNTKH.IdKH);
            return View(tb_DISCOUNTKH);
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/DiscountKH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdDCKH,IdKH,GiaTri,NgayHH,TinhTrang")] tb_DISCOUNTKH tb_DISCOUNTKH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_DISCOUNTKH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKH = new SelectList(db.tb_KHACHHANG, "IdKH", "TenKH", tb_DISCOUNTKH.IdKH);
            return View(tb_DISCOUNTKH);
        }
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/DiscountKH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_DISCOUNTKH tb_DISCOUNTKH = db.tb_DISCOUNTKH.Find(id);
            if (tb_DISCOUNTKH == null)
            {
                return HttpNotFound();
            }
            return View(tb_DISCOUNTKH);
        }
        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/DiscountKH/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_DISCOUNTKH tb_DISCOUNTKH = db.tb_DISCOUNTKH.Find(id);
            db.tb_DISCOUNTKH.Remove(tb_DISCOUNTKH);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

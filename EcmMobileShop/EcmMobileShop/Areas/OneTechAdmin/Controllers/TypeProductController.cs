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
    public class TypeProductController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/TypeProduct
        public ActionResult Index()
        {
            return View(db.tb_LOAISP.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/TypeProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISP tb_LOAISP = db.tb_LOAISP.Find(id);
            if (tb_LOAISP == null)
            {
                return HttpNotFound();
            }
            return View(tb_LOAISP);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/TypeProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/TypeProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdLoaiSP,TenLoaiSP,TrangThai")] tb_LOAISP tb_LOAISP)
        {
            if (ModelState.IsValid)
            {
                db.tb_LOAISP.Add(tb_LOAISP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_LOAISP);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/TypeProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISP tb_LOAISP = db.tb_LOAISP.Find(id);
            if (tb_LOAISP == null)
            {
                return HttpNotFound();
            }
            return View(tb_LOAISP);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/TypeProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdLoaiSP,TenLoaiSP,TrangThai")] tb_LOAISP tb_LOAISP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_LOAISP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_LOAISP);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/TypeProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISP tb_LOAISP = db.tb_LOAISP.Find(id);
            if (tb_LOAISP == null)
            {
                return HttpNotFound();
            }
            return View(tb_LOAISP);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/TypeProduct/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_LOAISP tb_LOAISP = db.tb_LOAISP.Find(id);
            db.tb_LOAISP.Remove(tb_LOAISP);
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

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
    public class YKIENController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/YKIEN
        public ActionResult Index()
        {
            return View(db.tb_YKIEN.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/YKIEN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_YKIEN tb_YKIEN = db.tb_YKIEN.Find(id);
            if (tb_YKIEN == null)
            {
                return HttpNotFound();
            }
            return View(tb_YKIEN);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/YKIEN/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/YKIEN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdYK,SDT,email,NgayYKien,NoiDung")] tb_YKIEN tb_YKIEN)
        {
            if (ModelState.IsValid)
            {
                db.tb_YKIEN.Add(tb_YKIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_YKIEN);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/YKIEN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_YKIEN tb_YKIEN = db.tb_YKIEN.Find(id);
            if (tb_YKIEN == null)
            {
                return HttpNotFound();
            }
            return View(tb_YKIEN);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/YKIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdYK,SDT,email,NgayYKien,NoiDung")] tb_YKIEN tb_YKIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_YKIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_YKIEN);
        }

        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/YKIEN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_YKIEN tb_YKIEN = db.tb_YKIEN.Find(id);
            if (tb_YKIEN == null)
            {
                return HttpNotFound();
            }
            return View(tb_YKIEN);
        }

        [Authorize(Roles = "Admin")]
        // POST: OneTechAdmin/YKIEN/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            tb_YKIEN tb_YKIEN = db.tb_YKIEN.Find(id);
            db.tb_YKIEN.Remove(tb_YKIEN);
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

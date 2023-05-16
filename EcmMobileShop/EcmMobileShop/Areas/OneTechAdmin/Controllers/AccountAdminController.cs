using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EcmMobileShop.Models;
using Firebase.Auth;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class AccountAdminController : Controller
    {
        EcmMobileShopEntities db = new EcmMobileShopEntities();
        private static string ApiKey = "AIzaSyChvF1RW2IL-N7gbgf_5LHAyOSWT5WK4w0";
        private static string Bucket = "ecmmobileshop-default-rtdb.firebaseio.com";
        
        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "PtNahcVALDcleR71F1jFTbj4j8AM4RED2PamGprQ",

            BasePath = "https://ecmmobileshop-default-rtdb.firebaseio.com/"

        };

        // GET: OneTechAdmin/AccountAdmin
        public ActionResult Index()
        {
            // Khởi tạo client
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            var response = client.Get("Khachhang/");
            Dictionary<string, SignUpModel> data = response.ResultAs<Dictionary<string, SignUpModel>>();

            // Đổ dữ liệu vào List
            List<SignUpModel> listSignUpModel = data.Values.ToList();

            return View(listSignUpModel);
        }

        // GET: OneTechAdmin/AccountAdmin/Details/5
        public ActionResult Details(string id)
        {
            // Khởi tạo client
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            var response = client.Get("Khachhang/");
            Dictionary<string, SignUpModel> data = response.ResultAs<Dictionary<string, SignUpModel>>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignUpModel customer = data.Values.FirstOrDefault(x => x.id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: OneTechAdmin/AccountAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OneTechAdmin/AccountAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<ActionResult> Create([Bind(Include = "id,Name,SDT,Diachi,Email,Password,Roles,Status")] SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                
               dangky(signUpModel);
                return RedirectToAction("Index");
            }

            return View(signUpModel);
        }

        public async void dangky(SignUpModel model)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));

            var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
            using (var db = new EcmMobileShopEntities())
            {

                tb_KHACHHANG kh = new tb_KHACHHANG();
                kh.TenKH = model.Name;
                kh.DiaChi = model.Diachi;
                kh.SDT = model.SDT;
                kh.TrangThai = true;
                db.tb_KHACHHANG.Add(kh);
                db.SaveChanges();
            }
            AddKhachHangToFirebase(model);
        }
        private async void AddKhachHangToFirebase(SignUpModel model)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            var data = model;
            PushResponse response =  client.Push("Khachhang/", data);
            data.id = response.Result.name;
            SetResponse setResponse = client.Set("Khachhang/" + data.id, data);
        }
        private void UpdateKhachHangToFirebase(SignUpModel model)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            var data = model;

            SetResponse setResponse = client.Set("Khachhang/" + data.id, data);
        }

        // GET: OneTechAdmin/AccountAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            // Khởi tạo client
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            var response = client.Get("Khachhang/");
            Dictionary<string, SignUpModel> data = response.ResultAs<Dictionary<string, SignUpModel>>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignUpModel customer = data.Values.FirstOrDefault(x => x.id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: OneTechAdmin/AccountAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit([Bind(Include = "id,Name,SDT,Diachi,Email,Password,Roles,Status")] SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                UpdateKhachHangToFirebase(signUpModel);

                tb_KHACHHANG kh = db.tb_KHACHHANG.SingleOrDefault(c => c.SDT == signUpModel.SDT);

                if (kh != null)
                {
                    // Update các thuộc tính
                    kh.DiaChi = signUpModel.Diachi;
                    kh.TenKH = signUpModel.Name;
                    kh.TrangThai = signUpModel.Status;

                    // Đính kèm đối tượng vào DbContext và xác định rằng nó đã bị thay đổi
                    db.tb_KHACHHANG.Attach(kh);
                    db.Entry(kh).State = EntityState.Modified;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(signUpModel);
        }

        // GET: OneTechAdmin/AccountAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignUpModel signUpModel = db.SignUpModels.Find(id);
            if (signUpModel == null)
            {
                return HttpNotFound();
            }
            return View(signUpModel);
        }

        // POST: OneTechAdmin/AccountAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SignUpModel signUpModel = db.SignUpModels.Find(id);
            db.SignUpModels.Remove(signUpModel);
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

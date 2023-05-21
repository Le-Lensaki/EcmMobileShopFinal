using EcmMobileShop.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Controllers
{
    public class OrdersPaymentController : Controller
    {
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();
        // GET: OrdersPayment
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) // Kiểm tra xem User đã đăng nhập hay chưa
            {
                var user = User.Identity as ClaimsIdentity;
                var addressClaim = user.FindFirst(ClaimTypes.StreetAddress);
                string address = addressClaim != null ? addressClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var phoneClaim = user.FindFirst(ClaimTypes.MobilePhone);
                string phone = phoneClaim != null ? phoneClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var nameClaim = user.FindFirst(ClaimTypes.Name);
                string name = nameClaim != null ? nameClaim.Value : "";
                // Thực hiện đặt hàng với thông tin từ User.Identity
                Orders(name, phone, address,1);
                return RedirectToAction("Cart", "Home");
            }
            tb_KHACHHANG model = new tb_KHACHHANG();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(tb_KHACHHANG model)
        {
            try
            {               
                if (ModelState.IsValid)
                {

                    Orders(model.TenKH, model.SDT, model.DiaChi,1);
                    return View("SuccessView");
                }
                else
                {
                    model = new tb_KHACHHANG();
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }




        public void Orders(string hoten,string sdt,string diachi,int tinhtranghd)
        {
            List<CartItem> shoppingCart = Session["ShoppingCart"] as List<CartItem>;
            tb_KHACHHANG kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == hoten && k.SDT == sdt && k.TrangThai == true);
            if(kh == null)
            {
                kh = new tb_KHACHHANG();
                ecmMobile.tb_KHACHHANG.Add(new tb_KHACHHANG { TenKH = hoten,SDT = sdt, DiaChi = diachi, TrangThai = true });
                ecmMobile.SaveChanges();
                kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == hoten && k.SDT == sdt && k.TrangThai == true);
            }
            tb_HOADON hd = new tb_HOADON();
            hd.tb_KHACHHANG = kh;
            hd.NgayLap = DateTime.Now;
            hd.IdTinhTrangDH = tinhtranghd;
            hd.DiaChiGiao = diachi;
            ecmMobile.tb_HOADON.Add(hd);
            ecmMobile.SaveChanges();
            



            foreach (CartItem item in shoppingCart)
            {
                tb_CT_SANPHAM ctsp = ecmMobile.tb_CT_SANPHAM.Single(ct => ct.IdMau == item.IdMau && ct.IdctSP == item.IdctSP);
                

                    tb_CHITIETHOADON cthd = new tb_CHITIETHOADON();
                cthd.tb_HOADON = hd;
                cthd.tb_CT_SANPHAM = ctsp;
                cthd.TenSP = item.TenSP;
                cthd.SoLuongBan = item.SoLuong;
                cthd.GiaBan = item.Gia;
                ecmMobile.tb_CHITIETHOADON.Add(cthd);

                ctsp.SoLuongSP -= item.SoLuong;

            }
            ecmMobile.SaveChanges();


            Session["ShoppingCart"] = null;
           
        }
        public ActionResult PaymentWithPaypal()
        {
            
            if (User.Identity.IsAuthenticated) // Kiểm tra xem User đã đăng nhập hay chưa
            {
            

                //getting the apiContext  
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                var user = User.Identity as ClaimsIdentity;
                var addressClaim = user.FindFirst(ClaimTypes.StreetAddress);
                string address = addressClaim != null ? addressClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var phoneClaim = user.FindFirst(ClaimTypes.MobilePhone);
                string phone = phoneClaim != null ? phoneClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var nameClaim = user.FindFirst(ClaimTypes.Name);
                string name = nameClaim != null ? nameClaim.Value : "";
                // Thực hiện đặt hàng với thông tin từ User.Identity


                try
                {
                    //A resource representing a Payer that funds a payment Payment Method as paypal  
                    //Payer Id will be returned when payment proceeds or click to pay  
                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {

                        return Redirect(pay(name, phone, address, apiContext));
                    }
                    else
                    {
                        // This function exectues after receving all parameters for the payment  
                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                        //If executed payment failed then we will show payment failure message to user  
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return View("FailureView");
                        }
                    }
                    Orders(name, phone, address, 3);
                    return View("SuccessView");
                    //userDao.ChangeVip(Convert.ToInt32(session.UserID));
                    //on successful payment, show success page to user.      
                }
                catch (Exception ex)
                {
                    return View("FailureView");
                }
            }
            tb_KHACHHANG model = new tb_KHACHHANG();
            return View(model);
        }




        private string pay(string hoten, string sdt, string diachi, APIContext apiContext)
        {
            //this section will be executed first because PayerID doesn't exist  
            //it is returned by the create function call of the payment class  
            // Creating a payment  
            // baseURL is the url on which paypal sendsback the data.  
            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/OrdersPayment/PaymentWithPayPal?";
            //here we are generating guid for storing the paymentID received in session  
            //which will be used in the payment execution  
            var guid = Convert.ToString((new Random()).Next(100000));
            //CreatePayment function gives us the payment approval url  
            //on which payer is redirected for paypal account payment  
            var createdPayment = this.CreatePayment(hoten, sdt, diachi, apiContext, baseURI + "guid=" + guid);
            //get links returned from paypal in response to Create function call  
            var links = createdPayment.links.GetEnumerator();
            string paypalRedirectUrl = null;
            while (links.MoveNext())
            {
                Links lnk = links.Current;
                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                {
                    //saving the payapalredirect URL to which user will be redirected for payment  
                    paypalRedirectUrl = lnk.href;
                }
            }
            // saving the paymentID in the key guid  
            Session.Add(guid, createdPayment.id);
            return paypalRedirectUrl;
        }



        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(string hoten, string sdt, string diachi,APIContext apiContext, string redirectUrl)
         {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            List<CartItem> shoppingCart = Session["ShoppingCart"] as List<CartItem>;

            foreach (var item in shoppingCart)
            {
                //Adding Item Details like name, currency, price etc  
                itemList.items.Add(new Item()
                {
                    name = item.TenSP,
                    currency = "USD",
                    price = item.Gia.ToString(),
                    quantity = item.SoLuong.ToString(),
                    sku = "Loai"+item.IdLoaiSP.ToString(),
                });
            }
            
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            double sum = 0;
            foreach (var item in shoppingCart)
            {
                sum += (item.Gia ?? 0) * item.SoLuong;
            }
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = sum.ToString(),
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = sum.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details,
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Thanh toán hoá đơn",
                invoice_number = Convert.ToString((new Random()).Next(100000)), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }




        public ActionResult FormHistory()
        {
            tb_KHACHHANG kh = new tb_KHACHHANG();
            if (User.Identity.IsAuthenticated) // Kiểm tra xem User đã đăng nhập hay chưa
            {
                var user = User.Identity as ClaimsIdentity;
                var addressClaim = user.FindFirst(ClaimTypes.StreetAddress);
                string address = addressClaim != null ? addressClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var phoneClaim = user.FindFirst(ClaimTypes.MobilePhone);
                string phone = phoneClaim != null ? phoneClaim.Value : "";

                // Lấy thông tin số điện thoại của user
                var nameClaim = user.FindFirst(ClaimTypes.Name);
                string name = nameClaim != null ? nameClaim.Value : "";

                kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == name && k.SDT == phone && k.TrangThai == true);

                return RedirectToAction("HistoryOrders", "OrdersPayment", new { idkh = kh.IdKH });
            }
            return View(kh);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> FormHistory(tb_KHACHHANG model)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    tb_KHACHHANG kh = ecmMobile.tb_KHACHHANG.SingleOrDefault(k => k.TenKH == model.TenKH && k.SDT == model.SDT && k.TrangThai == true);
                    return RedirectToAction("HistoryOrders", "OrdersPayment",new { idkh = kh.IdKH});

                }else
                {
                    model = new tb_KHACHHANG();
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        public ActionResult HistoryOrders(int idkh)
        {
            if(idkh == 0)
            {
                return RedirectToAction("Index", "Home");
            }    
            List<tb_HOADON> hd = ecmMobile.tb_HOADON.Where(p => p.IdKH == idkh).ToList();

            return View(hd);
        }

        public ActionResult Details(int ordersID)
        {
            List<tb_CHITIETHOADON> cthd = ecmMobile.tb_CHITIETHOADON.Where(p => p.IdHD == ordersID).ToList();


            return View(cthd);
        }

        public ActionResult Feedback(int IdctHD)
        {
            tb_FEEDBACK a = new tb_FEEDBACK();
            a.IdctHD = IdctHD;
            return View(a);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Feedback(tb_FEEDBACK model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    model.NgayFB = DateTime.Now;
                    ecmMobile.tb_FEEDBACK.Add(model);
                    ecmMobile.SaveChanges();
                    tb_CHITIETHOADON a = ecmMobile.tb_CHITIETHOADON.SingleOrDefault(mc => mc.IdctHD == model.IdctHD);

                    return RedirectToAction("Details", "OrdersPayment",new { ordersID = a.IdHD});
                }
                else
                {
                    model = new tb_FEEDBACK();
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
    }
}
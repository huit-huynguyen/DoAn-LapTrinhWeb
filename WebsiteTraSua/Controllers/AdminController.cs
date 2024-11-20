using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows;
using WebsiteTraSua.Models;

namespace WebsiteTraSua.Controllers
{
    public class AdminController : Controller
    {

        static string strcon = "Data Source=.;Initial Catalog=QuanLiTraSua;User ID=sa;Password=123";
        DatabaseDataContext db = new DatabaseDataContext(strcon);

        public ActionResult TrangChu()
        {
            List<DonHang> listDonHangMoi = db.DonHangs.Where(t => t.TrangThai == "Mới").ToList();

            Dictionary<int, List<string>> orderDetails = new Dictionary<int, List<string>>();

            foreach (var order in listDonHangMoi)
            {
                var details = new List<string>();
                var chiTietDonHangs = db.ChiTietDonHangs.Where(ct => ct.MaDH == order.MaDH);

                foreach (var chiTiet in chiTietDonHangs)
                {
                    var sanPhams = db.SanPhams.FirstOrDefault(d => d.MaSP == chiTiet.MaSP);
                    var toppings = db.ChiTietToppings
                                    .Where(t => t.MaCTDH == chiTiet.MaCTDH)
                                    .Join(db.Toppings,
                                          ct => ct.MaTopping,
                                          t => t.MaTopping,
                                          (ct, t) => t.TenTopping)
                                    .ToList();

                    string itemDetail = sanPhams.TenSP;
                    if (toppings.Any())
                    {
                        itemDetail += $" ({string.Join(", ", toppings)})";
                    }
                    details.Add(itemDetail);
                }

                orderDetails[order.MaDH] = details;
            }

            ViewBag.OrderDetails = orderDetails;

            return View(listDonHangMoi); ;
        }

        //Sản Phẩm: Xem, thêm, sửa, xóa
        public ActionResult SanPham() //xem
        {
            List<SanPham> listSanPham = db.SanPhams.ToList();
            return View(listSanPham);
        }

        public ActionResult ThemSanPham() //them
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.InsertOnSubmit(sanPham);
                db.SubmitChanges();
                MessageBox.Show("Thêm sản phẩm thành công");
                return RedirectToAction("SanPham");

            }

            return View(sanPham);
        }

        public ActionResult SuaSanPham(int? id) //sua
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == sanPham.MaSP);
                sp.TenSP = sanPham.TenSP;
                sp.Gia = sanPham.Gia;
                sp.MoTa = sanPham.MoTa;
                sp.HinhAnh = sanPham.HinhAnh;
                sp.MaSP = sanPham.MaSP;
                db.SubmitChanges();
                MessageBox.Show("Sửa sản phẩm thành công");
                return RedirectToAction("SanPham");

            }

            return View(sanPham);
        }

        public ActionResult XoaSanPham(int? id) //xoa
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaSanPham(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.DeleteOnSubmit(sanPham);
                db.SubmitChanges();
                MessageBox.Show("Xóa sản phẩm thành công");
                return RedirectToAction("SanPham");

            }

            return View(sanPham);
        }

        //Topping: Xem, thêm, sửa, xóa
        public ActionResult Topping() //xem
        {
            List<Topping> listTopping = db.Toppings.ToList();
            return View(listTopping);
        }

        public ActionResult ThemTopping() //them
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemTopping(Topping topping)
        {
            if (ModelState.IsValid)
            {
                db.Toppings.InsertOnSubmit(topping);
                db.SubmitChanges();
                MessageBox.Show("Thêm topping thành công");
                return RedirectToAction("Topping");

            }

            return View(topping);
        }

        public ActionResult SuaTopping(int? id) //sua
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Topping topping = db.Toppings.SingleOrDefault(n => n.MaTopping == id);
            if (topping == null)
            {
                return HttpNotFound();
            }
            return View(topping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaTopping(Topping topping)
        {
            if (ModelState.IsValid)
            {
                Topping tp = db.Toppings.SingleOrDefault(n => n.MaTopping == topping.MaTopping);
                tp.TenTopping = topping.TenTopping;
                tp.Gia = topping.Gia;
                tp.MaTopping = topping.MaTopping;
                db.SubmitChanges();
                MessageBox.Show("Sửa topping thành công");
                return RedirectToAction("Topping");

            }

            return View(topping);
        }

        public ActionResult XoaTopping(int? id) //xoa
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Topping topping = db.Toppings.SingleOrDefault(n => n.MaTopping == id);
            if (topping == null)
            {
                return HttpNotFound();
            }
            return View(topping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaTopping(Topping topping)
        {
            if (ModelState.IsValid)
            {
                db.Toppings.DeleteOnSubmit(topping);
                db.SubmitChanges();
                MessageBox.Show("Xóa topping thành công");
                return RedirectToAction("Topping");

            }

            return View(topping);
        }

        //Khách hàng: Chỉ cho xem
        public ActionResult KhachHang()
        {
            List<KhachHang> listKhachHang = db.KhachHangs.ToList();
            return View(listKhachHang);
        }


        //Đơn hàng: Chỉ cho xem
        public ActionResult DonHang()
        {
            List<DonHang> listDonHang = db.DonHangs.ToList();
            return View(listDonHang);
        }

        public ActionResult BaoCao()
        {
            List<DonHang> listDonHang = db.DonHangs.Where(d => d.NgayDat == System.DateTime.Today).ToList();

            if (listDonHang.Count == 0)
            {
                ViewBag.Total = 0;
                return View(listDonHang);
            }
            else
            {
                decimal total = db.DonHangs.Where(d => d.NgayDat == System.DateTime.Today).Sum(d => d.TongTien);
                ViewBag.Total = total;
            }

            return View(listDonHang);
        }
    }
}

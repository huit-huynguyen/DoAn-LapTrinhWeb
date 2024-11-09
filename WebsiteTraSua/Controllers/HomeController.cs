using System.Web.Mvc;

namespace WebsiteTraSua.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // GET: Home/TrangChu
        public ActionResult TrangChu()
        {
            return View();
        }

        // GET: Home/DanhMuc
        public ActionResult DanhMuc()
        {
            return View();
        }

        // GET: Home/ThongTin
        public ActionResult ThongTin()
        {
            return View();
        }

        // GET: Home/DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        // GET: Home/DangKy
        public ActionResult DangKy()
        {
            return View();
        }

        // GET: Home/GioHang
        public ActionResult GioHang()
        {
            return View();
        }
    }
}

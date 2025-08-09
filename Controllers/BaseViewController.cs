using Microsoft.AspNetCore.Mvc;

namespace account_web.Controllers
{
    public class BaseViewController : Controller
    {
        // 檢查用戶是否已登入
        protected bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }
        // 重定向到登入頁面
        protected IActionResult RedirectToLogin()
        {
            TempData["ErrorMessage"] = "請先登入以訪問此功能";
            return RedirectToAction("Login", "Auth");
        }
    }
}

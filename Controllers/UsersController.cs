using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using account_web.Data;
using account_web.Models;
using account_web.Services;

namespace account_web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserServices _userServices;

        public UsersController(UserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _userServices.GetUsers());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userServices.GetUserByUserId(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Password,FactoryId")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userServices.InsertUser(user);
                TempData["SuccessMessage"] = "使用者建立成功！";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userServices.GetUserByUserId(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, [Bind("UserId,Name,FactoryId")] User user)
        {
            try
            {
                await _userServices.UpdateUserByUserId(user);
                TempData["SuccessMessage"] = "使用者更新成功！";
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userServices.GetUserByUserId(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            try
            {
                System.Console.WriteLine($"DeleteConfirmed: {userId}");
                await _userServices.DeleteUser(userId);
                TempData["SuccessMessage"] = "使用者刪除成功！";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Users/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string userId, string currentPassword, string newPassword, string confirmPassword)
        {
            try
            {
                // 驗證新密碼確認
                if (newPassword != confirmPassword)
                {
                    TempData["ErrorMessage"] = "新密碼與確認密碼不符！";
                    return RedirectToAction(nameof(Edit), new { userId = userId });
                }

                // 驗證目前密碼
                var user = await _userServices.GetUserByUserId(userId);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "使用者不存在！";
                    return RedirectToAction(nameof(Index));
                }

                if (user.Password != currentPassword)
                {
                    TempData["ErrorMessage"] = "目前密碼錯誤！";
                    return RedirectToAction(nameof(Edit), new { userId = userId });
                }

                // 更新密碼
                await _userServices.UpdatePassword(userId, newPassword);
                TempData["SuccessMessage"] = "密碼修改成功！";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"密碼修改失敗：{ex.Message}";
            }

            return RedirectToAction(nameof(Edit), new { userId = userId });
        }
    }
}

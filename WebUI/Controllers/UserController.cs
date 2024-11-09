using Business.Services;
using Core.Hash;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Login_Service;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserTableService _userTableService;
        public UserController(IUserTableService userTableService)
        {
            _userTableService = userTableService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["users"] = await _userTableService.GetListAsync();
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Insert()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> InsertMethod(string name, string surname, string username, string password)
        {
            try
            {
                var res = await _userTableService.GetListAsync(x => x.UserName == username);
                if (res == null || res.Count == 0)
                {
                    string salt = HashHelper.GenerateSalt(); // Rastgele salt oluştur
                    string hash = HashHelper.GenerateHash(password, salt); // Hash oluştur

                    var rest = await _userTableService.AddOrUpdateAsync(new UserTableDto() { Name = name, SurName = surname, UserName = username, HashPassword = hash, SaltPassword = salt });

                    if (rest != null && rest.UserName != null)
                    {
                        return RedirectToAction("Success", "Result", new { message = "Account Created" });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Result");
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Result", new { message = "This username is used" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Result");
            }

        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Auth(string usrname, string password)
        {
            try
            {
                await new Services().Logoff(HttpContext);
                var res = await _userTableService.GetListAsync(x => x.UserName == usrname);
                if (res != null && res.Count == 1)
                {
                    bool isPasswordValid = HashHelper.VerifyPassword(password, res.FirstOrDefault().HashPassword, res.FirstOrDefault().SaltPassword);
                    if (isPasswordValid)
                    {
                        await new Services().Login(HttpContext, res.FirstOrDefault().Id.ToString(), "admin");
                        return RedirectToAction("Success", "Result", new { message = "Login successful!" });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Result", new { message = "Password is wrong!" });
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Result");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Result");
            }

        }
        public async Task<IActionResult> Logoff(string usrname, string password)
        {
            try
            {
                await new Services().Logoff(HttpContext);
                return RedirectToAction("Success", "Result", new { message = "Logout successful!" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Result");
            }

        }

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {

                var res = await _userTableService.GetByIdAsync(id);
                if (res != null && res.UserName != null)
                {
                    await _userTableService.DeleteAsync(res);

                    return RedirectToAction("Success", "Result");

                }
                else
                {
                    return RedirectToAction("Error", "Result");
                }
            }
            catch
            {
                return RedirectToAction("Error", "Result");

            }
        }

        [Authorize]
        public async Task<IActionResult> Update(Guid id)
        {

            return View(await _userTableService.GetByIdAsync(id));
        }
        [Authorize]
        public async Task<IActionResult> UpdateMethod(UserTableDto userTableDto,string password)
        {
            try
            {
                var rest = await _userTableService.GetByIdAsync(userTableDto.Id);
                var res = await _userTableService.GetListAsync(x => x.UserName == userTableDto.UserName);
                if (rest.UserName == userTableDto.UserName || res.Count == 0)
                {

                    string salt = HashHelper.GenerateSalt(); // Rastgele salt oluştur
                    string hash = HashHelper.GenerateHash(password, salt); // Hash oluştur

                    userTableDto.SaltPassword = salt;
                    userTableDto.HashPassword = hash;

                    var model = await _userTableService.AddOrUpdateAsync(userTableDto);

                    if (model != null && model.UserName != null)
                    {
                        return RedirectToAction("Success", "Result", new { message = "Account Updated" });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Result");
                    }

                }
                else
                {
                    return RedirectToAction("Error", "Result", new { message = "This username is used" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Result");
            }

        }

    }
}

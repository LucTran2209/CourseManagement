using CMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMS_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _apiUrl = "https://localhost:7175/api/Authen/login";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string? email, string? pass)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(_apiUrl + "/" + email + "/" +pass))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();
                              
                                User u = JsonConvert.DeserializeObject<User>(data);
                              
                                HttpContext.Session.SetString("UserName", u.FirstName + " " + u.LastName);

                                HttpContext.Session.SetInt32("UserId", u.UserId);

                                HttpContext.Session.Remove("ValidateLogin");

                                var backlink = HttpContext.Session.GetString("backlink");
                                if (!string.IsNullOrEmpty(backlink))
                                {
                                    return Redirect("/"+backlink);
                                }
                                return Redirect("/Home");
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("email", email);
                            HttpContext.Session.SetString("ValidateLogin", "*Email or Password is not correct!");
                            return Redirect("index");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("index");
        }
    }
}

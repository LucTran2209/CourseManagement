using CMS_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMS_MVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly string _apiUrl = "https://localhost:7175/api/course";

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetListCourseAsync(string? title)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(_apiUrl + "/GetAllCourse" + "?title=" + title))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<Course> list = JsonConvert.DeserializeObject<List<Course>>(data);

                                if (list != null && list.Count() != 0)
                                {

                                    HttpContext.Session.SetString("Search", title == null ? string.Empty : title);
                                    HttpContext.Session.SetInt32("Result", list.Count());
                                    ViewBag.ListCourse = list;
                                    HttpContext.Session.Remove("Result1");
                                }
                                else
                                {
                                    ViewBag.ListCourse = list;
                                    HttpContext.Session.SetInt32("Result", 0);
                                    HttpContext.Session.SetString("Result1", "No Course was found!");
                                }
                                return View();
                            }
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }

        public async Task<IActionResult> MyCoursesAsync(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(_apiUrl + "/GetAllMyCourseCreate/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<Course> list = JsonConvert.DeserializeObject<List<Course>>(data);

                                if (list != null && list.Count() != 0)
                                {                                 
                                    ViewBag.ListCourse = list;                                
                                }
                                else
                                {
                                    ViewBag.ListCourse = list;
                                    HttpContext.Session.SetInt32("Result", 0);
                                    HttpContext.Session.SetString("Result1", "You have not approven any course");
                                }
                                return View();
                            }
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View(ex); 
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EnrollCourseAsync(int courseId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                HttpContext.Session.SetString("backlink", "course/enrollcourse?courseid=" + courseId);
                return Redirect("../Login");               
            }

            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(_apiUrl + "/GetCourseById/" + courseId))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<Course> list = JsonConvert.DeserializeObject<List<Course>>(data);

                                if (list != null && list.Count() != 0)
                                {                                   
                                    ViewBag.ListCourse = list;                                  
                                }                            
                                return View();
                            }
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }            
        }

        public async Task<IActionResult> Enroll(int courseId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (HttpContext.Session.GetInt32("UserId") != null)
                    {
                        var enroll = new Enroll();
                        enroll.CourseId = courseId;
                        enroll.UserId = (int)HttpContext.Session.GetInt32("UserId");
                        enroll.TimeJoin = DateTime.Now;
                        enroll.Creator = false;

                        using (var response = await client.PostAsJsonAsync(_apiUrl + "/enrollcourse", enroll))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return Redirect("");
                            }
                            else
                            {
                                return View();
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/Login");
                    }
                    
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }

        // Create course
        public async Task<IActionResult> CreateNewCourseAsync()
        {

            using (var client = new HttpClient())
            {
                try
                {
                    string url = "https://localhost:7175/api/category";

                    using (var response = await client.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<Category> list = JsonConvert.DeserializeObject<List<Category>>(data);

                                if (list != null && list.Count() != 0)
                                {
                                    ViewBag.Categories = list;
                                }
                                HttpContext.Session.Remove("ResultCreate");
                                return View();
                            }
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }

        public async Task<IActionResult> CreateAsync(int userId, string courseTitle, string semester, DateTime timeStart, DateTime timeEnd, int categoryId)
        {
            CreateNewCourse course = new CreateNewCourse();
            course.UserId = userId;
            course.CourseTitle = courseTitle;
            course.Semester = semester;
            course.TimeStart = timeStart;
            course.TimeEnd = timeEnd;
            course.CategoryId = categoryId;

            using (var client = new HttpClient())
            {
                try
                {
                    if (HttpContext.Session.GetInt32("UserId") != null)
                    {                       
                        using (var response = await client.PostAsJsonAsync(_apiUrl, course))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                HttpContext.Session.SetString("ResultCreate", "A New Course has been created");
                                return Redirect("CreateNewCourse");
                            }
                            else
                            {
                                return View();
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/Login");
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }

        }


    }
}

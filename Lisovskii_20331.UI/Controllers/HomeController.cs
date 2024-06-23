using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lisovskii_20331.UI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;

namespace Lisovskii_20331.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        List<ListDemo> items = new List<ListDemo>
        {
            new ListDemo { Id=1, Name="Item 1"},
            new ListDemo { Id=2, Name="Item 2"},
            new ListDemo { Id=3, Name="Item 3"}
        };

        public ActionResult Index()
        {
            ViewData["LabStr"] = "Лабараторная работа 9";
            ViewData["Items"] = new SelectList(items, "Id", "Name");
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

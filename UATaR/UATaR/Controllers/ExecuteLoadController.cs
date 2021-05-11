using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UATaR.Controllers
{
    public class ExecuteLoadController : Controller
    {
        // GET: ExecuteLoadController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExecuteLoadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExecuteLoadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExecuteLoadController/Create
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

        // GET: ExecuteLoadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExecuteLoadController/Edit/5
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

        // GET: ExecuteLoadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExecuteLoadController/Delete/5
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

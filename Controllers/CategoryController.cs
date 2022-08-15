using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objLst = _db.Category;

            return View(objLst);
        }

        //Get - Create 
        public IActionResult Create()
        {
            return View();
        }

        //POST - Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Category obj)
        {
            if (ModelState.IsValid )
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get - Edit 
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
               return NotFound();
            }

            return View(obj);
        }

        //POST - Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get - Delete 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
               return NotFound();
            }

            return View(obj);
        }

        //POST - Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }  
                _db.Category.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
           
        }

    }

}

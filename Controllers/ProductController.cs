using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Data;
using Store.Models;
using Store.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment )
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objLst = _db.product.Include(u=>u.Category).Include(u=>u.ApplicationType);

            //IEnumerable<Product> objLst = _db.product;
            //foreach(var obj in objLst)
            //{
            //    obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            //    obj.ApplicationType = _db.ApplicationType.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
            //}

            return View(objLst);
        }

        //Get - UPSERT 
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem 
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});
            //ViewData["CategoryDropDown"] = CategoryDropDown;

            ProductVM productVM = new ProductVM()
            {
                product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                //this is for create
                return View(productVM);
            }
            else
            {  
                productVM.product  = _db.product.Find(id);
                if (productVM.product  == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //POST - Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM )
        {
            if (ModelState.IsValid )
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.product.Id == 0 )
                {
                    //Creating 
                    string upload = webRootPath + WC.ImagePath;
                    string filename = Guid.NewGuid().ToString();
                    string extention = Path.GetExtension(files[0].FileName);

                    using(var fileStream = new FileStream(Path.Combine(upload,filename+extention),FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.product.Image = filename + extention;

                    _db.product.Add(productVM.product);
                }
                else
                {
                    //Updating
                    //fetch the product from DB (because if image doesn't change we have to replace the existing image name)
                    var objFromDb = _db.product.AsNoTracking().FirstOrDefault(u => u.Id == productVM.product.Id);

                    if (files.Count>0) //It means that a new file is selected
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string filename = Guid.NewGuid().ToString();
                        string extention = Path.GetExtension(files[0].FileName);

                        //remove old file
                        var oldFile = Path.Combine(upload, objFromDb.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        //use new file
                        using (var fileStream = new FileStream(Path.Combine(upload, filename + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.product.Image = filename + extention;
                    }
                    else
                    {
                        //it means that image is not changed
                        productVM.product.Image = objFromDb.Image;
                    }
                    _db.product.Update(productVM.product);

                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            productVM.ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(productVM);  
        }
         
        //Get - Delete 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.product.Include(u => u.Category).Include(u=>u.ApplicationType).FirstOrDefault(u => u.Id == id);
            //product.Category = _db.Category.Find(product.CategoryId);
            if (product == null)
            {
               return NotFound();
            }

            return View(product);
        }

        //POST - Delete 
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.product.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

            //remove old file
            var oldFile = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
           
        }

    }

}

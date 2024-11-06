using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using novSocial_media.Data;
using novSocial_media.Models;
namespace novSocial_media.Controllers
{
    public class CategoryController : Controller // this class inherit the base Controller class from asp.net mvc
    {
        private readonly ApplicationDbContext _db;// Field of type ApplicationDbContext & _db holds the implementation of CategoryController
        public CategoryController(ApplicationDbContext db) //We want an implementation of application DB Context.
        {
            _db = db; //assign service to _db allow to use ApplicationDbContext methods
        }
        public IActionResult Index()
        {
            List<Category>  objcategoryList = _db.Categories.ToList();
            return View(objcategoryList);
        }
        public IActionResult Create()
        {
            return View(); // get action
        }
        [HttpPost]
        public IActionResult Create(Category obj) //to add this category object to the category table.
        {
            if(obj.Name == obj.DisplayOrder.ToString()) // name = 55 & order 55 then show what
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");  
            }
            if( obj.Name != null && obj.Name.ToLower() == "test")// tag halper asp-validation None, model-only show only model defined error  @ custom validation
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }

            if (ModelState.IsValid) // server side validation
            {
                _db.Categories.Add(obj); // pass obj of Category type.
                _db.SaveChanges(); // go to db save changes
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); // after add go view all category
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category? categorydb = _db.Categories.Find(id);
            if (categorydb == null)
            {
                return NotFound();
            }
            return View(categorydb);
         
        }
        [HttpPost]
        public IActionResult Edit(Category obj) //to add this category object to the category table.
        {
   
            if (ModelState.IsValid) // server side validation
            {
                _db.Categories.Update(obj); // pass obj of Category type.
                _db.SaveChanges(); // go to db save changes
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); // after add go view all category
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categorydb = _db.Categories.Find(id);
            if (categorydb == null)
            {
                return NotFound();
            }
            return View(categorydb);

        }
        [HttpPost , ActionName("Delete") ]
        public IActionResult DeletePOST(int? id) //to add this category object to the category table.
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges(); // save chages agter delete
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); // after delete go view all category

        }
    }

}

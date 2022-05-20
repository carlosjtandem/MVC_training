using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }

        //************************ CREATe *******
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The displayOrder cannot match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Index","OtherController");  // If you want to redirect to another controller and method
            }
            return View(obj);

        }
        //************************ EDIT *******
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            //var categoryFromDBFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDBSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDB == null) {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The displayOrder cannot match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Index","OtherController");  // If you want to redirect to another controller and method
            }
            return View(obj);
        }

        //************************ DELETE *******
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            //var categoryFromDBFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDBSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDB = _db.Categories.Find(id);
            //var categoryFromDBFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDBSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFromDB);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}

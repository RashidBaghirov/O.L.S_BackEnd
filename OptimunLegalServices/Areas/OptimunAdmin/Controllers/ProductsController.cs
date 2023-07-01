﻿//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//using System.Xml.Linq;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using OptimunLegalServices.DAL;
//using OptimunLegalServices.Entities;

//namespace Papa_Jhons.Areas.AdminAreas.Controllers
//{
//    //[Area("AdminAreas")]
//    //[Authorize(Roles = "Admin,Moderator")]

//    public class ProductsController : Controller
//    {
//        private readonly OptimunDbContext _context;
//        private readonly IWebHostEnvironment _env;

//        public ProductsController(OptimunDbContext context, IWebHostEnvironment env)
//        {
//            _context = context;
//            _env = env;
//        }
//        public IActionResult Index(int page = 1)
//        {
//            ViewBag.TotalPage = Math.Ceiling((double)_context.Blogs.Count() / 5);
//            ViewBag.CurrentPage = page;

//            IEnumerable<Blog> blogs = _context.Blogs.AsNoTracking().Skip((page - 1) * 5).Take(5).AsEnumerable();

//            return View(blogs);
//        }


//        //[HttpPost]
//        //public IActionResult Index(string search, int page = 1)
//        //{
//        //    ViewBag.TotalPage = Math.Ceiling((double)_context.Products.Count() / 5);
//        //    ViewBag.CurrentPage = page;

//        //    IEnumerable<Product> products = _context.Products.Include(p => p.Category).Include(x => x.PizzaCategory)
//        //                                                 .AsNoTracking().Skip((page - 1) * 5).Take(5).AsEnumerable();
//        //    if (!string.IsNullOrEmpty(search))
//        //    {
//        //        products = products.Where(x => x.Name.ToLower().StartsWith(search.ToLower().Substring(0, Math.Min(search.Length, 3)))).ToList();
//        //    }

//        //    return View(products);
//        //}

//    //    public IActionResult Create()
//    //    {
//    //        ViewBag.Category = _context.Categories.AsEnumerable();
//    //        ViewBag.PizzaCategory = _context.PizzaCategory.AsEnumerable();
//    //        return View();

//    //    }

//    //    [HttpPost]
//    //    public async Task<IActionResult> Create(Product newProduct)
//    //    {
//    //        ViewBag.Category = _context.Categories.AsEnumerable();
//    //        ViewBag.PizzaCategory = _context.PizzaCategory.AsEnumerable();
//    //        if (!ModelState.IsValid)
//    //        {
//    //            foreach (string message in ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage))
//    //            {
//    //                ModelState.AddModelError("", message);
//    //            }
//    //        }
//    //        if (!newProduct.Image.IsValidFile("image/") || !newProduct.Image.IsValidFile("image/"))
//    //        {
//    //            ModelState.AddModelError(string.Empty, "Please choose image file");
//    //            return View();
//    //        }
//    //        Product product = new()
//    //        {
//    //            Name = newProduct.Name,
//    //            Desc = newProduct.Desc,
//    //            Price = newProduct.Price,
//    //            CategoryId = newProduct.CategoryId

//    //        };
//    //        if (newProduct.PizzaCategory is not null)
//    //        {
//    //            product.PizzaCategoryId = newProduct.PizzaCategoryId;
//    //        }
//    //        var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
//    //        product.ImagePath = await newProduct.Image.CreateImage(imagefolderPath, "Products");



//    //        _context.Products.Add(product);
//    //        _context.SaveChanges();
//    //        return RedirectToAction(nameof(Index));
//    //    }

//    //    public IActionResult Edit(int id)
//    //    {
//    //        if (id == 0) return Redirect("~/Error/Error");
//    //        Product product = _context.Products.Include(x => x.Category).Include(x => x.PizzaCategory).FirstOrDefault(x => x.Id == id);
//    //        ViewBag.Category = _context.Categories.AsEnumerable();
//    //        ViewBag.PizzaCategory = _context.PizzaCategory.AsEnumerable();

//    //        if (product is null) return Redirect("~/Error/Error");
//    //        return View(product);
//    //    }
//    //    [HttpPost]
//    //    public async Task<IActionResult> Edit(int id, Product edited)
//    //    {
//    //        Product product = _context.Products.Include(x => x.Category).Include(x => x.PizzaCategory).FirstOrDefault(x => x.Id == id);
//    //        ViewBag.Category = _context.Categories.AsEnumerable();
//    //        ViewBag.PizzaCategory = _context.PizzaCategory.AsEnumerable();

//    //        if (product is null) return Redirect("~/Error/Error");




//    //        if (edited.Image is not null)
//    //        {
//    //            if (!edited.Image.IsValidFile("image/"))
//    //            {
//    //                ModelState.AddModelError(string.Empty, "Please choose image file");
//    //                return View();
//    //            }
//    //            if (!edited.Image.IsValidLength(2))
//    //            {
//    //                ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 2MB");
//    //                return View();
//    //            }
//    //            await AdjustPlantPhoto(edited.Image, product);
//    //        }


//    //        product.Name = edited.Name;
//    //        product.Desc = edited.Desc;
//    //        product.Price = edited.Price;
//    //        product.CategoryId = edited.CategoryId;
//    //        product.PizzaCategoryId = edited.PizzaCategoryId;
//    //        _context.SaveChanges();
//    //        return RedirectToAction(nameof(Index));
//    //    }

//    //    private async Task AdjustPlantPhoto(IFormFile image, Product product)
//    //    {
//    //        var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
//    //        string filepath = Path.Combine(imagefolderPath, "Products", product.ImagePath);
//    //        ExtensionMethods.DeleteImage(filepath);
//    //        product.ImagePath = await image.CreateImage(imagefolderPath, "Products");
//    //    }




//    //    public IActionResult Details(int id)
//    //    {
//    //        if (id == 0) return Redirect("~/Error/Error");
//    //        Product product = _context.Products.Include(x => x.Category).Include(x => x.PizzaCategory).FirstOrDefault(x => x.Id == id);
//    //        ViewBag.Category = _context.Categories.AsEnumerable();
//    //        ViewBag.PizzaCategory = _context.PizzaCategory.AsEnumerable();

//    //        if (product is null) return Redirect("~/Error/Error");
//    //        return View(product);
//    //    }

//    //    public IActionResult Delete(int id)
//    //    {
//    //        if (id == 0) return Redirect("~/Error/Error");
//    //        Product product = _context.Products.Include(x => x.Category).Include(x => x.PizzaCategory).FirstOrDefault(x => x.Id == id);
//    //        ViewBag.Category = _context.Categories.AsEnumerable();
//    //        ViewBag.PizzaCategory = _context.PizzaCategory.AsEnumerable();

//    //        if (product is null) return Redirect("~/Error/Error");
//    //        return View(product);
//    //    }

//    //    [HttpPost]
//    //    public IActionResult Delete(int id, Product delete)
//    //    {
//    //        if (id != delete.Id) return Redirect("~/Error/Error");
//    //        Product? product = _context.Products.FirstOrDefault(s => s.Id == id);
//    //        if (product is null) return Redirect("~/Error/Error");

//    //        var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
//    //        string filepath = Path.Combine(imagefolderPath, "Products", product.ImagePath);
//    //        ExtensionMethods.DeleteImage(filepath);

//    //        _context.Products.Remove(product);
//    //        _context.SaveChanges();
//    //        return RedirectToAction(nameof(Index));
//    //    }
//    //}

//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels.Home;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Product> products = _context.Products.Where(p=>!p.IsDeleted).Include(p=>p.ProductImages).Take(4).AsQueryable();
            HomeVM home = new HomeVM { Sliders= _context.Sliders.OrderBy(s=>s.Order), Sponsors = _context.Sponsors };
            return View(home);
        }
        public IActionResult LoadProducts(int skip, int take)
        {
            return Json(_context.Products.Where(p=>!p.IsDeleted).Include(p=>p.ProductImages).Skip(skip).Take(take));
        }
    }
}

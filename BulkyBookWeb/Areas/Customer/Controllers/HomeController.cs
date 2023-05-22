using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(
         string sortOrder,
         string currentFilter,
         string searchString,
         int? pageNumber,
         string category)
        {
            var products = from product in _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType")
                           select product;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CurrentFilter"] = searchString;
            ViewData["Category"] = category;

            ViewData["Categories"] = _unitOfWork.Category.GetAll().Select(c => c.Name).ToList();
            ViewData["SearchString"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name == category);
            }

            var searchedProducts = products; // Lưu kết quả tìm kiếm

            switch (sortOrder)
            {
                case "name_desc":
                    searchedProducts = searchedProducts.OrderByDescending(s => s.Title);
                    break;
                case "price":
                    searchedProducts = searchedProducts.OrderBy(s => s.Price100);
                    break;
                case "price_desc":
                    searchedProducts = searchedProducts.OrderByDescending(s => s.Price100);
                    break;
                default:
                    searchedProducts = searchedProducts.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Product>.CreateAsync(searchedProducts, pageNumber ?? 1, pageSize));
        }


        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType"),
            };

            return View(cartObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
            }
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
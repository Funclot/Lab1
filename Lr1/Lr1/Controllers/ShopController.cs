using Microsoft.AspNetCore.Mvc;

namespace Lr1.Controllers
{
    [Route("store")]
    [Route("shop")]
    public class ShopController : Controller
    {
        // GET: /store
        // GET: /shop
        public IActionResult Index()
        {
            ViewBag.StoreName = "Магазин 'У Михаилыча'";
            ViewData["ProductsCount"] = 15;
            return View();
        }

        // GET: /store/category/electronics
        // GET: /shop/category/electronics
        [Route("category/{categoryName}")]
        public IActionResult Category(string categoryName)
        {
            ViewBag.Category = categoryName;
            ViewBag.Products = new[] { "Ноутбук", "Смартфон", "Планшет" };
            return View();
        }

        // GET: /store/product/42/details
        [Route("product/{id}/details")]
        public IActionResult ProductDetails(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductName = $"Товар #{id}";
            return View();
        }
    }
}

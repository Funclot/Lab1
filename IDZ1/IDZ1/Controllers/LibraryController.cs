using Microsoft.AspNetCore.Mvc;

namespace IDZ1.Controllers
{
    public class LibraryController : Controller
    {
        private static List<string[]> Books = new List<string[]>
        {
            new [] { "978-1", "Чистый код", "Роберт Мартин", "2019" },
            new [] { "978-2", "C# для начинающих", "Иван Иванов", "2022" },
            new [] { "978-3", "Алгоритмы", "Кормен", "2016" }
        };

        private static List<string[]> DeletedBooks = new List<string[]>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            ViewData["Title"] = "Каталог книг";
            ViewBag.Books = Books;
            return View();
        }

        public IActionResult Book(string isbn)
        {
            var book = Books.FirstOrDefault(b => b[0] == isbn);
            if (book == null) 
                ViewBag.Error = "Книга не найдена";
            else 
                ViewBag.Book = book;

            return View();
        }

        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Results = new List<string[]>();
                return View();
            }

            query = query.ToLower();
            ViewBag.Results = Books
                .Where
                (b => 
                b[1].ToLower().Contains(query) 
                || b[2].ToLower().Contains(query) 
                || b[0].ToLower().Contains(query))
                .ToList();

            return View();
        }

      
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Add(string isbn, string title, string author, string year)
        {
            // простая проверка
            if (string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(title))
            {
                ViewBag.Error = "ISBN и Название обязательны!";
                return View();
            }

            // чтобы не было одинаковых ISBN
            if (Books.Any(b => b[0] == isbn))
            {
                ViewBag.Error = "Книга с таким ISBN уже существует!";
                return View();
            }

            if (string.IsNullOrWhiteSpace(author)) author = "Неизвестно";
            if (string.IsNullOrWhiteSpace(year)) year = DateTime.Now.Year.ToString();

            Books.Add(new[] { isbn.Trim(), title.Trim(), author.Trim(), year.Trim() });

            return RedirectToAction("Catalog");
        }

        public IActionResult Delete(string isbn)
        {
            var book = Books.FirstOrDefault(b => b[0] == isbn);
            if (book != null)
            {
                Books.Remove(book);
                DeletedBooks.Add(book);
            }

            return RedirectToAction("Catalog");
        }

        public IActionResult Restore(string isbn)
        {
            var book = DeletedBooks.FirstOrDefault(b => b[0] == isbn);
            if (book != null)
            {
                DeletedBooks.Remove(book);
                Books.Add(book);
            }

            return RedirectToAction("Deleted");
        }
        public IActionResult Deleted()
        {
            ViewBag.DeletedBooks = DeletedBooks;
            return View();
        }
    }
}
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private static List<Author> authors = new List<Author>
{
    new Author
    {
        Id = 1,
        Name = "Robert Martin",
        Country = "USA"
    },

    new Author
    {
        Id = 2,
        Name = "Martin Fowler",
        Country = "UK"
    }
};
        private static List<Book> books = new List<Book>
    {
        new Book
        {
            Id = 1,
            Title = "Clean Code",
            Price = 2500,
            AuthorId = 1
        },

        new Book
        {
            Id = 2,
            Title = "Refactoring",
            Price = 3000,
            AuthorId = 2
        },

        new Book
        {
            Id = 3,
            Title = "Clean Architecture",
            Price = 2800,
            AuthorId = 1
        }
    };
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Authors = authors;

            model.Books = books.Select(book =>
                new BookCardViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,

                    AuthorName = authors
                        .FirstOrDefault(a => a.Id == book.AuthorId)
                        ?.Name
                }
            ).ToList();

            return View(model);
        }


        public IActionResult About()
        {
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            int newId = books.Count == 0
                ? 1
                : books[books.Count - 1].Id + 1;

            Book newBook = new Book
            {
                Id = newId,
                Title = title,
                Price = price,
                AuthorId = authorId
            };

            books.Add(newBook);

            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            int newId = authors.Count == 0
                ? 1
                : authors[authors.Count - 1].Id + 1;

            Author newAuthor = new Author
            {
                Id = newId,
                Name = name,
                Country = country
            };

            authors.Add(newAuthor);

            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book = books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                books.Remove(book);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

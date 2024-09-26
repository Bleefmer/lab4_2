using Microsoft.AspNetCore.Mvc;
using WebApplication4_2.Models;
using System.Collections.Generic;

namespace WebApplication4_2.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            return Content("Привітання в бібліотеці!");
        }

        public IActionResult Books()
        {
            var books = LoadBooks();
            return View(books);
        }

        public IActionResult Profile(int? id)
        {
            if (id.HasValue)
            {
                var userInfo = LoadUserInfo(id.Value);
                if (userInfo != null)
                {
                    return View(userInfo);
                }
            }

            var currentUserInfo = LoadCurrentUserInfo();
            return View(currentUserInfo);
        }

        private List<Book> LoadBooks()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("books.json")
                .Build();
            return configuration.GetSection("Books").Get<List<Book>>();
        }

        private User LoadUserInfo(int id)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("users.json")
                .Build();
            var users = configuration.GetSection("Users").Get<List<User>>();
            return users.FirstOrDefault(u => u.Id == id);
        }

        private User LoadCurrentUserInfo()
        {
            // Повертаємо інформацію про поточного користувача (статичні дані для прикладу)
            return new User { Id = 0, Name = "Петро Петров", Email = "petro@example.com" };
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Mission11.Models;
using Mission11.Models.ViewModels;
using System.Diagnostics;

namespace Mission11.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _repo;

        public HomeController(IBookRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(int pageNum)
        {
            int pageSize = 10; // Number of items per page

            var Books = new BooksListViewModel // Creating a view model for books
            {
                Books = _repo.Books // Retrieving books from repository
                .OrderBy(x => x.BookId) 
                .Skip((pageNum - 1) * pageSize) // Implementing pagination
                .Take(pageSize), // Taking a specific number of books for the page

                PaginationInfo = new PaginationInfo // Creating pagination information
                {
                    CurrentPage = pageNum, 
                    ItemsPerPage = pageSize, 
                    TotalItems = _repo.Books.Count()
                }
            };

            return View(Books);
        }
    }
}

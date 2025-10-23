using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [Authorize]
    [HttpGet("getbooks")]
    public IEnumerable<Book> Get()
    {
        int userAge = 0;
        var currentUser = HttpContext.User;
        var resultBookList = LibraryWarehouse.Library;

        if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        {
            DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)!.Value);
            userAge = DateTime.Today.Year - birthDate.Year;
            if (userAge >= 21)
            {
                resultBookList.Add(new Book { Author = "Judith Levine", Title = "Harmful to Minors", AgeRestriction = true });
            }
        }

        return resultBookList;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("addbooks")]
    public Task Add([FromBody] Book book)
    {
        var currentUser = HttpContext.User;
        var resultBookList = LibraryWarehouse.Library;

        LibraryWarehouse.Library.Add(book);

        return Task.CompletedTask;
    }
}

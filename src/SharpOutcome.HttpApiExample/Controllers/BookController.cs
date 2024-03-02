using System.Net;
using Microsoft.AspNetCore.Mvc;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;
using SharpOutcome.HttpApiExample.Services;

namespace SharpOutcome.HttpApiExample.Controllers;

[Route("api/books")]
[Consumes("application/json")]
[Produces("application/json")]
public sealed class BookController : ApiControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService) => _bookService = bookService;

    [HttpGet]
    public async Task<IActionResult> GetBook()
    {
        var data = await _bookService.GetAllAsync();
        return ResponseMaker(HttpStatusCode.OK, data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var result = await _bookService.GetOneAsync(id);

        return result.Match<IActionResult>(
            entity => ResponseMaker(HttpStatusCode.OK, entity),
            err => ResponseMaker(err)
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutBook(int id, BookRequest dto)
    {
        if (!ModelState.IsValid) return ResponseMaker(HttpStatusCode.BadRequest);

        var result = await _bookService.UpdateAsync(id, dto);

        return await result.MatchAsync<IActionResult>(
            entity => ResponseMakerAsync<Book, BookResponse>(HttpStatusCode.OK, entity),
            err => ResponseMaker(err)
        );
    }

    [HttpPost]
    public async Task<IActionResult> PostBook(BookRequest dto)
    {
        if (!ModelState.IsValid) return ResponseMaker(HttpStatusCode.BadRequest);

        var result = await _bookService.CreateAsync(dto);

        return await result.MatchAsync<IActionResult>(
            entity => ResponseMakerAsync<Book, BookResponse>(HttpStatusCode.Created, entity),
            err => ResponseMaker(err)
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.RemoveAsync(id);

        return result.Match(
            success => ResponseMaker(success),
            err => ResponseMaker(err)
        );
    }
}
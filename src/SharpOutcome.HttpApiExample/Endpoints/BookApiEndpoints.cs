using FluentValidation;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;
using SharpOutcome.HttpApiExample.Services;
using SharpOutcome.HttpApiExample.Utils;

namespace SharpOutcome.HttpApiExample.Endpoints;

public struct BookApiEndpoints : IApiEndpoint
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<BookRequest>, BookRequestValidator>();
        services.AddScoped<IBookService, BookService>();
    }

    public void DefineRoutes(IEndpointRouteBuilder routes)
    {
        var bookRoute = routes.MapGroup("api/books/");

        bookRoute.MapPost("", CreateBook)
            .AddEndpointFilter<FluentValidationFilter<BookRequest>>()
            .Produces<ApiResponse<Book>>(statusCode: StatusCodes.Status201Created)
            .Produces<ApiResponse>(statusCode: StatusCodes.Status400BadRequest);

        bookRoute.MapGet("{id:int}", GetSingleBook)
            .Produces<ApiResponse<Book>>(statusCode: StatusCodes.Status200OK)
            .Produces<ApiResponse>(statusCode: StatusCodes.Status404NotFound);

        bookRoute.MapGet("", GetAllBooks).Produces<ApiResponse<IList<Book>>>();

        bookRoute.MapPut("{id:int}", UpdateBook)
            .AddEndpointFilter<FluentValidationFilter<BookRequest>>()
            .Produces<ApiResponse<Book>>(statusCode: StatusCodes.Status200OK)
            .Produces<ApiResponse>(statusCode: StatusCodes.Status400BadRequest);

        bookRoute.MapDelete("{id:int}", DeleteBook)
            .Produces(statusCode: StatusCodes.Status204NoContent)
            .Produces<ApiResponse>(statusCode: StatusCodes.Status404NotFound);
    }


    public static async Task<IResult> CreateBook(BookRequest dto, IBookService bookService)
    {
        var result = await bookService.CreateAsync(dto);

        return result.Match(
            entity => ApiEndpointResponse.Send(StatusCodes.Status201Created, entity),
            err => ApiEndpointResponse.Send(err)
        );
    }

    public static async Task<IResult> GetSingleBook(int id, IBookService bookService)
    {
        var result = await bookService.GetOneAsync(id);

        return result.Match(
            entity => ApiEndpointResponse.Send(StatusCodes.Status200OK, entity),
            err => ApiEndpointResponse.Send(err)
        );
    }

    public static async Task<IResult> GetAllBooks(IBookService bookService)
    {
        var data = await bookService.GetAllAsync();
        return ApiEndpointResponse.Send(StatusCodes.Status200OK, data);
    }


    public static async Task<IResult> UpdateBook(int id, BookRequest dto, IBookService bookService)
    {
        var result = await bookService.UpdateAsync(id, dto);

        return result.Match(
            entity => ApiEndpointResponse.Send(StatusCodes.Status200OK, entity),
            err => ApiEndpointResponse.Send(err)
        );
    }

    public static async Task<IResult> DeleteBook(int id, IBookService bookService)
    {
        var result = await bookService.RemoveAsync(id);

        return result.Match(
            success => ApiEndpointResponse.Send(success),
            err => ApiEndpointResponse.Send(err)
        );
    }
}
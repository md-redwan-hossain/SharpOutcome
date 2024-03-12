using FluentValidation;
using SharpOutcome.HttpApiExample.DataTransferObjects;
using SharpOutcome.HttpApiExample.Services;
using SharpOutcome.HttpApiExample.Utils;

namespace SharpOutcome.HttpApiExample.Endpoints;

public static class BookApiEndpoints
{
    public static void RegisterBookApiEndpointServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<BookRequest>, BookRequestValidator>();
        services.AddScoped<IBookService, BookService>();
    }

    public static void MapBookApiEndpoints(this IEndpointRouteBuilder app)
    {
        var route = app.MapGroup("api/books/");

        route.MapPost("", CreateBook);
        route.MapGet("{id:int}", GetSingleBook);
        route.MapGet("", GetAllBooks);
        route.MapPut("{id:int}", UpdateBook);
        route.MapDelete("{id:int}", DeleteBook);
    }


    public static async Task<IResult> CreateBook(BookRequest dto,
        IValidator<BookRequest> bookRequestValidator, IBookService bookService)
    {
        var validationResult = await bookRequestValidator.ValidateAsync(dto);
        if (validationResult.IsValid is false)
        {
            return MinimalApiResponse.Send(StatusCodes.Status400BadRequest,
                FluentValidationUtils.MapErrors(validationResult.Errors));
        }

        var result = await bookService.CreateAsync(dto);

        return result.Match(
            entity => MinimalApiResponse.Send(StatusCodes.Status201Created, entity),
            err => MinimalApiResponse.Send(err)
        );
    }

    public static async Task<IResult> GetSingleBook(int id, IBookService bookService)
    {
        var result = await bookService.GetOneAsync(id);

        return result.Match(
            entity => MinimalApiResponse.Send(StatusCodes.Status200OK, entity),
            err => MinimalApiResponse.Send(err)
        );
    }

    public static async Task<IResult> GetAllBooks(IBookService bookService)
    {
        var data = await bookService.GetAllAsync();
        return MinimalApiResponse.Send(StatusCodes.Status200OK, data);
    }


    public static async Task<IResult> UpdateBook(int id, BookRequest dto,
        IValidator<BookRequest> bookRequestValidator, IBookService bookService)
    {
        var validationResult = await bookRequestValidator.ValidateAsync(dto);
        if (validationResult.IsValid is false)
        {
            return MinimalApiResponse.Send(StatusCodes.Status400BadRequest,
                FluentValidationUtils.MapErrors(validationResult.Errors));
        }

        var result = await bookService.UpdateAsync(id, dto);

        return result.Match(
            entity => MinimalApiResponse.Send(StatusCodes.Status200OK, entity),
            err => MinimalApiResponse.Send(err)
        );
    }

    public static async Task<IResult> DeleteBook(int id, IBookService bookService)
    {
        var result = await bookService.RemoveAsync(id);

        return result.Match(
            success => MinimalApiResponse.Send(success),
            err => MinimalApiResponse.Send(err)
        );
    }
}
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharpOutcome.Helpers;
using SharpOutcome.Helpers.Enums;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;

namespace SharpOutcome.HttpApiExample.Services;

public class BookService : IBookService
{
    private readonly BookDbContext _bookDbContext;

    public BookService(BookDbContext bookDbContext)
    {
        _bookDbContext = bookDbContext;
    }

    public async Task<ValueOutcome<Book, HttpBadOutcome>> CreateAsync(BookRequest dto)
    {
        try
        {
            var duplicateIsbn = await _bookDbContext.Books
                .Where(x => x.Isbn == dto.Isbn)
                .FirstOrDefaultAsync();

            if (duplicateIsbn is not null)
            {
                return new HttpBadOutcome(HttpBadOutcomeTag.Conflict, $"Duplicate isbn: {dto.Isbn}");
            }

            var entity = await dto.BuildAdapter().AdaptToTypeAsync<Book>();
            await _bookDbContext.Books.AddAsync(entity);
            await _bookDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new HttpBadOutcome(HttpBadOutcomeTag.InternalServerError);
        }
    }

    public async Task<ValueOutcome<Book, HttpBadOutcome>> UpdateAsync(int id, BookRequest dto)
    {
        try
        {
            var entityToUpdate = await _bookDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (entityToUpdate is null) return new HttpBadOutcome(HttpBadOutcomeTag.NotFound);

            await dto.BuildAdapter().AdaptToAsync(entityToUpdate);
            _bookDbContext.Books.Attach(entityToUpdate);
            _bookDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            await _bookDbContext.SaveChangesAsync();
            return entityToUpdate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new HttpBadOutcome(HttpBadOutcomeTag.InternalServerError);
        }
    }

    public async Task<IList<Book>> GetAllAsync()
    {
        return await _bookDbContext.Books.ToListAsync();
    }

    public async Task<ValueOutcome<Book, HttpBadOutcome>> GetOneAsync(int id)
    {
        var entity = await _bookDbContext.Books.FindAsync(id);
        if (entity is null) return new HttpBadOutcome(HttpBadOutcomeTag.NotFound);
        return entity;
    }

    public async Task<ValueOutcome<HttpGoodOutcome, HttpBadOutcome>> RemoveAsync(int id)
    {
        try
        {
            var entityToDelete = await _bookDbContext.Books.FindAsync(id);
            if (entityToDelete is null) return new HttpBadOutcome(HttpBadOutcomeTag.NotFound);

            if (_bookDbContext.Entry(entityToDelete).State is EntityState.Detached)
            {
                _bookDbContext.Books.Attach(entityToDelete);
            }

            _bookDbContext.Books.Remove(entityToDelete);
            await _bookDbContext.SaveChangesAsync();

            return new HttpGoodOutcome(HttpGoodOutcomeTag.NoContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new HttpBadOutcome(HttpBadOutcomeTag.InternalServerError);
        }
    }
}
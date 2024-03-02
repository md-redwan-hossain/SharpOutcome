using Mapster;
using Microsoft.EntityFrameworkCore;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;

namespace SharpOutcome.HttpApiExample.Services;

public class BookService : IBookService
{
    private readonly BookDbContext _bookDbContext;
    public BookService(BookDbContext bookDbContext) => _bookDbContext = bookDbContext;

    public async Task<Outcome<Book, IBadOutcome>> CreateAsync(BookRequest dto)
    {
        try
        {
            var entity = new Book();
            await dto.BuildAdapter().AdaptToAsync(entity);
            await _bookDbContext.Books.AddAsync(entity);
            await _bookDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadOutcome(BadOutcomeType.Unexpected);
        }
    }

    public async Task<Outcome<Book, IBadOutcome>> UpdateAsync(int id, BookRequest dto)
    {
        try
        {
            var entityToUpdate = await _bookDbContext.Books.FindAsync(id);
            if (entityToUpdate is null) return new BadOutcome(BadOutcomeType.NotFound);

            await dto.BuildAdapter().AdaptToAsync(entityToUpdate);
            _bookDbContext.Books.Attach(entityToUpdate);
            _bookDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            await _bookDbContext.SaveChangesAsync();
            return entityToUpdate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadOutcome(BadOutcomeType.Unexpected);
        }
    }

    public async Task<IList<Book>> GetAllAsync()
    {
        return await _bookDbContext.Books.ToListAsync();
    }

    public async Task<Outcome<Book, IBadOutcome>> GetOneAsync(int id)
    {
        var entity = await _bookDbContext.Books.FindAsync(id);
        if (entity is null) return new BadOutcome(BadOutcomeType.NotFound);
        return entity;
    }

    public async Task<Outcome<IGoodOutcome, IBadOutcome>> RemoveAsync(int id)
    {
        try
        {
            var entityToDelete = await _bookDbContext.Books.FindAsync(id);
            if (entityToDelete is null) return new BadOutcome(BadOutcomeType.NotFound);

            if (_bookDbContext.Entry(entityToDelete).State is EntityState.Detached)
            {
                _bookDbContext.Books.Attach(entityToDelete);
            }

            _bookDbContext.Books.Remove(entityToDelete);
            await _bookDbContext.SaveChangesAsync();

            return new GoodOutcome(GoodOutcomeType.Deleted);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadOutcome(BadOutcomeType.Unexpected);
        }
    }
}
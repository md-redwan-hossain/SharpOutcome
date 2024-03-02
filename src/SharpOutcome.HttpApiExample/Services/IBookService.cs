using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;

namespace SharpOutcome.HttpApiExample.Services;

public interface IBookService
{
    Task<Outcome<Book, IBadOutcome>> CreateAsync(BookRequest dto);
    Task<Outcome<Book, IBadOutcome>> UpdateAsync(int id, BookRequest dto);
    Task<IList<Book>> GetAllAsync();
    Task<Outcome<Book, IBadOutcome>> GetOneAsync(int id);
    Task<Outcome<IGoodOutcome, IBadOutcome>> RemoveAsync(int id);
}
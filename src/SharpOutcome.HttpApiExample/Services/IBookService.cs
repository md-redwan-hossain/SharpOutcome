using SharpOutcome.Helpers;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;

namespace SharpOutcome.HttpApiExample.Services;

public interface IBookService
{
    Task<ValueOutcome<Book, IBadOutcome>> CreateAsync(BookRequest dto);
    Task<ValueOutcome<Book, IBadOutcome>> UpdateAsync(int id, BookRequest dto);
    Task<IList<Book>> GetAllAsync();
    Task<ValueOutcome<Book, IBadOutcome>> GetOneAsync(int id);
    Task<ValueOutcome<IGoodOutcome, IBadOutcome>> RemoveAsync(int id);
}
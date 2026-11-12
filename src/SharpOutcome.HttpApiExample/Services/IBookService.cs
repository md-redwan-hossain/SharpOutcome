using SharpOutcome.Helpers;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;

namespace SharpOutcome.HttpApiExample.Services;

public interface IBookService
{
    Task<HttpOutcome<Book>> CreateAsync(BookRequest dto);
    Task<ValueOutcome<Book, HttpBadOutcome>> UpdateAsync(int id, BookRequest dto);
    Task<IList<Book>> GetAllAsync();
    Task<ValueOutcome<Book, HttpBadOutcome>> GetOneAsync(int id);
    Task<ValueOutcome<HttpGoodOutcome, HttpBadOutcome>> RemoveAsync(int id);
}
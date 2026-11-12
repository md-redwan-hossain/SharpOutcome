using SharpOutcome.Helpers;
using SharpOutcome.HttpApiExample.Data;
using SharpOutcome.HttpApiExample.DataTransferObjects;

namespace SharpOutcome.HttpApiExample.Services;

public interface IBookService
{
    Task<HttpUnionOutcome<Book>> CreateAsync(BookRequest dto);
    Task<HttpUnionOutcome<Book>> UpdateAsync(int id, BookRequest dto);
    Task<IList<Book>> GetAllAsync();
    Task<ValueOutcome<Book, HttpBadOutcome>> GetOneAsync(int id);
    Task<ValueOutcome<HttpGoodOutcome, HttpBadOutcome>> RemoveAsync(int id);
}
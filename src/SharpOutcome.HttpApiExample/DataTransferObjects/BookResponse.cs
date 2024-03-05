namespace SharpOutcome.HttpApiExample.DataTransferObjects;

public record BookResponse(
    int Id,
    string Title,
    string Isbn,
    string Genre,
    string Author,
    int Price
);
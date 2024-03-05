using System.ComponentModel.DataAnnotations;

namespace SharpOutcome.HttpApiExample.DataTransferObjects;

public record BookRequest(
    [MaxLength(256)] [Required] string Title,
    [MaxLength(38)] [Required] string Isbn,
    [MaxLength(256)] [Required] string Genre,
    [MaxLength(256)] [Required] string Author,
    [Required] int Price
);
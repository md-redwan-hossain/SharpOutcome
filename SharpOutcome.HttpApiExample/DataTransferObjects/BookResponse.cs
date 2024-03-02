using System.ComponentModel.DataAnnotations;

namespace SharpOutcome.HttpApiExample.DataTransferObjects;

public record BookResponse(
    [Required] int Id,
    [MaxLength(256)] [Required] string Title,
    [MaxLength(256)] [Required] string Genre,
    [MaxLength(256)] [Required] string Author,
    [Required] double Price
);
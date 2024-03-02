﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharpOutcome.HttpApiExample.Data;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(256);
        builder.Property(x => x.Genre).HasMaxLength(256);
        builder.Property(x => x.Author).HasMaxLength(256);
    }
}
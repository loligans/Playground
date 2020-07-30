using Metadata.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Metadata.Data
{
    /// <summary>
    /// The session to the database for performing CRUD operations on metadata records.
    /// </summary>
    public interface IMetadataDbContext
    {
        /// <summary>
        /// The set of all <see cref="FileMetadata"/> entities in the database.
        /// </summary>
        /// <value></value>
        DbSet<FileMetadata> FileMetadatas { get; }
    }

    /// <inheritdoc cref="IMetadataDbContext" />
    public class MetadataDbContext : DbContext, IMetadataDbContext
    {
        /// <inheritdoc cref="DbContext"/>
        public MetadataDbContext(DbContextOptions<MetadataDbContext> options) : base(options) { }

        /// <inheritdoc cref="IMetadataDbContext.FileMetadatas" />
        public DbSet<FileMetadata> FileMetadatas { get; set; }
    }
}

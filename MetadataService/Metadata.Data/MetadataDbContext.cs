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
        /// The set of all <see cref="ImageMetadata"/> entities in the database.
        /// </summary>
        /// <value></value>
        DbSet<ImageMetadata> ImageMetadatas { get; }

        /// <summary>
        /// The set of all <see cref="AudioMetadata"/> entities in the database.
        /// </summary>
        /// <value></value>
        DbSet<AudioMetadata> AudioMetadatas { get; }

        /// <summary>
        /// The set of all <see cref="FileMetadata"/> entities in the database.
        /// </summary>
        /// <value></value>
        DbSet<FileMetadata> FileMetadatas { get; }

        /// <summary>
        /// The set of all <see cref="VideoMetadata"/> entities in the database.
        /// </summary>
        /// <value></value>
        DbSet<VideoMetadata> VideoMetadatas { get; }
    }

    /// <inheritdoc cref="IMetadataDbContext" />
    public class MetadataDbContext : DbContext, IMetadataDbContext
    {
        /// <inheritdoc cref="DbContext"/>
        public MetadataDbContext(DbContextOptions<MetadataDbContext> options) : base(options) { }

        /// <inheritdoc cref="IMetadataDbContext.ImageMetadatas" />
        public DbSet<ImageMetadata> ImageMetadatas { get; set; }

        /// <inheritdoc cref="IMetadataDbContext.AudioMetadatas" />
        public DbSet<AudioMetadata> AudioMetadatas { get; set; }

        /// <inheritdoc cref="IMetadataDbContext.FileMetadatas" />
        public DbSet<FileMetadata> FileMetadatas { get; set; }

        /// <inheritdoc cref="IMetadataDbContext.VideoMetadatas" />
        public DbSet<VideoMetadata> VideoMetadatas { get; set; }
    }
}

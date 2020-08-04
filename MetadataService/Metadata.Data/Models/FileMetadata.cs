using System.ComponentModel.DataAnnotations;
using System;

namespace Metadata.Data.Models
{
    /// <summary>
    ///
    /// </summary>
    public class FileMetadata
    {
        /// <summary>
        /// the unique identifier of this record
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The type of file.
        /// </summary>
        [Required]
        public string Type { get; set; }
        /// <summary>
        /// The path to the file.
        /// </summary>
        [Required]
        [StringLength(384)]
        public string Path { get; set; }
        /// <summary>
        /// A short description of the file.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// The date the file was created
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// The last time the file was modified
        /// </summary>
        public DateTime Modified { get; set; }
        /// <summary>
        /// The file size in bytes
        /// </summary>
        public long Size { get; set; }
    }
}

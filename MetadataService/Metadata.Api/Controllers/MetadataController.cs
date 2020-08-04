using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Metadata.Data.Models;
using Metadata.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Metadata.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly ILogger<MetadataController> _logger;
        private readonly IMetadataDbContext _context;

        /// <summary>
        ///
        /// </summary>
        public MetadataController(IMetadataDbContext context, ILogger<MetadataController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        [HttpGet("{id}")]
        public async Task<FileMetadata> GetMetadataById(Guid id)
        {
            return await _context.FileMetadatas.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        [HttpPost("")]
        public async Task CreateMetadata(FileMetadata request)
        {
            _context.FileMetadatas.Add(request);
            await _context.SaveChangesAsync();
        }
    }
}

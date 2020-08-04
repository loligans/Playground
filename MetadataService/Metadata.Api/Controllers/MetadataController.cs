using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Metadata.Data.Models;

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public MetadataController(ILogger<MetadataController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<FileMetadata> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new FileMetadata
            {
                Id = Guid.NewGuid()
            })
            .ToArray();
        }
    }
}

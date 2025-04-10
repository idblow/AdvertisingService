using AdvertisingService.Models;
using AdvertisingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisingController : ControllerBase
    {
        private readonly AdvertisingServices _advertisingService;
        public AdvertisingController(AdvertisingServices advertisingService)
        {
            _advertisingService = advertisingService;
        }
        [HttpPost("load")]
        public IActionResult LoadData([FromBody] FileUploadRequest request)
        {
            if (string.IsNullOrEmpty(request?.FilePath))
            {
                return BadRequest("File path is required");
            }

            try
            {
                _advertisingService.LoadDataFromFile(request.FilePath);
                return Ok("Data loaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading data: {ex.Message}");
            }
        }

        [HttpGet("platforms")]
        public IActionResult GetPlatforms([FromQuery] string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("Location is required");
            }

            try
            {
                var platforms = _advertisingService.GetPlatformsForLocation(location);
                return Ok(platforms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving platforms: {ex.Message}");
            }
        }
    }
}

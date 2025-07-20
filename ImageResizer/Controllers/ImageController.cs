using ImageResizer.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ImageResizer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    [HttpPost("resize")]
    public async Task<IActionResult> ResizeImage(IFormFile? file,
        [FromQuery] int maxWidth = 1920, [FromQuery] int maxHeight = 1080, [FromQuery] int quality = 85)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file provided");
        }

        if (!IsValidImageFile(file))
        {
            return BadRequest("Invalid file format. Only JPEG, PNG, GIF, and BMP are supported.");  
        }


        // if (file.Length > 50 * 1024 * 1024) // 50MB limit
        // {
        //     return BadRequest("File too large. Maximum size is 50MB.");
        // }
        try
        {
            // Get original image info
            var originalInfo = await _imageService.GetImageInfoAsync(file);
            
            // Resize image
            var resizedImageBytes = await _imageService.ResizeImageAsync(file, maxWidth, maxHeight, quality);
            
            // Prepare response with metadata
            var response = new
            {
                originalSize = originalInfo.OriginalSize,
                newSize = resizedImageBytes.Length,
                originalDimensions = new { width = originalInfo.OriginalWidth, height = originalInfo.OriginalHeight },
                newDimensions = CalculateNewDimensions(originalInfo.OriginalWidth, originalInfo.OriginalHeight, maxWidth, maxHeight),
                compressionRatio = Math.Round((1 - (double)resizedImageBytes.Length / originalInfo.OriginalSize) * 100, 2),
                fileName = $"resized_{originalInfo.FileName}"
            };

            return File(resizedImageBytes, "image/jpeg", response.fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error processing image: {ex.Message}");
        }
    }
    
    [HttpPost("info")]
    public async Task<IActionResult> GetImageInfo(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        try
        {
            var info = await _imageService.GetImageInfoAsync(file);
            return Ok(info);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error reading image: {ex.Message}");
        }
    }
    
    private bool IsValidImageFile(IFormFile file)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
    
    private (int width, int height) CalculateNewDimensions(int originalWidth, int originalHeight, int maxWidth, int maxHeight)
    {
        if (originalWidth <= maxWidth && originalHeight <= maxHeight)
            return (originalWidth, originalHeight);

        var ratioX = (double)maxWidth / originalWidth;
        var ratioY = (double)maxHeight / originalHeight;
        var ratio = Math.Min(ratioX, ratioY);

        return ((int)(originalWidth * ratio), (int)(originalHeight * ratio));
    }
}
using System.Net.Mime;
using ImageResizer.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

public class ImageService : IImageService
{
    public async Task<byte[]> ResizeImageAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080, int quality = 85)
    {
        //using var image = await MediaTypeNames.Image.LoadAsync(file.OpenReadStream());
        using var image = await Image.LoadAsync(file.OpenReadStream());
        
        // Calculate new dimensions while maintaining aspect ratio
        var (newWidth, newHeight) = CalculateNewDimensions(image.Width, image.Height, maxWidth, maxHeight);
        
        // Resize the image
        image.Mutate(x => x.Resize(newWidth, newHeight));
        
        // Convert to bytes with specified quality
        using var ms = new MemoryStream();
        await image.SaveAsJpegAsync(ms, new JpegEncoder { Quality = quality });
        
        return ms.ToArray();
    }

    public async Task<ImageMetadata> GetImageInfoAsync(IFormFile file)
    {
        using var image = await Image.LoadAsync(file.OpenReadStream());
        
        return new ImageMetadata
        {
            FileName = file.FileName,
            OriginalSize = file.Length,
            OriginalWidth = image.Width,
            OriginalHeight = image.Height,
            Format = image.Metadata.DecodedImageFormat?.Name ?? "Unknown"
        };
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
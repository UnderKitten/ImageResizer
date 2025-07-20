namespace ImageResizer.Services
{
    public interface IImageService
    {
        Task<byte[]> ResizeImageAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080, int quality = 85);
        Task<ImageMetadata> GetImageInfoAsync(IFormFile file);
    }

    public class ImageMetadata
    {
        public string FileName { get; set; }
        public long OriginalSize { get; set; }
        public long NewSize { get; set; }
        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }
        public int NewWidth { get; set; }
        public int NewHeight { get; set; }
        public string Format { get; set; }
    }
}


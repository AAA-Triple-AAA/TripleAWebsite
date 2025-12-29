using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using System.Text.RegularExpressions;

namespace TripleAWebsite.Frontend.Services
{
    public class FileService(IWebHostEnvironment environment)
    {
        private readonly string _uploadDirectory = Path.Combine(environment.WebRootPath, "uploads");

        public async Task<string> SaveFileAsync(IBrowserFile file, string? customFileName = null)
        {
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }

            // Determine if the file is an image we should optimize
            var extension = Path.GetExtension(file.Name).ToLowerInvariant();
            var isImage = extension == ".jpg" || extension == ".jpeg" || extension == ".png";

            string fileName;
            string finalExtension = isImage ? ".webp" : extension; // Force WebP for images

            if (!string.IsNullOrEmpty(customFileName))
            {
                // Sanitize name and use the correct extension
                var sanitized = Regex.Replace(customFileName, @"[^a-zA-Z0-9\-]", "-");
                // Remove the old extension from the custom name if it was passed in (e.g. "Resume.pdf" -> "Resume")
                sanitized = Path.GetFileNameWithoutExtension(sanitized);
                fileName = $"{sanitized}{finalExtension}";
            }
            else
            {
                fileName = $"{Guid.NewGuid()}{finalExtension}";
            }

            var filePath = Path.Combine(_uploadDirectory, fileName);

            // Delete overwrite if exists
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // --- OPTIMIZATION LOGIC ---
            if (isImage)
            {
                // Load the image from the stream
                await using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 20); // 20MB max
                using var image = await Image.LoadAsync(stream);

                // Resize if width is larger than 800px (Good balance for profile/projects)
                // This preserves aspect ratio automatically
                if (image.Width > 800)
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(800, 0), // 0 maintains aspect ratio based on width
                        Mode = ResizeMode.Max
                    }));
                }

                // Save as WebP with high compression
                await image.SaveAsWebpAsync(filePath, new WebpEncoder { Quality = 75 });
            }
            else
            {
                // It's a PDF or other file, save normally
                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 20).CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }

        public void DeleteFile(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;
            var absolutePath = Path.Combine(environment.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }
        }
    }
}
using Microsoft.AspNetCore.Components.Forms;
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

            string fileName;

            if (!string.IsNullOrEmpty(customFileName))
            {
                // 1. Sanitize the user's name (remove spaces, special chars)
                // e.g. "Adrian Abella" -> "Adrian-Abella"
                var sanitized = Regex.Replace(customFileName, @"[^a-zA-Z0-9\-]", "-");

                // 2. Add the extension (e.g. .pdf)
                fileName = $"{sanitized}{Path.GetExtension(file.Name)}";
            }
            else
            {
                // Fallback to GUID if no name provided
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
            }

            var filePath = Path.Combine(_uploadDirectory, fileName);

            // 3. Delete if a file with this exact name already exists (overwrite)
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // 4. Save to disk
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 10).CopyToAsync(stream); // bumped to 10MB

            return $"/uploads/{fileName}";
        }

        public void DeleteFile(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            // Convert "/uploads/file.jpg" -> "C:\...\wwwroot\uploads\file.jpg"
            var absolutePath = Path.Combine(environment.WebRootPath, relativePath.TrimStart('/'));

            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }
        }
    }
}

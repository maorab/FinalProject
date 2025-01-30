using Microsoft.AspNetCore.Components.Forms;

namespace Clients.Blazor.Services
{
    public class FileService
    {
        private const string BASE_FOLDER = "wwwroot/Pictures";

        public string Folder4Display => "Pictures";

        public async Task<string> SaveFileAsync(IBrowserFile file, string subFolder, string? existingFileName = null)
        {
            try
            {
                string folderPath = $"{BASE_FOLDER}/{subFolder}";

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                if (!string.IsNullOrEmpty(existingFileName))
                    await DeleteFileAsync(existingFileName, folderPath);

                // Save the file
                string uniqueFileName = GetUniqueFileName(file.Name, subFolder);
                using (var fileStream = new FileStream($"{folderPath}/{uniqueFileName}", FileMode.Create))
                {
                    await file.OpenReadStream(maxAllowedSize: 10485760).CopyToAsync(fileStream); // Max size 10MB
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);//נקודת עצירה כאן
                return "";
            }
        }


        public async Task DeleteFileAsync(string fileName, string subFolder)
        {
            string filePath = $"{BASE_FOLDER}/{subFolder}/{fileName}";

            if (IsFileExists(fileName, subFolder))
                await Task.Run(() => File.Delete(filePath));
        }

        private bool IsFileExists(string fileName, string subFolder)
        {
            string filePath = $"{BASE_FOLDER}/{subFolder}/{fileName}";
            return File.Exists(filePath);
        }

        private string GetUniqueFileName(string fileName, string subFolder)
        {
            int count = 1;

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            while (IsFileExists(fileName, subFolder))
            {
                fileName = $"{fileNameWithoutExt}_{count}{extension}";
                count++;
            }

            return fileName;
        }
    }
}
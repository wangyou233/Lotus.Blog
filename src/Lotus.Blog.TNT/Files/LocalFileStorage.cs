using Lotus.Blog.TNT.Autofac;

namespace Lotus.Blog.TNT.Files;

public class LocalFileStorage : IFileStorage
{
    public string GenerateFilePath(string siteId, string fileName)
    {
        var date = DateTime.Now;
        return $"{siteId}/{date.Year}/{date.Month}/{date.Day}/{fileName}";
    }

    public async Task<bool> SaveFileAsync(string filePath, byte[] fileContent)
    {
        var fileInfo = new FileInfo(filePath);

        try
        {
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }

            await File.WriteAllBytesAsync(filePath, fileContent);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> SaveFileAppointAsync(string filePath, byte[] fileContent)
    {
        var fileInfo = new FileInfo(filePath);

        try
        {
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }

            await File.WriteAllBytesAsync(filePath, fileContent);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<byte[]> GetFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        return await File.ReadAllBytesAsync(filePath);
    }

    public bool RemoveFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return true;
        }

        File.Delete(filePath);

        return true;
    }

    public string GenerateFilePath(string fileName)
    {
        var date = DateTime.Now;
        return $"{date.Year}/{date.Month}/{date.Day}/{fileName}";
    }

    public bool CheckIsImage(string file)
    {
        var imageExtensions = new List<string> {".JPG", ".JPE", ".BMP", ".GIF", ".PNG"};

        var fileInfo = new FileInfo(file);
        return imageExtensions.Contains(fileInfo.Extension.ToUpperInvariant());
    }
}
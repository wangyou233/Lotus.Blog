namespace Lotus.Blog.TNT.Files;

public interface IFileStorage
{
    Task<bool> SaveFileAsync(string filePath, byte[] fileContent);
    Task<bool> SaveFileAppointAsync(string filePath, byte[] fileContent);

    Task<byte[]> GetFileAsync(string filePath);

    bool RemoveFile(string filePath);

    string GenerateFilePath(string fileName);


    bool CheckIsImage(string file);


}
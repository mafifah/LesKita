using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Download;
using System.IO;
using System.Threading;

namespace LesKita;

public class ClsServisDrive
{
    private const string ServiceAccountJsonFile = @"credentials.json";  // Path ke file JSON Service Account
    private DriveService GetDriveService()
    {
        var credential = GoogleCredential.FromFile(ServiceAccountJsonFile)
            .CreateScoped(DriveService.Scope.Drive);

        return new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "LesKita"
        });
    }
    public async Task<List<(string Id, string Name)>> GetAllFilesAsync()
    {
        var service = GetDriveService();

        var request = service.Files.List();
        request.Fields = "files(id, name)";
        request.PageSize = 100; // Batasi jika perlu

        var result = await request.ExecuteAsync();
        return result.Files.Select(f => (f.Id, f.Name)).ToList();
    }
    public async Task<string> UploadFileAsync(string filePath)
    {
        var credential = GoogleCredential.FromFile(ServiceAccountJsonFile)
            .CreateScoped(DriveService.Scope.DriveFile);  // Scope untuk akses file Google Drive
        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "LesKita"
        });

        var fileMetadata = new Google.Apis.Drive.v3.Data.File
        {
            Name = Path.GetFileName(filePath)
        };

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
        request.Fields = "id";  // Mendapatkan id file yang baru di-upload
        var uploadResult = await request.UploadAsync();

        if (uploadResult.Status == Google.Apis.Upload.UploadStatus.Completed)
        {
            return request.ResponseBody?.Id;  // Mengembalikan fileId setelah upload selesai
        }

        return null;
    }

    public async Task<string> ReadFileContentAsync(string fileId)
    {
        var credential = GoogleCredential.FromFile(ServiceAccountJsonFile)
            .CreateScoped(DriveService.Scope.DriveReadonly);  // Scope untuk hanya membaca file
        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "LesKita"
        });

        var request = service.Files.Get(fileId);
        var stream = new MemoryStream();
        await request.DownloadAsync(stream);

        stream.Position = 0;
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    public async Task<bool> DeleteFileAsync(string fileId)
    {
        var service = GetDriveService();

        try
        {
            await service.Files.Delete(fileId).ExecuteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
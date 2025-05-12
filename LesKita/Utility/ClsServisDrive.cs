using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace LesKita;

public class ClsServisDrive
{
    public async Task UploadFileAsync(string filePath)
    {
        var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "YOUR_CLIENT_ID",
                ClientSecret = "YOUR_CLIENT_SECRET"
            },
            new[] { DriveService.Scope.DriveFile },
            "user",
            CancellationToken.None
        );

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "LesKita",
        });

        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = Path.GetFileName(filePath)
        };

        using var stream = new FileStream(filePath, FileMode.Open);
        var request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
        request.Fields = "id";
        await request.UploadAsync();

        var file = request.ResponseBody;
        Console.WriteLine("File ID: " + file.Id);
    }

    public async Task<string> ReadFileContentAsync(string fileId)
    {
        var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "YOUR_CLIENT_ID",
                ClientSecret = "YOUR_CLIENT_SECRET"
            },
            new[] { DriveService.Scope.DriveReadonly },
            "user",
            CancellationToken.None
        );

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
        string content = await reader.ReadToEndAsync();
        return content;
    }
}

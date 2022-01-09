using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

static DriveService GetClient()
{
    string credenitalsJSONPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIAL");

    using (var stream = new FileStream(credenitalsJSONPath, FileMode.Open, FileAccess.Read))
    {
        var credentials = GoogleCredential.FromStream(stream);

        if (credentials != null)
        {
            if (credentials.IsCreateScopedRequired)
            {
                string[] scopes = { DriveService.Scope.Drive };
                credentials = credentials.CreateScoped(scopes);
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "GanjMp3"
            });

            return service;
        }
    }
    return null;
}

static void GetDownloadLink(string fileId = "")
{
    var _driveClient = GetClient();

    var result = _driveClient.Files.List();

    foreach(var item in result.Execute().Files)
    {
        System.Console.WriteLine(item.Name);
    }
}

GetDownloadLink();
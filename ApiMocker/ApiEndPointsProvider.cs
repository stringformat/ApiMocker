using System.Text.Json;

namespace ApiMocker;

public class ApiEndPointsProvider
{
    private readonly IConfiguration _configuration;

    public ApiEndPointsProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<EndPoint>?> GetEndPoints()
    {
        var path = _configuration["EndPointsConfigurationFilePath"];
        var file =  await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<IEnumerable<EndPoint>>(file);
    }
}
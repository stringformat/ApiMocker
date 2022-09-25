using System.Net;

namespace ApiMocker;

public record EndPoint(
    string Url,
    HttpStatusCode ResponseCode,
    string? ResponseContentType,
    Dictionary<string, string[]>? ResponseHeaders,
    string? ResponsePath);
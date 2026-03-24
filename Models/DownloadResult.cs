namespace DownloadManager.Models;

public class DownloadResult
{
    public string Url { get; init; }
    public string Destination { get; init; }
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public long BytesDownloaded { get; init; }
    public TimeSpan Duration { get; init; }
}
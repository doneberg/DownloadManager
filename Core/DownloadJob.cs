namespace DownloadManager.Core;

public class DownloadJob : IDisposable
{
    private readonly HttpClient _client;
    private readonly CancellationTokenSource _cts;
    private bool _disposed  = false;
    
    public string Url { get; }
    public string Destination { get; }

    public DownloadJob(string url, string destination)
    {
        Url = url;
        Destination = destination;
        _client = new HttpClient();
        _cts = new CancellationTokenSource();
    }
    
    public void Cancel() => _cts.Cancel();

    public async Task RunAsync()
    {
        using var response = await _client.GetAsync(Url, _cts.Token);
        using var fileStream = new FileStream(Destination, FileMode.Create);
        
        await response.Content.CopyToAsync(fileStream, _cts.Token);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _client.Dispose();
                _cts.Dispose();
            }
            _disposed = true;
        }
    }
}
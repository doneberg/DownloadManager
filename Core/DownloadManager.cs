namespace DownloadManager.Core;

public class DownloadManager
{
    private readonly int _maxConcurrent;
    
    public DownloadManager(int maxConcurrent = 3)
    {
        _maxConcurrent = maxConcurrent;
    }

    public async Task RunAllAsync(List<(string url, string dest)> downloads)
    {
        using var semaphore = new SemaphoreSlim(_maxConcurrent);

        var tasks = downloads.Select(async d =>
        {
            await semaphore.WaitAsync();
            try
            {
                using var job = new DownloadJob(d.url, d.dest);
                await job.RunAsync();
                Console.WriteLine($"Done: {d.dest}");
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);
    }
}
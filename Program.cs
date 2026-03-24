using DownloadManager.Core;

var downloads = new List<(string, string)>
{
    //links go here
    ("https://github.com/doneberg/DownloadManager/archive/refs/heads/main.zip", "DownloadManager-main.zip"),
};

using var monitor = new QueueMonitor();
monitor.Start();

var manager = new DownloadManager.Core.DownloadManager(maxConcurrent: 2);

monitor.Log("Starting downloads...");
await manager.RunAllAsync(downloads);
monitor.Log("Finished downloads.");
monitor.Stop();
using System.Collections.Concurrent;

namespace DownloadManager.Core;

public class QueueMonitor : IDisposable
{
    private readonly Thread _monitorThread;
    private readonly ConcurrentQueue<string> _logQueue;
    private bool _running = true;

    public QueueMonitor()
    {
        _logQueue = new ConcurrentQueue<string>();

        _monitorThread = new Thread(MonitorLoop)
        {
            IsBackground = true,
            Name = "QueueMonitor"
        };
    }
    public void Start() => _monitorThread.Start();
    public void Log(string message) => _logQueue.Enqueue(message);

    private void MonitorLoop()
    {
        while (_running || _logQueue.IsEmpty)
        {
            if (_logQueue.TryDequeue(out var msg))
                Console.WriteLine($"[LOG] {msg}");
            else
                Thread.Sleep(100);
        }
    }
    public void Stop() => _running = false;
    public void Dispose()
    {
        Stop();
        _monitorThread.Join();
    }
}
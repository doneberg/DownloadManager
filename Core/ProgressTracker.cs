namespace DownloadManager.Core;

public class ProgressTracker
{
    private int _completed;
    private int _failed;
    private readonly int _total;
    
    public  ProgressTracker(int total)
    {
        _total = total;
    }
    
    public void RecordSuccess() => Interlocked.Increment(ref _completed);
    
    public void RecordFailed() => Interlocked.Increment(ref _failed);

    public void Print()
    {
        Console.WriteLine($"Progress: {_completed}/{_total} done,  {_failed} failed");
    }
}
namespace NotificationManager.Hybrid.CSHelper;

public class Helper
{
    public delegate void WithinFunc();
    public delegate void AfterFunc();
    public static void TimeFunction(WithinFunc within, AfterFunc after, int ms)
    {
        //Task task = Task.Run(() => within());
        //if (task.Wait(TimeSpan.FromMilliseconds(ms)))
        //{
        //    after();
        //}
    }
}

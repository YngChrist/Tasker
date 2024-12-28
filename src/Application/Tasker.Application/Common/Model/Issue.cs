namespace Tasker.Application.Common.Model;

public class Issue
{
    public ulong Id { get; set; }
    public string TaskText { get; set; }
    public string StatusText { get; set; }
    public IssuePriority Priority { get; set; }
    public string Project { get; set; }
}
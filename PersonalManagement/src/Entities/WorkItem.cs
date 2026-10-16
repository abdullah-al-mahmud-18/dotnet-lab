namespace PersonalManagement.Entities;

public class WorkItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public WorkItemStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? StartedOn { get; set; }
    public DateTime? EndedOn { get; set; }
    public DateTime DeadLine { get; set; }
}

public enum WorkItemStatus
{
    Todo = 0,
    InProgress = 1,
    Done = 2
}

public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2
}
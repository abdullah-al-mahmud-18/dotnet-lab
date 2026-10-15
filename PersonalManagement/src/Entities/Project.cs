namespace PersonalManagement.Entities;

public class Project
{
    public Guid Id {get; set;}
    public string Name {get; set;} = null!;
    public string? Description {get; set;}
    public List<WorkItem> workItems {get; set;} = new List<WorkItem>();
    public ProjectStatus Status {get; set;}
    public DateTime CreatedOn {get; set;}
    public DateTime StartedOn {get; set;}
    public DateTime EndedOn {get; set;}
    public DateTime DeadLine {get; set;}
}

public enum ProjectStatus
{
    Todo = 0,
    InProgress = 1,
    Done = 2
}
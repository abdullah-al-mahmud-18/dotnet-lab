using PersonalManagement.Entities;

namespace PersonalManagement.Services;

public class WorkItemService
{
    public WorkItem CreateWorkItem(string name, Priority priority, DateTime deadline, string desc = "N/A")
    {
        return new WorkItem()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = desc,
            Status = WorkItemStatus.Todo,
            CreatedOn = DateTime.UtcNow,
            DeadLine = deadline
        };
    }
    public string GetWorkItemDetails(WorkItem workItem)
    {
        if (workItem is null) return "No work item to display";
        
        return string.Join(Environment.NewLine,
            $"===== {workItem.Name} Details =====",
            $"Id:          {workItem.Id}",
            $"Name:        {workItem.Name}",
            $"Description: {workItem.Description}",
            $"Status:      {workItem.Status}",
            $"Created On:  {workItem.CreatedOn.ToString("yyyy-MM-dd")}",
            $"Started On:  {workItem.StartedOn?.ToString("yyyy-MM-dd") ?? "N/A"}",
            $"Ended On:    {workItem.EndedOn?.ToString("yyyy-MM-dd") ?? "N/A"}",
            $"Deadline On: {workItem.DeadLine.ToString("yyyy-MM-dd")}"
        );
    }
}
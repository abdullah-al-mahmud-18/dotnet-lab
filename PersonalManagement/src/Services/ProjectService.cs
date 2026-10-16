using PersonalManagement.Entities;

namespace PersonalManagement.Services;

public class ProjectService
{
    public Project CreateProject(string name, DateTime deadline, string desc = "N/A")
    {
        return new Project()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = desc,
            Status = ProjectStatus.Todo,
            CreatedOn = DateTime.UtcNow,
            DeadLine = deadline
        };
    }

    public void AddWorkItem(Project project, WorkItem workItem)
    {
        project.workItems.Add(workItem);
    }

    public void AddWorkItems(Project project, WorkItem[] workItems)
    {
        foreach(WorkItem item in workItems)
        {
            project.workItems.Add(item);
        }
    }

    public string GetProjectDetails(Project project)
    {
        if (project is null) return "No project to display";

        string workItemsStr = $"===== Work Items of {project.Name} =====";
        WorkItemService wiService = new WorkItemService();

        if (project.workItems.Any())
        {
            foreach(WorkItem workItem in project.workItems)
            {
                workItemsStr = string.Join(Environment.NewLine, workItemsStr, wiService.GetWorkItemDetails(workItem));
            }            
        }

        return string.Join(Environment.NewLine,
            $"===== {project.Name} Details =====",
            $"Id:          {project.Id}",
            $"Name:        {project.Name}",
            $"Description: {project.Description}",
            $"Status:      {project.Status}",
            $"Created On:  {project.CreatedOn.ToString("yyyy-MM-dd")}",
            $"Started On:  {project.StartedOn?.ToString("yyyy-MM-dd") ?? "N/A"}",
            $"Ended On:    {project.EndedOn?.ToString("yyyy-MM-dd") ?? "N/A"}",
            $"Deadline On: {project.DeadLine.ToString("yyyy-MM-dd")}",
            $"Archived:    {(project.IsArchived ? "Yes" : "No")}",
            $"{(project.workItems.Any() ? workItemsStr : "")}"
        );
    }
}
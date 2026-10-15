using PersonalManagement.Entities;

namespace PersonalManagement.Services;

public class ProjectService
{
    public Project CreateProject(string name, DateTime deadline, string desc = "")
    {
        return new Project()
        {
            Id = new Guid(),
            Name = name,
            Description = desc,
            Status = ProjectStatus.Todo,
            CreatedOn = DateTime.UtcNow,
            DeadLine = deadline
        };
    }

    public string GetProjectDetails(Project project)
    {
        return $"{project.Id}\n{project.Name}\n{project.Description}\n{project.Status}\n{project.CreatedOn}\n{project.StartedOn}\n{project.EndedOn}\n{project.DeadLine}\n{project.IsArchived}";
    }
}
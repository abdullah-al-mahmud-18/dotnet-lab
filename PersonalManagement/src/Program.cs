using PersonalManagement.Services;
using PersonalManagement.Entities;

namespace PersonalManagement;
public class App
{
    public static void Main(string[] args)
    {
        ProjectService projectService = new ProjectService();
        WorkItemService wiService = new WorkItemService();

        WorkItem wItem1 = wiService.CreateWorkItem(".NET Lab", Priority.Low, new DateTime(2026, 10, 20));
        WorkItem wItem2 = wiService.CreateWorkItem("Python Lab", Priority.Low, new DateTime(2026, 10, 23));

        Project firstProject = projectService.CreateProject("First Project", DateTime.UtcNow);
        projectService.AddWorkItems(firstProject, [wItem1, wItem2]);
        Console.WriteLine(projectService.GetProjectDetails(firstProject));           

        Project secondProject = projectService.CreateProject("Second Project", DateTime.UtcNow);
        Console.WriteLine(projectService.GetProjectDetails(secondProject));           
    }
}



using PersonalManagement.Services;
using PersonalManagement.Entities;

namespace PersonalManagement;
public class App
{
    public static void Main(string[] args)
    {
        ProjectService projectService = new ProjectService();

        Project firstProject = projectService.CreateProject("First Project", DateTime.UtcNow);
        Console.WriteLine(projectService.GetProjectDetails(firstProject));           

        Project secondProject = projectService.CreateProject("Second Project", DateTime.UtcNow);
        Console.WriteLine(projectService.GetProjectDetails(secondProject));           
    }
}



using Microsoft.AspNetCore.Mvc.RazorPages;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProjectsModel : PageModel
{
    public List<Project> Projects { get; set; } = new List<Project>();


// Se obtiene los repositorios públicos de Github
    public async Task OnGetAsync()
    {
        var client = new GitHubClient(new ProductHeaderValue("Portfolio"));
        var repositories = await client.Repository.GetAllForUser("JimmyLeiZ");

        // Aquí se agrega las tecnologías para cada repositorio
        Projects = repositories.Select(repo => new Project
        {
            Repository = repo,
            Technologies = GetTechnologiesForRepo(repo.Name)
        }).ToList();
    }

    private List<string> GetTechnologiesForRepo(string repoName)
    {
        // Aquí se asigna las tecnologías según el nombre del repositorio
        return repoName switch
        {
            "actividad1" => new List<string> { "C#", ".NET" },
            "PROGRAMACION_3" => new List<string> { "HTML", "CSS" },
            "repo3" => new List<string> { "Python", "JavaScript" },
            _ => new List<string> { "Otras tecnologías" } // Por defecto
        };
    }
}

public class Project
{
    public Repository Repository { get; set; }
    public List<string> Technologies { get; set; }
}

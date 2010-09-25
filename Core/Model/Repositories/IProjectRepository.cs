
namespace WaveTech.Insight.Model.Repositories
{
	public interface IProjectRepository
	{
		Project GetProject(string filePath);
		Project SaveProject(Project project, string filePath);
	}
}

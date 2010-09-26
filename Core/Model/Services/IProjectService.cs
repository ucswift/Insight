
namespace WaveTech.Insight.Model.Services
{
	public interface IProjectService
	{
		Project GetProject(string filePath);
		Project SaveProject(Project project, string filePath);
	}
}

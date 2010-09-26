using WaveTech.Insight.Model;
using WaveTech.Insight.Model.Repositories;
using WaveTech.Insight.Model.Services;

namespace WaveTech.Insight.Services
{
	public class ProjectService : IProjectService
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectService(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		public Project GetProject(string filePath)
		{
			return _projectRepository.GetProject(filePath);
		}

		public Project SaveProject(Project project, string filePath)
		{
			return _projectRepository.SaveProject(project, filePath);
		}
	}
}
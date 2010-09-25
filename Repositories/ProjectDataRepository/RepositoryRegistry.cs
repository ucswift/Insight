using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Repositories;

namespace WaveTech.Insight.Repositories.ProjectDataRepository
{
	internal class RepositoryRegistry : Registry
	{
		public RepositoryRegistry()
		{
			For<IProjectRepository>().Use<ProjectRepository>();
		}
	}
}
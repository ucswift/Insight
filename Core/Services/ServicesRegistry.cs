using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Services;

namespace WaveTech.Insight.Services
{
	public class ServicesRegistry : Registry
	{
		public ServicesRegistry()
		{
			For<IAnalysisService>().Use<AnalysisService>();
			For<IImageService>().Use<ImageService>();
			For<IReportingService>().Use<ReportingService>();
			For<IProjectService>().Use<ProjectService>();
		}
	}
}

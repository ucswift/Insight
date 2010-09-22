using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Services;

namespace WaveTech.Insight.Services
{
	public class ServicesRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IAnalysisService>().TheDefault.Is.OfConcreteType<AnalysisService>();
			ForRequestedType<IImageService>().TheDefault.Is.OfConcreteType<ImageService>();
			ForRequestedType<IReportingService>().TheDefault.Is.OfConcreteType<ReportingService>();
		}
	}
}

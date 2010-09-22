using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Analyzers;

namespace WaveTech.Insight.Analyzers
{
	public class AnalyzerRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<ILocAnalyzer>().TheDefault.Is.OfConcreteType<CSharpLocAnalyzer>().WithName("CSharpLocAnalyzer");
		}
	}
}
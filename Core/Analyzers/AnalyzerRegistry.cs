using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Analyzers;

namespace WaveTech.Insight.Analyzers
{
	internal class AnalyzerRegistry : Registry
	{
		public AnalyzerRegistry()
		{
			For<ILocAnalyzer>().Use<CSharpLocAnalyzer>().Named("CSharpLocAnalyzer");
			For<ILocAnalyzer>().Use<AspxLocAnalyzer>().Named("AspxLocAnalyzer");
		}
	}
}
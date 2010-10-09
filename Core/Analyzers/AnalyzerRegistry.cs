using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Analyzers;

namespace WaveTech.Insight.Analyzers
{
	public class AnalyzerRegistry : Registry
	{
		public AnalyzerRegistry()
		{
			For<ILocAnalyzer>().Use<CSharpLocAnalyzer>().Named("CSharpLocAnalyzer");
			For<ILocAnalyzer>().Use<AspxLocAnalyzer>().Named("AspxLocAnalyzer");
			For<ILocAnalyzer>().Use<SqlLocAnalyzer>().Named("SqlLocAnalyzer");
			For<ILocAnalyzer>().Use<XamlLocAnalyzer>().Named("XamlLocAnalyzer");
		}
	}
}
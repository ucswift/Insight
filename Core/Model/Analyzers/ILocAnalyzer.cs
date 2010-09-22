namespace WaveTech.Insight.Model.Analyzers
{
	public interface ILocAnalyzer
	{
		LocReport CompileLocReport(string filePath);
		LocReport CompileLocReport(string[] fileData);
		SourceLine InspectLine(string line, ref bool inMultilineComment);
	}
}
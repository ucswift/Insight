using System.Collections.Generic;

namespace WaveTech.Insight.Model.Services
{
	public interface IAnalysisService
	{
		Dictionary<FileTypes, List<LocReport>> AnalyzeDirectory(string directoryPath, string validExtensions);
		LocReport AnalyzeFile(string filePath);
	}
}
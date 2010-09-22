using System.Collections.Generic;
using System.IO;
using System.Linq;
using WaveTech.Insight.Framework;
using WaveTech.Insight.Model;
using WaveTech.Insight.Model.Analyzers;
using WaveTech.Insight.Model.Services;

namespace WaveTech.Insight.Services
{
	public class AnalysisService : IAnalysisService
	{
		public Dictionary<FileTypes, List<LocReport>> AnalyzeDirectory(string directoryPath)
		{
			Dictionary<FileTypes, List<string>> preProcess = new Dictionary<FileTypes, List<string>>();
			Dictionary<FileTypes, List<LocReport>> postProcess = new Dictionary<FileTypes, List<LocReport>>();

			//string validExtensions = "*.cs,*.aspx,*.sql,*.xaml";
			string validExtensions = "*.cs";
			string[] extFilter = validExtensions.Split(new char[] { ',' });

			DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);

			foreach (string extension in extFilter)
			{
				switch (Path.GetExtension(extension))
				{
					case ".cs":
						List<FileInfo> cSharpFiles = dirInfo.GetFiles(extension).ToList();
						List<string> cSharpFilePaths = new List<string>();

						foreach (FileInfo fi in cSharpFiles)
							cSharpFilePaths.Add(fi.FullName);

						preProcess.Add(FileTypes.CSharp, cSharpFilePaths);
						break;
					case ".aspx":
						break;
					case ".sql":
						break;
					case ".xaml":
						break;
					default:
						break;
				}
			}

			foreach (var v in preProcess)
			{
				List<LocReport> reports = new List<LocReport>();

				foreach (var i in v.Value)
					reports.Add(AnalyzeFile(i));

				postProcess.Add(v.Key, reports);
			}

			return postProcess;
		}

		public LocReport AnalyzeFile(string filePath)
		{
			LocReport report = null;

			switch (Path.GetExtension(filePath))
			{
				case ".cs":
					ILocAnalyzer cSharpAnalyzer = ObjectLocator.GetInstance<ILocAnalyzer>("CSharpLocAnalyzer");
					report = cSharpAnalyzer.CompileLocReport(filePath);
					break;
				case ".aspx":
					break;
				case ".sql":
					break;
				case ".xaml":
					break;
				default:
					break;
			}

			return report;
		}
	}
}
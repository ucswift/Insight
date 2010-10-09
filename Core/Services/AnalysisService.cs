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
		public Dictionary<FileTypes, List<LocReport>> AnalyzeDirectory(string directoryPath, string validExtensions)
		{
			Dictionary<FileTypes, List<string>> preProcess = new Dictionary<FileTypes, List<string>>();
			Dictionary<FileTypes, List<LocReport>> postProcess = new Dictionary<FileTypes, List<LocReport>>();

			//string validExtensions = "*.cs,*.aspx,*.sql,*.xaml";
			//string validExtensions = "*.cs";

			string[] extFilter = validExtensions.Split(new char[] { ',' });

			DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);

			foreach (string extension in extFilter)
			{
				switch (Path.GetExtension(extension))
				{
					case "*.cs":
						List<FileInfo> cSharpFiles = dirInfo.GetFiles(extension).ToList();
						List<string> cSharpFilePaths = new List<string>();

						foreach (FileInfo fi in cSharpFiles)
							cSharpFilePaths.Add(fi.FullName);

						preProcess.Add(FileTypes.CSharp, cSharpFilePaths);
						break;
					case "*.aspx":
						List<FileInfo> aspxFiles = dirInfo.GetFiles(extension).ToList();
						List<string> aspxFilePaths = new List<string>();

						foreach (FileInfo fi in aspxFiles)
							aspxFilePaths.Add(fi.FullName);

						preProcess.Add(FileTypes.Aspx, aspxFilePaths);
						break;
					case "*.sql":
						List<FileInfo> sqlFiles = dirInfo.GetFiles(extension).ToList();
						List<string> sqlFilePaths = new List<string>();

						foreach (FileInfo fi in sqlFiles)
							sqlFilePaths.Add(fi.FullName);

						preProcess.Add(FileTypes.Sql, sqlFilePaths);
						break;
					case "*.xaml":
						List<FileInfo> xamlFiles = dirInfo.GetFiles(extension).ToList();
						List<string> xamlFilePaths = new List<string>();

						foreach (FileInfo fi in xamlFiles)
							xamlFilePaths.Add(fi.FullName);

						preProcess.Add(FileTypes.Sql, xamlFilePaths);
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
					ILocAnalyzer aspxAnalyzer = ObjectLocator.GetInstance<ILocAnalyzer>("AspxLocAnalyzer");
					report = aspxAnalyzer.CompileLocReport(filePath);
					break;
				case ".sql":
					ILocAnalyzer sqlAnalyzer = ObjectLocator.GetInstance<ILocAnalyzer>("SqlLocAnalyzer");
					report = sqlAnalyzer.CompileLocReport(filePath);
					break;
				case ".xaml":
					ILocAnalyzer xamlAnalyzer = ObjectLocator.GetInstance<ILocAnalyzer>("XamlLocAnalyzer");
					report = xamlAnalyzer.CompileLocReport(filePath);
					break;
				default:
					break;
			}

			return report;
		}
	}
}
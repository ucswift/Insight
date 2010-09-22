using System;
using System.IO;
using WaveTech.Insight.Model;
using WaveTech.Insight.Model.Analyzers;

namespace WaveTech.Insight.Analyzers
{
	public class CSharpLocAnalyzer : ILocAnalyzer
	{
		private const int CommentLength = 2;

		public LocReport CompileLocReport(string filePath)
		{
			LocReport report = null;

			if (File.Exists(filePath))
			{
				string[] lines = File.ReadAllLines(filePath);
				report = CompileLocReport(lines);
			}

			return report;
		}

		public LocReport CompileLocReport(string[] fileData)
		{
			LocReport report = new LocReport();
			bool inMultilineComment = false;

			foreach (string line in fileData)
			{
				report.Lines.Add(InspectLine(line, ref inMultilineComment));
			}

			return report;
		}

		public SourceLine InspectLine(string line, ref bool inMultilineComment)
		{
			SourceLine sourceLine = new SourceLine();

			if (line.Trim().StartsWith((@"/*")))
				inMultilineComment = true;

			if (inMultilineComment)
				sourceLine.LineType = LineTypes.Comment;
			else if (String.IsNullOrEmpty(line))
				sourceLine.LineType = LineTypes.Empty;
			else if (line.Trim().Length <= 0)
				sourceLine.LineType = LineTypes.Empty;
			else if (line.Trim().StartsWith(@"//") || line.Trim().StartsWith(@"/*"))
				sourceLine.LineType = LineTypes.Comment;
			else if (line.IndexOf(@"//") > 0 || (line.IndexOf(@"/*") > 0 && line.IndexOf(@"*/") > 0))
				sourceLine.LineType = LineTypes.SourceAndComment;
			else if (line.Trim().StartsWith("using") && line.Trim().EndsWith(";"))
				sourceLine.LineType = LineTypes.IncludesDirective;
			else if (line.Trim().Equals("{", StringComparison.OrdinalIgnoreCase) || line.Trim().Equals("}", StringComparison.OrdinalIgnoreCase))
				sourceLine.LineType = LineTypes.Syntax;
			else
				sourceLine.LineType = LineTypes.Source;

			if (inMultilineComment)
				if (line.IndexOf(@"*/") > 0)
					inMultilineComment = false;

			if (sourceLine.LineType != LineTypes.Empty)
			{
				if (line.IndexOf(@"//") >= 0 || line.IndexOf(@"/*") >= 0 || line.IndexOf(@"*/") >= 0)
				{
					if (line.Trim().IndexOf(@"//") == 0)
						sourceLine.CommentLength = line.Trim().Length;
					else if (line.Trim().IndexOf(@"//") > 0)
					{
						sourceLine.CommentLength = line.Trim().Length - line.Trim().IndexOf(@"//");
						sourceLine.SourceLength = line.Trim().IndexOf(@"//");
					}
					else if (line.IndexOf(@"/*") > 0)
					{
						int start = line.IndexOf(@"/*");
						int end = line.IndexOf(@"*/");

						if (end >= 0)
						{
							sourceLine.CommentLength = end - start - CommentLength;
							sourceLine.SourceLength = line.Trim().Length - sourceLine.CommentLength - (CommentLength * 2);
						}
						else
						{
							sourceLine.CommentLength = line.Trim().Length - start;
							sourceLine.SourceLength = start - CommentLength;
						}

					}
					else if (line.IndexOf(@"*/") > 0)
					{
						sourceLine.CommentLength = line.IndexOf(@"*/");
						sourceLine.SourceLength = line.Trim().Length - line.IndexOf(@"*/");
					}
				}
				else
				{
					sourceLine.SourceLength = line.Trim().Length;
				}
			}

			return sourceLine;
		}
	}
}
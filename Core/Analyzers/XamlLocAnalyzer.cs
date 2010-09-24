using System;
using System.IO;
using WaveTech.Insight.Model;
using WaveTech.Insight.Model.Analyzers;

namespace WaveTech.Insight.Analyzers
{
	public class XamlLocAnalyzer : ILocAnalyzer
	{
		private const int StartCommentSyntaxLength = 4;
		private const int EndCommentSyntaxLength = 3;
		private const int CombinedCommentLength = StartCommentSyntaxLength + EndCommentSyntaxLength;

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

			if (line.Trim().StartsWith((@"<!--")))
				inMultilineComment = true;

			if (inMultilineComment)
				sourceLine.LineType = LineTypes.Comment;
			else if (String.IsNullOrEmpty(line))
				sourceLine.LineType = LineTypes.Empty;
			else if (line.Trim().Length <= 0)
				sourceLine.LineType = LineTypes.Empty;
			else if (line.IndexOf(@"<!--") > 0 || (line.IndexOf(@"-->") > 0))
				sourceLine.LineType = LineTypes.SourceAndComment;
			else
				sourceLine.LineType = LineTypes.Source;

			if (inMultilineComment)
				if (line.IndexOf(@"-->") > 0)
					inMultilineComment = false;

			if (sourceLine.LineType != LineTypes.Empty)
			{
				if (line.IndexOf(@"<!--") >= 0 || line.IndexOf(@"-->") >= 0)
				{
					if (line.IndexOf(@"<!--") >= 0)
					{
						int start = line.IndexOf(@"<!--");
						int end = line.IndexOf(@"-->");

						if (end >= 0)
						{
							sourceLine.CommentLength = end - start - StartCommentSyntaxLength;
							sourceLine.SourceLength = line.Trim().Length - sourceLine.CommentLength - CombinedCommentLength;
						}
						else
						{
							sourceLine.CommentLength = line.Trim().Length - start;
							sourceLine.SourceLength = start - StartCommentSyntaxLength;
						}

					}
					else if (line.IndexOf(@"-->") > 0)
					{
						sourceLine.CommentLength = line.IndexOf(@"-->");
						sourceLine.SourceLength = line.Trim().Length - line.IndexOf(@"-->");
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

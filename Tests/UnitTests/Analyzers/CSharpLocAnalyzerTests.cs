using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using WaveTech.Insight.Analyzers;
using WaveTech.Insight.Model;

namespace UnitTests.Analyzers
{
	namespace CSharpLocAnalyzerTests
	{
		public class with_the_csharp_loc_analyzer : FixtureBase
		{
			protected CSharpLocAnalyzer cSharpLocAnalyzer;

			protected string singleCSharpCodeLine = "int i = 1 + 50 - 10 / 2;";
			protected string singleCSharpCommentLine = "//Just a comment about some code";
			protected string singleCSharpCodeLineWithComment = "int i = 1 + 50 - 10 / 2; // This code is soooooo cool";
			protected string[] multipleCSharpLines;

			protected bool inMultilineComment;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				cSharpLocAnalyzer = new CSharpLocAnalyzer();
				inMultilineComment = false;

				multipleCSharpLines = new string[13];
				multipleCSharpLines[0] = "";
				multipleCSharpLines[1] = " ";
				multipleCSharpLines[2] = "/* Function that does stuff";
				multipleCSharpLines[3] = "   and it's soo killer */";
				multipleCSharpLines[4] = "public int MyFunction()";
				multipleCSharpLines[5] = "{ ";
				multipleCSharpLines[6] = "   int i = 5;";
				multipleCSharpLines[7] = "   int t = 10;";
				multipleCSharpLines[8] = "   int it = i + t; // I + T = IT Sweet!";
				multipleCSharpLines[9] = "";
				multipleCSharpLines[10] = "   // Return the value";
				multipleCSharpLines[11] = "   return it;";
				multipleCSharpLines[12] = "}";

			}
		}

		[TestFixture]
		public class when_analysing_a_csharp_file : with_the_csharp_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
				path = path.Replace("file:\\", "");

				LocReport report = cSharpLocAnalyzer.CompileLocReport(path + "\\Files\\TestCSharpFile.cs");
			}
		}

		[TestFixture]
		public class when_analysing_a_csharp_code_line : with_the_csharp_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLine, ref inMultilineComment);

				Assert.NotNull(line);
			}

			[Test]
			public void should_have_a_line_type_of_source()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLine, ref inMultilineComment);

				Assert.AreEqual(LineTypes.Source, line.LineType);
			}

			[Test]
			public void should_have_a_total_length_matching_data_length()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLine, ref inMultilineComment);

				Assert.AreEqual(singleCSharpCodeLine.Length, line.TotalLength);
			}

			[Test]
			public void should_have_a_source_length_of_24()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLine, ref inMultilineComment);

				Assert.AreEqual(24, line.SourceLength);
			}

			[Test]
			public void should_have_a_comment_length_of_0()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLine, ref inMultilineComment);

				Assert.AreEqual(0, line.CommentLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_csharp_comment_line : with_the_csharp_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCommentLine, ref inMultilineComment);

				Assert.NotNull(line);
			}

			[Test]
			public void should_have_a_line_type_of_comment()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCommentLine, ref inMultilineComment);

				Assert.AreEqual(LineTypes.Comment, line.LineType);
			}

			[Test]
			public void should_have_a_total_length_matching_data_length()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCommentLine, ref inMultilineComment);

				Assert.AreEqual(singleCSharpCommentLine.Length, line.TotalLength);
			}

			[Test]
			public void should_have_a_source_length_of_0()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCommentLine, ref inMultilineComment);

				Assert.AreEqual(0, line.SourceLength);
			}

			[Test]
			public void should_have_a_comment_length_of_32()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCommentLine, ref inMultilineComment);

				Assert.AreEqual(32, line.CommentLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_csharp_inlinecomment_line : with_the_csharp_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLineWithComment, ref inMultilineComment);

				Assert.NotNull(line);
			}

			[Test]
			public void should_have_a_line_type_of_sourceandcomment()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLineWithComment, ref inMultilineComment);

				Assert.AreEqual(LineTypes.SourceAndComment, line.LineType);
			}

			[Test]
			public void should_have_a_total_length_matching_data_length()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLineWithComment, ref inMultilineComment);

				Assert.AreEqual(singleCSharpCodeLineWithComment.Length, line.TotalLength);
			}

			[Test]
			public void should_have_a_source_length_of_25()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLineWithComment, ref inMultilineComment);

				Assert.AreEqual(25, line.SourceLength);
			}

			[Test]
			public void should_have_a_comment_length_of_28()
			{
				SourceLine line = cSharpLocAnalyzer.InspectLine(singleCSharpCodeLineWithComment, ref inMultilineComment);

				Assert.AreEqual(28, line.CommentLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_mutliple_csharp_lines : with_the_csharp_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				Assert.NotNull(report);
			}

			[Test]
			public void should_have_13_lines()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				Assert.AreEqual(13, report.Lines.Count);
			}

			[Test]
			public void should_have_4_source_only_lines()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				var sourceOnlyLines = from l in report.Lines
															where l.LineType == LineTypes.Source
															select l;

				Assert.AreEqual(4, sourceOnlyLines.Count());
			}

			[Test]
			public void should_have_3_comment_only_lines()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				var commentOnlyLines = from l in report.Lines
															 where l.LineType == LineTypes.Comment
															 select l;

				Assert.AreEqual(3, commentOnlyLines.Count());
			}

			[Test]
			public void should_have_1_commentandsource_lines()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				var commentAndSourceLines = from l in report.Lines
																		where l.LineType == LineTypes.SourceAndComment
																		select l;

				Assert.AreEqual(1, commentAndSourceLines.Count());
			}

			[Test]
			public void should_have_3_empty_lines()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				var emptyLines = from l in report.Lines
												 where l.LineType == LineTypes.Empty
												 select l;

				Assert.AreEqual(3, emptyLines.Count());
			}

			[Test]
			public void should_have_2_syntax_lines()
			{
				LocReport report = cSharpLocAnalyzer.CompileLocReport(multipleCSharpLines);

				var syntaxLines = from l in report.Lines
													where l.LineType == LineTypes.Syntax
													select l;

				Assert.AreEqual(2, syntaxLines.Count());
			}
		}
	}
}

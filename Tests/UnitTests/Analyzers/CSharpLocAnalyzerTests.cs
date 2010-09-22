using System.IO;
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

			protected bool inMultilineComment;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				cSharpLocAnalyzer = new CSharpLocAnalyzer();
				inMultilineComment = false;
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
	}
}

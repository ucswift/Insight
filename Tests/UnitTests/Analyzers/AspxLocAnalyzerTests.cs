
using System.Linq;
using NUnit.Framework;
using WaveTech.Insight.Analyzers;
using WaveTech.Insight.Model;

namespace UnitTests.Analyzers
{
	namespace AspxLocAnalyzerTests
	{
		public class with_the_aspx_loc_analyzer : FixtureBase
		{
			protected AspxLocAnalyzer locAnalyzer;

			protected string singleLine = "<div class=\"module-wrapper\">";
			protected string singleCommentLine = "<!--Just a comment about some code -->";
			protected string singleLineWithComment = "<div class=\"module-wrapper\"> <!--Nothing goes here-->";
			protected string[] multipleLines;

			protected bool inMultilineComment;

			protected int commentSyntaxLength = 7;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				locAnalyzer = new AspxLocAnalyzer();
				inMultilineComment = false;

				multipleLines = new string[13];
				multipleLines[0] = "";
				multipleLines[1] = "<!--End sub-right-->";
				multipleLines[2] = "<div id=\"sub-left\">";
				multipleLines[3] = "   <h2>Benefits</h2>";
				multipleLines[4] = "<div class=\"featured-content\"><!--Leave empty-->";
				multipleLines[5] = "  <p><strong>Our Software as a Service Platform has many benefits</strong></p>";
				multipleLines[6] = "   <ul>";
				multipleLines[7] = "   <li>&bull; Access from anywhere, anytime</li>";
				multipleLines[8] = "   <li>&bull; No maintenance or hassles</li>";
				multipleLines[9] = "   </ul> <!--End featured-content-->";
				multipleLines[10] = " </div>";
				multipleLines[11] = "</div>";
				multipleLines[12] = " ";

			}
		}

		[TestFixture]
		public class when_analysing_a_aspx_code_line : with_the_aspx_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLine, ref inMultilineComment);

				Assert.NotNull(line);
			}

			[Test]
			public void should_have_a_line_type_of_source()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLine, ref inMultilineComment);

				Assert.AreEqual(LineTypes.Source, line.LineType);
			}

			[Test]
			public void should_have_a_total_length_matching_data_length()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLine, ref inMultilineComment);

				Assert.AreEqual(singleLine.Length, line.TotalLength);
			}

			[Test]
			public void should_have_a_source_length_of_total_length()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLine, ref inMultilineComment);

				Assert.AreEqual(singleLine.Length, line.SourceLength);
			}

			[Test]
			public void should_have_a_comment_length_of_0()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLine, ref inMultilineComment);

				Assert.AreEqual(0, line.CommentLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_aspx_comment_line : with_the_aspx_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.NotNull(line);
			}

			[Test]
			public void should_have_a_line_type_of_comment()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.AreEqual(LineTypes.Comment, line.LineType);
			}

			[Test]
			public void should_have_a_total_length_matching_data_length()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.AreEqual(singleCommentLine.Length - commentSyntaxLength, line.TotalLength);
			}

			[Test]
			public void should_have_a_comment_length_of_total_length()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.AreEqual(singleCommentLine.Length - commentSyntaxLength, line.CommentLength);
			}

			[Test]
			public void should_have_a_source_length_of_0()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.AreEqual(0, line.SourceLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_aspx_sourceandcomment_line : with_the_aspx_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.NotNull(line);
			}

			[Test]
			public void should_have_a_line_type_of_sourceandcomment()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(LineTypes.SourceAndComment, line.LineType);
			}

			[Test]
			public void should_have_a_total_length_matching_data_length()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(singleLineWithComment.Length - commentSyntaxLength, line.TotalLength);
			}

			[Test]
			public void should_have_a_comment_length_of_17()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(17, line.CommentLength);
			}

			[Test]
			public void should_have_a_source_length_of_29()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(29, line.SourceLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_mutliple_aspx_lines : with_the_aspx_loc_analyzer
		{
			[Test]
			public void should_not_be_null()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				Assert.NotNull(report);
			}

			[Test]
			public void should_have_13_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				Assert.AreEqual(13, report.Lines.Count);
			}

			[Test]
			public void should_have_8_source_only_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var sourceOnlyLines = from l in report.Lines
															where l.LineType == LineTypes.Source
															select l;

				Assert.AreEqual(8, sourceOnlyLines.Count());
			}

			[Test]
			public void should_have_1_comment_only_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var commentOnlyLines = from l in report.Lines
															 where l.LineType == LineTypes.Comment
															 select l;

				Assert.AreEqual(1, commentOnlyLines.Count());
			}

			[Test]
			public void should_have_2_commentandsource_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var commentAndSourceLines = from l in report.Lines
																		where l.LineType == LineTypes.SourceAndComment
																		select l;

				Assert.AreEqual(2, commentAndSourceLines.Count());
			}

			[Test]
			public void should_have_2_empty_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var emptyLines = from l in report.Lines
												 where l.LineType == LineTypes.Empty
												 select l;

				Assert.AreEqual(2, emptyLines.Count());
			}
		}
	}
}
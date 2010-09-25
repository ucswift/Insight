using System.Linq;
using NUnit.Framework;
using WaveTech.Insight.Analyzers;
using WaveTech.Insight.Model;

namespace UnitTests.Analyzers
{
	namespace SqlLocAnalyzerTests
	{
		public class with_the_sql_loc_analyzer : FixtureBase
		{
			protected SqlLocAnalyzer locAnalyzer;

			protected string singleLine = "SELECT TOP 1000 * FROM dbo.Customers";
			protected string singleCommentLine = "-- Just a comment about some code";
			protected string singleLineWithComment = "SELECT TOP 1000 * FROM dbo.Customers /* Main menu tooltip */";
			protected string[] multipleLines;

			protected bool inMultilineComment;

			protected int commentSyntaxLength = 2;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				locAnalyzer = new SqlLocAnalyzer();
				inMultilineComment = false;

				multipleLines = new string[13];
				multipleLines[0] = "";
				multipleLines[1] = "/****** Object:  StoredProcedure [dbo].[MyCoolSproc]    Script Date: 01/01/2010 18:21:00 ******/";
				multipleLines[2] = "SET QUOTED_IDENTIFIER OFF";
				multipleLines[3] = "GO";
				multipleLines[4] = "-- This Sproc does cool stuff";
				multipleLines[5] = "PROCEDURE [dbo].[MyCoolSproc]";
				multipleLines[6] = "SELECT Name,Address,Money TOP 100000 FROM dbo.Customers c";
				multipleLines[7] = "    INNER JOIN dbo.AllTheCustomersMoney atcm ON c.CustomerId = atcm.CustomerId";
				multipleLines[8] = "    INNER JOIN dbo.Addresses a ON a.CustomerId = c.CustomerId";
				multipleLines[9] = "ORDER BY NAME DESC; /* We need their money in alphabetical order */";
				multipleLines[10] = " ";
				multipleLines[11] = "-- Were finished in this SPROC --";
				multipleLines[12] = "";

			}
		}

		[TestFixture]
		public class when_analysing_a_sql_code_line : with_the_sql_loc_analyzer
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
		public class when_analysing_a_sql_comment_line : with_the_sql_loc_analyzer
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

				Assert.AreEqual(singleCommentLine.Length, line.TotalLength);
			}

			[Test]
			public void should_have_a_comment_length_of_total_length()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.AreEqual(singleCommentLine.Length, line.CommentLength);
			}

			[Test]
			public void should_have_a_source_length_of_0()
			{
				SourceLine line = locAnalyzer.InspectLine(singleCommentLine, ref inMultilineComment);

				Assert.AreEqual(0, line.SourceLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_sql_sourceandcomment_line : with_the_sql_loc_analyzer
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

				Assert.AreEqual(singleLineWithComment.Length - commentSyntaxLength * 2, line.TotalLength);
			}

			[Test]
			public void should_have_a_comment_length_of_19()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(19, line.CommentLength);
			}

			[Test]
			public void should_have_a_source_length_of_37()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(37, line.SourceLength);
			}
		}

		[TestFixture]
		public class when_analysing_a_mutliple_sql_lines : with_the_sql_loc_analyzer
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
			public void should_have_6_source_only_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var sourceOnlyLines = from l in report.Lines
															where l.LineType == LineTypes.Source
															select l;

				Assert.AreEqual(6, sourceOnlyLines.Count());
			}

			[Test]
			public void should_have_3_comment_only_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var commentOnlyLines = from l in report.Lines
															 where l.LineType == LineTypes.Comment
															 select l;

				Assert.AreEqual(3, commentOnlyLines.Count());
			}

			[Test]
			public void should_have_1_commentandsource_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var commentAndSourceLines = from l in report.Lines
																		where l.LineType == LineTypes.SourceAndComment
																		select l;

				Assert.AreEqual(1, commentAndSourceLines.Count());
			}

			[Test]
			public void should_have_3_empty_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var emptyLines = from l in report.Lines
												 where l.LineType == LineTypes.Empty
												 select l;

				Assert.AreEqual(3, emptyLines.Count());
			}
		}
	}
}

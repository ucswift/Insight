using NUnit.Framework;
using WaveTech.Insight.Analyzers;

namespace UnitTests.Analyzers
{
	namespace SqlLocAnalyzerTests
	{
		public class with_the_sql_loc_analyzer : FixtureBase
		{
			protected SqlLocAnalyzer locAnalyzer;

			protected string singleLine = "<Grid odc:ImageRenderOptions.LargeImageScalingMode=\"NearestNeighbor\" odc:RibbonChrome.IsGrayScaleEnabled=\"true\" DataContext=\"{Binding ElementName=main}\">";
			protected string singleCommentLine = "<!--Just a comment about some code -->";
			protected string singleLineWithComment = "<odc:RibbonToolTip Title=\"Application Menu\" Description=\"Click here to save, open or exit.\" /> <!-- Main menu tooltip-->";
			protected string[] multipleLines;

			protected bool inMultilineComment;

			protected int commentSyntaxLength = 7;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				locAnalyzer = new SqlLocAnalyzer();
				inMultilineComment = false;

				multipleLines = new string[13];
				multipleLines[0] = "";
				multipleLines[1] = "<!--Home menu bar group-->";
				multipleLines[2] = "<odc:RibbonTabItem Title=\"Home\" odc:KeyTip.Key=\"H\">";
				multipleLines[3] = "   <odc:RibbonGroup Title=\"General\" odc:KeyTip.Key=\"GA\">";
				multipleLines[4] = "       <odc:RibbonButton Content=\"New\" MinWidth=\"54\" odc:RibbonBar.MinSize=\"Medium\" odc:KeyTip.Key=\"S\" SmallImage=\"img/Files-48x48.png\" LargeImage=\"img/Files-48x48.png\" Command=\"{x:Static local:Commands.NewCommand}\" /><!-- New menu button -->";
				multipleLines[5] = "       <odc:RibbonButton Content=\"Open\" MinWidth=\"54\" odc:RibbonBar.MinSize=\"Medium\" odc:KeyTip.Key=\"S\" SmallImage=\"img/Folders-32x32.png\" LargeImage=\"img/Folders-32x32.png\" Command=\"{x:Static local:Commands.OpenCommand}\" />";
				multipleLines[6] = "       <odc:RibbonButton Content=\"Save\" MinWidth=\"54\" odc:RibbonBar.MinSize=\"Medium\" odc:KeyTip.Key=\"S\" SmallImage=\"img/Drive-48x48.png\" LargeImage=\"img/Drive-48x48.png\" Command=\"{x:Static local:Commands.SaveCommand}\" />";
				multipleLines[7] = "   </odc:RibbonGroup>";
				multipleLines[8] = "</odc:RibbonTabItem>";
				multipleLines[9] = "<!-- End Home Menugroup -->";
				multipleLines[10] = " ";
				multipleLines[11] = "</odc:RibbonBar.Tabs>";
				multipleLines[12] = "</odc:RibbonBar>";

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

				Assert.AreEqual(singleLineWithComment.Length - commentSyntaxLength, line.TotalLength);
			}

			[Test]
			public void should_have_a_comment_length_of_18()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(18, line.CommentLength);
			}

			[Test]
			public void should_have_a_source_length_of_95()
			{
				SourceLine line = locAnalyzer.InspectLine(singleLineWithComment, ref inMultilineComment);

				Assert.AreEqual(95, line.SourceLength);
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
			public void should_have_8_source_only_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var sourceOnlyLines = from l in report.Lines
															where l.LineType == LineTypes.Source
															select l;

				Assert.AreEqual(8, sourceOnlyLines.Count());
			}

			[Test]
			public void should_have_2_comment_only_lines()
			{
				LocReport report = locAnalyzer.CompileLocReport(multipleLines);

				var commentOnlyLines = from l in report.Lines
															 where l.LineType == LineTypes.Comment
															 select l;

				Assert.AreEqual(2, commentOnlyLines.Count());
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

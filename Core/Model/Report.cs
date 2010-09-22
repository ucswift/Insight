namespace WaveTech.Insight.Model
{
	public class Report
	{
		public int LinesOfCode { get; set; }
		public int LinesOfComments { get; set; }
		public int LinesOfSynatx { get; set; }
		public int LinesOfCodeAndComments { get; set; }
		public int LinesOfSynatxAndComments { get; set; }

		public int TotalLines
		{
			get { return LinesOfCode + LinesOfComments + LinesOfSynatx + LinesOfCodeAndComments + LinesOfSynatxAndComments; }
		}

		public double PersonMonths;
		public double ProjectCost;
	}
}
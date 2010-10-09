using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveTech.Insight.Framework;

namespace WaveTech.Insight.Model
{
	public class Project
	{
		public string Name { get; set; }
		public string DirectoryRoot { get; set; }
		public float CostPerHour { get; set; }

		public DictionaryProxy<FileTypes, float> CodeWeighting { get; set; }
		public DictionaryProxy<FileTypes, List<LocReport>> Sloc { get; set; }

		public int WorkDayHours { get; set; }
		public int WorkDayPerMonth { get; set; }
		public int WorkingMonths { get; set; }

		public double EffortAdjustmentFactor { get; set; }
		public double ProjectComplexity { get; set; }

		public Project()
		{
			CodeWeighting = new DictionaryProxy<FileTypes, float>(new Dictionary<FileTypes, float>());
			Sloc = new DictionaryProxy<FileTypes, List<LocReport>>(new Dictionary<FileTypes, List<LocReport>>());

			WorkDayHours = 8;
			WorkDayPerMonth = 20;
			WorkingMonths = 12;

			EffortAdjustmentFactor = 0.6;
			ProjectComplexity = 1.04;
		}

		public string GetExtensionsToProcess()
		{
			StringBuilder sb = new StringBuilder();

			int i = 0;
			foreach (var v in CodeWeighting.Original)
			{
				i++;

				switch (v.Key)
				{
					case FileTypes.CSharp:
						sb.Append("*.cs");
						break;
					case FileTypes.Aspx:
						sb.Append("*.aspx");
						break;
					case FileTypes.Sql:
						sb.Append("*.sql");
						break;
					case FileTypes.Xaml:
						sb.Append("*.xaml");
						break;
					case FileTypes.Html:
						sb.Append("*.html");
						break;
				}

				if (i < CodeWeighting.Original.Count)
					sb.Append(",");
			}

			return sb.ToString();
		}

		public int GetTotalLineCount(LineTypes lineType, FileTypes fileType, bool scale)
		{
			var sloc = from v in Sloc.ToDictionary()[fileType]
								 from x in v.Lines
								 where x.LineType == lineType
								 select x;

			return sloc.Count();
		}
	}
}
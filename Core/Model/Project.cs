using System.Collections.Generic;
using System.Linq;
using WaveTech.Insight.Framework;

namespace WaveTech.Insight.Model
{
	public class Project
	{
		public string Name { get; set; }
		public float CostPerHour { get; set; }
		public DictionaryProxy<FileTypes, int> CodeWeighting { get; set; }
		public DictionaryProxy<FileTypes, List<LocReport>> Sloc { get; set; }

		public int WorkDayHours { get; set; }
		public int WorkDayPerMonth { get; set; }
		public int WorkingMonths { get; set; }

		public double EffortAdjustmentFactor { get; set; }
		public double ProjectComplexity { get; set; }

		public Project()
		{
			CodeWeighting = new DictionaryProxy<FileTypes, int>();
			Sloc = new DictionaryProxy<FileTypes, List<LocReport>>();

			WorkDayHours = 8;
			WorkDayPerMonth = 20;
			WorkingMonths = 12;

			EffortAdjustmentFactor = 0.6;
			ProjectComplexity = 1.04;
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
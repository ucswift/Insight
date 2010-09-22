using System.Collections.Generic;

namespace WaveTech.Insight.Model
{
	public class LocReport
	{
		public List<SourceLine> Lines { get; set; }

		public LocReport()
		{
			Lines = new List<SourceLine>();
		}
	}
}
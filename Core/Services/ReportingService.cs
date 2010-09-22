using System;
using WaveTech.Insight.Model;
using WaveTech.Insight.Model.Services;

namespace WaveTech.Insight.Services
{
	public class ReportingService : IReportingService
	{
		public Report GenerateReport(Project project)
		{
			Report report = new Report();

			double ksloc = project.GetTotalLineCount(LineTypes.Source, FileTypes.CSharp, false);
			ksloc += project.GetTotalLineCount(LineTypes.SourceAndComment, FileTypes.CSharp, false);

			double adjustedKsloc = ksloc/1000;

			double personMonths = 2.45 * project.EffortAdjustmentFactor * adjustedKsloc;
			personMonths = Math.Pow(personMonths, project.ProjectComplexity);

			report.PersonMonths = personMonths;

			return report;
		}
	}
}
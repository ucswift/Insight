using WaveTech.Insight.Model;

namespace WaveTech.Insight.InsightWpf.Classes
{
	public class UIContext
	{
		#region Private Members
		private static bool _newProject;
		private static Project _project;
		#endregion Private Members

		#region Public Properties
		public static Project Project
		{
			get { return _project; }
			set
			{
				if (value != null || value != _project)
				{
					_project = value;
				}
			}
		}
		#endregion Public Properties

		public static void SetNewProject()
		{
			_project = new Project();
			_newProject = true;
		}
	}
}

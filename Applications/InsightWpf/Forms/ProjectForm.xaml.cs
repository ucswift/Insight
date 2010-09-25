using System.Windows;
using System.Windows.Controls;
using WaveTech.Insight.InsightWpf.Classes;
using WaveTech.Insight.Model;

namespace WaveTech.Insight.InsightWpf.Forms
{
	/// <summary>
	/// Interaction logic for ProjectForm.xaml
	/// </summary>
	public partial class ProjectForm : UserControl
	{
		#region Dependency Properties
		public static readonly DependencyProperty ProjectProperty =
		DependencyProperty.Register("Project", typeof(Project), typeof(ProjectForm),
		new FrameworkPropertyMetadata(
				null,
				FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				ProjectPropertyChanged));

		public Project Project
		{
			get { return (Project)GetValue(ProjectProperty); }
			set { SetValue(ProjectProperty, value); }
		}

		private static void ProjectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
		#endregion Dependency Properties

		public ProjectForm()
		{
			InitializeComponent();
			WindowHelper.CheckAndApplyTheme(this);
		}
	}
}
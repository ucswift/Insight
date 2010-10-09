using System.Linq;
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

		private void ComplexityRadioButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private float ConvertExperiance(string experiance)
		{
			return 0.0f;
		}

		private void chkCSharp_Click(object sender, RoutedEventArgs e)
		{
			if (chkCSharp.IsChecked.Value)
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.CSharp).Count() <= 0)
				{
					UIContext.Project.CodeWeighting.Original.Add(FileTypes.CSharp, ConvertExperiance(exprCSharp.Experience));
				}
			}
			else
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.CSharp).Count() >= 0)
				{
					UIContext.Project.CodeWeighting.Original.Remove(FileTypes.CSharp);
				}
			}
		}

		private void chkXaml_Click(object sender, RoutedEventArgs e)
		{
			if (chkXaml.IsChecked.Value)
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Xaml).Count() <= 0)
				{
					UIContext.Project.CodeWeighting.Original.Add(FileTypes.Xaml, ConvertExperiance(exprXaml.Experience));
				}
			}
			else
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Xaml).Count() >= 0)
				{
					UIContext.Project.CodeWeighting.Original.Remove(FileTypes.Xaml);
				}
			}
		}

		private void chkAspx_Click(object sender, RoutedEventArgs e)
		{
			if (chkAspx.IsChecked.Value)
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Aspx).Count() <= 0)
				{
					UIContext.Project.CodeWeighting.Original.Add(FileTypes.Aspx, ConvertExperiance(exprAspx.Experience));
				}
			}
			else
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Aspx).Count() >= 0)
				{
					UIContext.Project.CodeWeighting.Original.Remove(FileTypes.Aspx);
				}
			}
		}

		private void chkHtml_Click(object sender, RoutedEventArgs e)
		{
			if (chkHtml.IsChecked.Value)
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Html).Count() <= 0)
				{
					UIContext.Project.CodeWeighting.Original.Add(FileTypes.Html, ConvertExperiance(exprHtml.Experience));
				}
			}
			else
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Html).Count() >= 0)
				{
					UIContext.Project.CodeWeighting.Original.Remove(FileTypes.Html);
				}
			}
		}

		private void chkSql_Click(object sender, RoutedEventArgs e)
		{
			if (chkSql.IsChecked.Value)
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Sql).Count() <= 0)
				{
					UIContext.Project.CodeWeighting.Original.Add(FileTypes.Sql, ConvertExperiance(exprSql.Experience));
				}
			}
			else
			{
				if (UIContext.Project.CodeWeighting.Original.Where(x => x.Key == FileTypes.Sql).Count() >= 0)
				{
					UIContext.Project.CodeWeighting.Original.Remove(FileTypes.Sql);
				}
			}
		}
	}
}
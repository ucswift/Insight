using System;
using System.Windows.Media.Imaging;
using Odyssey.Controls;
using Odyssey.Controls.Classes;
using WaveTech.Insight.InsightWpf.Classes;
using WaveTech.Insight.InsightWpf.Forms;

namespace WaveTech.Insight.InsightWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RibbonWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			try
			{
				IconBitmapDecoder ibd = new IconBitmapDecoder(
					new Uri(@"pack://application:,,/Insight.ico", UriKind.RelativeOrAbsolute),
					BitmapCreateOptions.None,
					BitmapCacheOption.Default);
				Icon = ibd.Frames[0];
			}
			catch { }

			SkinManager.SkinId = SkinId.OfficeBlack;


		}

		public void Initalize()
		{
			contentRoot.Content = null;

			if (UIContext.Project == null)
			{
				contentRoot.Content = null;
			}
			else
			{
				ProjectForm projectForm = new ProjectForm();
				projectForm.Project = UIContext.Project;
				contentRoot.Content = projectForm;
			}
		}
	}
}

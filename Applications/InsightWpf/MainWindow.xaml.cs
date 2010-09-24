using Odyssey.Controls;
using Odyssey.Controls.Classes;

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

			SkinManager.SkinId = SkinId.OfficeBlack;
		}
	}
}

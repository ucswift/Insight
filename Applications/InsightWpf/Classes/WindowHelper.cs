
using System.Windows.Controls;
using WPF.Themes;

namespace WaveTech.Insight.InsightWpf.Classes
{
	internal static class WindowHelper
	{
		internal static void CheckAndApplyTheme(ContentControl control)
		{
			ThemeManager.ApplyTheme(control, "BureauBlack");
		}
	}
}
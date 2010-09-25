using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WaveTech.Insight.InsightWpf.Controls
{
	/// <summary>
	/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
	///
	/// Step 1a) Using this custom control in a XAML file that exists in the current project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:WaveTech.Insight.InsightWpf.Controls"
	///
	///
	/// Step 1b) Using this custom control in a XAML file that exists in a different project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:WaveTech.Insight.InsightWpf.Controls;assembly=WaveTech.Insight.InsightWpf.Controls"
	///
	/// You will also need to add a project reference from the project where the XAML file lives
	/// to this project and Rebuild to avoid compilation errors:
	///
	///     Right click on the target project in the Solution Explorer and
	///     "Add Reference"->"Projects"->[Browse to and select this project]
	///
	///
	/// Step 2)
	/// Go ahead and use your control in the XAML file.
	///
	///     <MyNamespace:ExperienceControl/>
	///
	/// </summary>
	public class ExperienceControl : ListBox
	{
		public static readonly RoutedEvent NewExperienceEvent = EventManager.RegisterRoutedEvent
				("Experience", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ExperienceControl));


		public delegate void NewExperienceCustomEventHandler
						(object sender, ExperienceRoutedEventArgs e);

		public static readonly RoutedEvent NewExperienceCustomEvent =
						 EventManager.RegisterRoutedEvent
				("NewExperienceCustom", RoutingStrategy.Bubble,
									 typeof(NewExperienceCustomEventHandler),
				typeof(ExperienceControl));

		public static readonly DependencyProperty ExperienceProperty =
				DependencyProperty.Register("Experience", typeof(string), typeof(ExperienceControl),
				new FrameworkPropertyMetadata(
						null,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
						ExperiencePropertyChanged));

		private State[] _States = { new State("Off", Colors.Gray), new State("Amateur", Colors.Blue), 
																new State("Low", Colors.Green), new State("Medium", Colors.Yellow), 
																new State("High", Colors.IndianRed), new State("Expert", Colors.DarkRed),  };

		static ExperienceControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ExperienceControl),
																							 new FrameworkPropertyMetadata(typeof(ExperienceControl)));
		}


		public ExperienceControl()
		{

			FrameworkElementFactory fGrid = new FrameworkElementFactory(typeof(System.Windows.Controls.Primitives.UniformGrid));
			fGrid.SetValue(System.Windows.Controls.Primitives.UniformGrid.ColumnsProperty, 7);

			ItemsPanel = new ItemsPanelTemplate(fGrid);

			for (int i = 0; i < _States.Length; i++)
			{
				if (i == 1)
				{
					ListBoxItem lbiSeperator = new ListBoxItem();
					lbiSeperator.IsEnabled = false;
					lbiSeperator.Width = 5;

					Rectangle seperator = new Rectangle();
					seperator.Width = 5;
					seperator.Height = 23;
					seperator.Margin = new Thickness(1);
					seperator.Fill = new SolidColorBrush(Colors.Black);

					lbiSeperator.Content = seperator;

					Items.Add(lbiSeperator);
				}

				ListBoxItem lbi = new ListBoxItem();
				lbi.VerticalAlignment = VerticalAlignment.Bottom;
				lbi.Height = 23;

				Rectangle item = new Rectangle();

				item.Width = 10;
				item.Height = 5 * (i / 1.5) + 5;
				item.Margin = new Thickness(1);
				item.Fill = new SolidColorBrush(_States[i].Color);


				ToolTip t = new ToolTip();
				t.Content = _States[i].Name;
				item.ToolTip = t;
				lbi.ToolTip = t;

				lbi.Content = item;
				Items.Add(lbi);
			}


			this.MaxHeight = 30;
			this.MaxWidth = 120;
			this.VerticalAlignment = VerticalAlignment.Bottom;


			SelectedValuePath = "Fill";
		}

		public string Experience
		{
			get { return (string)GetValue(ExperienceProperty); }
			set { SetValue(ExperienceProperty, value); }
		}

		private static void ExperiencePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}

		public event RoutedEventHandler ExperienceEvent
		{
			add { AddHandler(NewExperienceEvent, value); }
			remove { RemoveHandler(NewExperienceEvent, value); }
		}

		private void RaiseNewExperienceEvent()
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(NewExperienceEvent);
			RaiseEvent(newEventArgs);
		}

		private void RaiseNewExperienceCustomEvent()
		{
			ToolTip t = (ToolTip)(SelectedItem as ListBoxItem).ToolTip;
			ExperienceRoutedEventArgs newEventArgs = new ExperienceRoutedEventArgs(t.Content.ToString());

			Experience = t.Content.ToString();

			newEventArgs.RoutedEvent = ExperienceControl.NewExperienceCustomEvent;
			RaiseEvent(newEventArgs);
		}

		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			base.OnSelectionChanged(e);

			RaiseNewExperienceEvent();
			RaiseNewExperienceCustomEvent();
		}
	}

	public class ExperienceRoutedEventArgs : RoutedEventArgs
	{
		#region Instance fields
		private string _name = "";
		#endregion
		#region Constructor

		public ExperienceRoutedEventArgs(string name)
		{
			this._name = name;
		}
		#endregion

		#region Public properties
		public string ExperianceLevel
		{
			get { return _name; }
		}
		#endregion
	}

	internal class State
	{
		internal string Name { get; set; }
		internal Color Color { get; set; }

		public State(string name, Color color)
		{
			Name = name;
			this.Color = color;
		}
	}
}

﻿using System.Windows.Input;

namespace WaveTech.Insight.InsightWpf
{
	public class Commands
	{
		#region Command Routers
		public static readonly RoutedUICommand SaveCommand = new RoutedUICommand("Save", "SaveCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S") }));

		public static readonly RoutedUICommand NewCommand = new RoutedUICommand("New", "NewCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N") }));

		public static readonly RoutedUICommand OpenCommand = new RoutedUICommand("Open", "OpenCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl+O") }));

		public static readonly RoutedUICommand HomeCommand = new RoutedUICommand("Home", "HomeCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+H") }));

		public static readonly RoutedUICommand HelpCommand = new RoutedUICommand("Help", "HelpCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+D") }));

		public static readonly RoutedUICommand AboutCommand = new RoutedUICommand("About", "AboutCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+A") }));
		#endregion Command Routers

		#region Constructor
		static Commands()
		{
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(SaveCommand, SaveProject));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NewCommand, NewProject));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(OpenCommand, OpenProject));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(HomeCommand, Home));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(HelpCommand, Help));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(AboutCommand, About));
		}
		#endregion Constructor

		#region Private Event Handlers
		private static void SaveProject(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void NewProject(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void OpenProject(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void Home(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void About(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void Help(object sender, ExecutedRoutedEventArgs e)
		{

		}
		#endregion Private Event Handlers
	}
}
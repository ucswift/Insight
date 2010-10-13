
using System.ComponentModel;

namespace WaveTech.Insight.Model
{

	/// <summary>
	/// Represents the base type of all business objects.
	/// </summary>
	public class BaseObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (!Notifying)
			{
				try
				{
					IsModified = true;
					if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
				finally
				{
					Notifying = false;
				}
			}
		}


		protected bool Notifying { get; private set; }

		public void RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(propertyName);
		}

		private bool isModified;

		/// <summary>
		/// Gets whether data is modified.
		/// </summary>
		public bool IsModified
		{
			get { return isModified; }
			set
			{
				if (isModified != value)
				{
					isModified = value;
					OnModifiedChanged();
					if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsModified"));
					isModified = value;
				}
			}
		}

		protected virtual void OnModifiedChanged()
		{
		}
	}
}

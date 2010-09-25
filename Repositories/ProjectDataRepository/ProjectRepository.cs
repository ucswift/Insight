using System.IO;
using System.Reflection;
using WaveTech.Insight.Model;
using WaveTech.Insight.Model.Providers;
using WaveTech.Insight.Model.Repositories;

namespace WaveTech.Insight.Repositories.ProjectDataRepository
{
	public class ProjectRepository : IProjectRepository
	{
		#region Private Readonly Members
		private readonly ISerializationProvider _serializationProvider;
		private readonly string _path;
		#endregion Private Readonly Members

		#region Constructor
		public ProjectRepository(ISerializationProvider serializationProvider)
		{
			_serializationProvider = serializationProvider;

			_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			_path = _path.Replace("file:\\", "");
		}
		#endregion Constructor

		public Project GetProject(string filePath)
		{
			if (File.Exists(filePath) == false)
				return null;

			string rawFileData;
			using (StreamReader reader = new StreamReader(filePath))
			{
				rawFileData = reader.ReadToEnd();
			}

			Project project = _serializationProvider.Deserialize<Project>(rawFileData);

			return project;
		}

		public Project SaveProject(Project project, string filePath)
		{
			if (File.Exists(filePath))
				File.Delete(filePath);

			string plainTextObjectData;
			plainTextObjectData = _serializationProvider.Serialize(project);

			using (StreamWriter writer = new StreamWriter(filePath))
			{
				writer.Write(plainTextObjectData);
			}

			return GetProject(filePath);
		}
	}
}

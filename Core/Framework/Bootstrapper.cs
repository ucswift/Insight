using System.IO;
using System.Reflection;
using StructureMap;

namespace WaveTech.Insight.Framework
{
	public static class Bootstrapper
	{
		public static void Configure()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			path = path.Replace("file:\\", "");

			ObjectFactory.Configure(x => x.Scan(
				scan =>
				{
					scan.AssembliesFromPath(path, assembly => assembly.GetName().Name.Contains("WaveTech.Insight"));
					scan.WithDefaultConventions();
					scan.LookForRegistries();
				}
			));
		}
	}
}
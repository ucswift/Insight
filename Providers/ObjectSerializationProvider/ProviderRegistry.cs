using StructureMap.Configuration.DSL;
using WaveTech.Insight.Model.Providers;


namespace WaveTech.Insight.Providers.ObjectSerializationProvider
{
	public class ProviderRegistry : Registry
	{
		public ProviderRegistry()
		{
			For<ISerializationProvider>().Use<SerializationProvider>();
		}
	}
}
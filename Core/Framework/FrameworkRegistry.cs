using StructureMap.Configuration.DSL;

namespace WaveTech.Insight.Framework
{
	public class FrameworkRegistry : Registry
	{
		public FrameworkRegistry()
		{
			For<IEventAggregator>().Singleton().Use<EventAggregator>();
		}
	}
}
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace WaveTech.Insight.Framework
{
	public class FrameworkRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IEventAggregator>().TheDefaultIsConcreteType<EventAggregator>().CacheBy(InstanceScope.Singleton);
		}
	}
}
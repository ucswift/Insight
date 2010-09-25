
namespace WaveTech.Insight.Model.Providers
{
	public interface ISerializationProvider
	{
		string Serialize(object o);
		T Deserialize<T>(string o);
	}
}
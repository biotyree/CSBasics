namespace Clones
{
	public interface ICloneVersionSystem
	{
		string Execute(string query);
	}

	public class Factory<TValue> 
	{
		public static ICloneVersionSystem CreateCVS()
		{
			return new CloneVersionSystem<TValue>();
			//return new checking.CloneVersionSystemSolved();
		}
	}
}
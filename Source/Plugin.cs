using MelonLoader;

namespace ItemCounter
{
	public class Plugin : MelonMod
    {
		private static Plugin instance;

		public override void OnApplicationStart()
		{
			instance = this;
			LoggerInstance.Msg("Initializing this thing");
			base.OnApplicationStart();
		}

		public override void OnApplicationLateStart()
		{
			base.OnApplicationLateStart();
			ItemCounter.Initialize();
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			base.OnSceneWasLoaded(buildIndex, sceneName);
			ItemCounter.Reinitialize();
		}

		public static void Msg(object obj)
		{
			instance.LoggerInstance.Msg(obj);
		}

		public static void Error(object obj)
		{
			instance.LoggerInstance.Error(obj);
		}
	}
}

using NoSurrender.Infra.Scenes;

namespace NoSurrender.App.Scenes
{
	public static class Scene
	{
		public static SceneIdentifier Title { get; } = new("Title", SceneLoadMethod.BuiltIn);
		
	}
}
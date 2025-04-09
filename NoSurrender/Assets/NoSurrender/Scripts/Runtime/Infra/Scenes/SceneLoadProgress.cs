using NoSurrender.Common.Exceptions;

namespace NoSurrender.Infra.Scenes
{
	public readonly struct SceneLoadProgress
	{
		public float Value { get; }

		public SceneLoadProgress(float value)
		{
			Throw.IfArgumentOutOfRange(value, 0f, 1f);
			
			Value = value;
		}
	}
}
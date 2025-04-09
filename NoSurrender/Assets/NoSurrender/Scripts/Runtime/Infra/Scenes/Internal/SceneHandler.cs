using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace NoSurrender.Infra.Scenes
{
	internal abstract class SceneHandler
	{
		public SceneIdentifier SceneIdentifier { get; }

		protected SceneHandler(SceneIdentifier sceneIdentifier)
		{
			SceneIdentifier = sceneIdentifier;
		}

		public abstract UniTask LoadSceneAsync(IProgress<float>? progress, CancellationToken cancellationToken);
		public abstract UniTask UnloadSceneAsync(IProgress<float>? progress, CancellationToken cancellationToken);
	}
}
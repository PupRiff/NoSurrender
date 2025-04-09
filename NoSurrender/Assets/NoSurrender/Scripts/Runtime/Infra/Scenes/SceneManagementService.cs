using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace NoSurrender.Infra.Scenes
{
	public class SceneManagementService
	{
		private readonly LinkedList<SceneHandler> _sceneHandlers = new();
		
		public async UniTask PushSceneAsync(SceneIdentifier sceneIdentifier, IProgress<float>? progress = null, CancellationToken linkedToken = default)
		{
			if (IsSceneLoaded(sceneIdentifier))
			{
				throw new InvalidOperationException($"Scene '{sceneIdentifier}' is already loaded.");
			}

			var sceneHandler = CreateSceneHandler(sceneIdentifier);
			await sceneHandler.LoadSceneAsync(progress, linkedToken);
			_sceneHandlers.AddLast(sceneHandler);
		}
		
		public async UniTask PopSceneAsync(IProgress<float>? progress = null, CancellationToken linkedToken = default)
		{
			var sceneHandler = _sceneHandlers.Last();
			await sceneHandler.UnloadSceneAsync(progress, linkedToken);
			_sceneHandlers.Remove(sceneHandler);
		}
		
		public async UniTask ClearHistoryAsync(IProgress<float>? progress = null, CancellationToken linkedToken = default)
		{
			while (_sceneHandlers.Count > 0)
			{
				await PopSceneAsync(progress, linkedToken);
			}
		}
		
		public bool IsSceneLoaded(SceneIdentifier sceneIdentifier)
		{
			return _sceneHandlers.Any(handler => handler.SceneIdentifier == sceneIdentifier);
		}

		private static SceneHandler CreateSceneHandler(SceneIdentifier sceneIdentifier)
		{
			return sceneIdentifier.LoadMethod switch
			{
				_ => throw new NotImplementedException($"SceneLoadType '{sceneIdentifier.LoadMethod}' is not implemented.")
			};
		}
	}
}
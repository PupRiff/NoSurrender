using UnityEditor;
using UnityEngine;

namespace NoSurrender.Infra.Scenes
{
	internal sealed class SceneManagementSettings : ScriptableSingleton<SceneManagementSettings>
	{
		[SerializeField]
		private string _systemSceneName = "SystemScene";
		
		public string SystemSceneName => _systemSceneName;
	}
}
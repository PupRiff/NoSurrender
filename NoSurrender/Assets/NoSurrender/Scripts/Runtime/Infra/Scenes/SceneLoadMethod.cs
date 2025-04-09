namespace NoSurrender.Infra.Scenes
{
	/// <summary>
	/// シーンの読み込み方式を表す列挙型
	/// </summary>
	public enum SceneLoadMethod
	{
		/// <summary>
		/// ビルトインシーン
		/// </summary>
		BuiltIn,
    
		/// <summary>
		/// Addressables経由で読み込むシーン
		/// </summary>
		Addressables
	}
}
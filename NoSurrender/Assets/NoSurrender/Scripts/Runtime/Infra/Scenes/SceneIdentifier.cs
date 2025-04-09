using System;
using NoSurrender.Common.Exceptions;

namespace NoSurrender.Infra.Scenes
{
	public readonly struct SceneIdentifier : IEquatable<SceneIdentifier>
	{
		/// <summary>
		/// シーン名
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// シーンの読み込み方式
		/// </summary>
		public SceneLoadMethod LoadMethod { get; }

		public SceneIdentifier(string name, SceneLoadMethod loadMethod)
		{
			Throw.IfArgumentNullOrEmpty(name);
			Throw.IfArgumentOutOfRange(loadMethod);
			
			Name = name;
			LoadMethod = loadMethod;
		}

		public bool Equals(SceneIdentifier other)
		{
			return Name == other.Name && LoadMethod == other.LoadMethod;
		}

		public override bool Equals(object? obj)
		{
			return obj is SceneIdentifier other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, (int)LoadMethod);
		}
		
		public static bool operator ==(SceneIdentifier left, SceneIdentifier right) => left.Equals(right);
		public static bool operator !=(SceneIdentifier left, SceneIdentifier right) => !(left == right);
	}
}
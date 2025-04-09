using System.ComponentModel;
using System.Threading;
using NoSurrender.Common.Exceptions;
using UnityEngine;

namespace NoSurrender.Common.Threading.Extensions
{
	public static class MonoBehaviourExtensions
	{
		public static CancellationTokenSource CreateLinkedCancellationTokenSource(this MonoBehaviour monoBehaviour, CancellationToken cancellationToken)
		{
			Throw.IfArgumentNull(monoBehaviour);
			return CancellationTokenSource.CreateLinkedTokenSource(monoBehaviour.destroyCancellationToken, cancellationToken);
		}
	}
}
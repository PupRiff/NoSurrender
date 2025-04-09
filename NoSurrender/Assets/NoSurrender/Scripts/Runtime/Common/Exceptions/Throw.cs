using System;
using System.Runtime.CompilerServices;
using FastEnumUtility;


namespace NoSurrender.Common.Exceptions
{
	public static class Throw
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IfArgumentNull(object? value, [CallerArgumentExpression("value")] string? paramName = null)
		{
			if (value is null)
			{
				throw new ArgumentNullException(paramName);
			}
			
			if (value == null)
			{
				throw new InvalidOperationException($"'{paramName}' is destroyed.");
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IfArgumentNullOrEmpty(string? value, [CallerArgumentExpression("value")] string? paramName = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(paramName);
			}
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IfArgumentOutOfRange<TValue>(TValue value, TValue min, TValue max, [CallerArgumentExpression("value")] string? paramName = null)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
			{
				throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}.");
			}
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IfArgumentOutOfRange<TEnum>(TEnum value, [CallerArgumentExpression("value")] string? paramName = null)
			where TEnum : struct, Enum
		{
			if (!FastEnum.IsDefined(value))
			{
				throw new ArgumentOutOfRangeException(paramName, $"Value '{FastEnum.GetName(value)}' is not defined in enum '{typeof(TEnum).Name}'.");
			}
		}
	}
}
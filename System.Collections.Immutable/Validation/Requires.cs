using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validation
{
	// Token: 0x02000003 RID: 3
	internal static class Requires
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[DebuggerStepThrough]
		public static void NotNull<T>([ValidatedNotNull] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		[DebuggerStepThrough]
		public static T NotNullPassthrough<T>([ValidatedNotNull] T value, string parameterName) where T : class
		{
			Requires.NotNull<T>(value, parameterName);
			return value;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		[DebuggerStepThrough]
		public static void NotNullAllowStructs<T>([ValidatedNotNull] T value, string parameterName)
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207A File Offset: 0x0000027A
		[DebuggerStepThrough]
		private static void FailArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002082 File Offset: 0x00000282
		[DebuggerStepThrough]
		public static void Range(bool condition, string parameterName, string message = null)
		{
			if (!condition)
			{
				Requires.FailRange(parameterName, message);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000208E File Offset: 0x0000028E
		[DebuggerStepThrough]
		public static void FailRange(string parameterName, string message = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A6 File Offset: 0x000002A6
		[DebuggerStepThrough]
		public static void Argument(bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B3 File Offset: 0x000002B3
		[DebuggerStepThrough]
		public static void Argument(bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020BE File Offset: 0x000002BE
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void FailObjectDisposed<TDisposed>(TDisposed disposed)
		{
			throw new ObjectDisposedException(disposed.GetType().FullName);
		}
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D1 RID: 2257
	internal static class KdfWorkLimiter
	{
		// Token: 0x0600525C RID: 21084 RVA: 0x00127794 File Offset: 0x00126794
		internal static void SetIterationLimit(ulong workLimit)
		{
			KdfWorkLimiter.t_State = new KdfWorkLimiter.State
			{
				RemainingAllowedWork = workLimit
			};
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x001277B4 File Offset: 0x001267B4
		internal static bool WasWorkLimitExceeded()
		{
			return KdfWorkLimiter.t_State.WorkLimitWasExceeded;
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x001277C0 File Offset: 0x001267C0
		internal static void ResetIterationLimit()
		{
			KdfWorkLimiter.t_State = null;
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x001277C8 File Offset: 0x001267C8
		internal static void RecordIterations(int workCount)
		{
			KdfWorkLimiter.RecordIterations((long)workCount);
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x001277D4 File Offset: 0x001267D4
		internal static void RecordIterations(long workCount)
		{
			KdfWorkLimiter.State state = KdfWorkLimiter.t_State;
			bool flag = false;
			checked
			{
				try
				{
					if (!state.WorkLimitWasExceeded)
					{
						state.RemainingAllowedWork -= (ulong)workCount;
						flag = true;
					}
				}
				finally
				{
					if (!flag)
					{
						state.RemainingAllowedWork = 0UL;
						state.WorkLimitWasExceeded = true;
						throw new CryptographicException();
					}
				}
			}
		}

		// Token: 0x04002A5B RID: 10843
		[ThreadStatic]
		private static KdfWorkLimiter.State t_State;

		// Token: 0x020008D2 RID: 2258
		private sealed class State
		{
			// Token: 0x04002A5C RID: 10844
			internal ulong RemainingAllowedWork;

			// Token: 0x04002A5D RID: 10845
			internal bool WorkLimitWasExceeded;
		}
	}
}

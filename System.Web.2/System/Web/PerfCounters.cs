using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000E5 RID: 229
	internal sealed class PerfCounters
	{
		// Token: 0x06000E41 RID: 3649 RVA: 0x000030B5 File Offset: 0x000012B5
		private PerfCounters()
		{
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000287FD File Offset: 0x000269FD
		internal static void Open(string appName)
		{
			PerfCounters.OpenCounter(appName);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00028805 File Offset: 0x00026A05
		internal static void OpenStateCounters()
		{
			PerfCounters.OpenCounter(null);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00028810 File Offset: 0x00026A10
		private static void OpenCounter(string appName)
		{
			try
			{
				if (HttpRuntime.IsEngineLoaded)
				{
					if (PerfCounters._global == IntPtr.Zero)
					{
						PerfCounters._global = UnsafeNativeMethods.PerfOpenGlobalCounters();
					}
					if (appName == null)
					{
						if (PerfCounters._stateService == IntPtr.Zero)
						{
							PerfCounters._stateService = UnsafeNativeMethods.PerfOpenStateCounters();
						}
					}
					else if (appName != null)
					{
						PerfCounters._instance = UnsafeNativeMethods.PerfOpenAppCounters(appName);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00028884 File Offset: 0x00026A84
		internal static void IncrementCounter(AppPerfCounter counter)
		{
			if (PerfCounters._instance != null)
			{
				UnsafeNativeMethods.PerfIncrementCounter(PerfCounters._instance.UnsafeHandle, (int)counter);
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002889D File Offset: 0x00026A9D
		internal static void DecrementCounter(AppPerfCounter counter)
		{
			if (PerfCounters._instance != null)
			{
				UnsafeNativeMethods.PerfDecrementCounter(PerfCounters._instance.UnsafeHandle, (int)counter);
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000288B6 File Offset: 0x00026AB6
		internal static void IncrementCounterEx(AppPerfCounter counter, int delta)
		{
			if (PerfCounters._instance != null)
			{
				UnsafeNativeMethods.PerfIncrementCounterEx(PerfCounters._instance.UnsafeHandle, (int)counter, delta);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000288D0 File Offset: 0x00026AD0
		internal static void SetCounter(AppPerfCounter counter, int value)
		{
			if (PerfCounters._instance != null)
			{
				UnsafeNativeMethods.PerfSetCounter(PerfCounters._instance.UnsafeHandle, (int)counter, value);
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000288EA File Offset: 0x00026AEA
		internal static int GetGlobalCounter(GlobalPerfCounter counter)
		{
			if (PerfCounters._global != IntPtr.Zero)
			{
				return UnsafeNativeMethods.PerfGetCounter(PerfCounters._global, (int)counter);
			}
			return -1;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002890A File Offset: 0x00026B0A
		internal static void IncrementGlobalCounter(GlobalPerfCounter counter)
		{
			if (PerfCounters._global != IntPtr.Zero)
			{
				UnsafeNativeMethods.PerfIncrementCounter(PerfCounters._global, (int)counter);
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00028928 File Offset: 0x00026B28
		internal static void DecrementGlobalCounter(GlobalPerfCounter counter)
		{
			if (PerfCounters._global != IntPtr.Zero)
			{
				UnsafeNativeMethods.PerfDecrementCounter(PerfCounters._global, (int)counter);
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00028946 File Offset: 0x00026B46
		internal static void SetGlobalCounter(GlobalPerfCounter counter, int value)
		{
			if (PerfCounters._global != IntPtr.Zero)
			{
				UnsafeNativeMethods.PerfSetCounter(PerfCounters._global, (int)counter, value);
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00028968 File Offset: 0x00026B68
		internal static void IncrementStateServiceCounter(StateServicePerfCounter counter)
		{
			if (PerfCounters._stateService == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.PerfIncrementCounter(PerfCounters._stateService, (int)counter);
			switch (counter)
			{
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_ACTIVE:
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_ACTIVE);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_ABANDONED:
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_ABANDONED);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_TIMED_OUT:
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_TIMED_OUT);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_TOTAL:
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_TOTAL);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000289CC File Offset: 0x00026BCC
		internal static void DecrementStateServiceCounter(StateServicePerfCounter counter)
		{
			if (PerfCounters._stateService == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.PerfDecrementCounter(PerfCounters._stateService, (int)counter);
			switch (counter)
			{
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_ACTIVE:
				PerfCounters.DecrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_ACTIVE);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_ABANDONED:
				PerfCounters.DecrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_ABANDONED);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_TIMED_OUT:
				PerfCounters.DecrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_TIMED_OUT);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_TOTAL:
				PerfCounters.DecrementGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_TOTAL);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00028A30 File Offset: 0x00026C30
		internal static void SetStateServiceCounter(StateServicePerfCounter counter, int value)
		{
			if (PerfCounters._stateService == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.PerfSetCounter(PerfCounters._stateService, (int)counter, value);
			switch (counter)
			{
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_ACTIVE:
				PerfCounters.SetGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_ACTIVE, value);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_ABANDONED:
				PerfCounters.SetGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_ABANDONED, value);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_TIMED_OUT:
				PerfCounters.SetGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_TIMED_OUT, value);
				return;
			case StateServicePerfCounter.STATE_SERVICE_SESSIONS_TOTAL:
				PerfCounters.SetGlobalCounter(GlobalPerfCounter.STATE_SERVER_SESSIONS_TOTAL, value);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000558 RID: 1368
		internal static readonly IPerfCounters Instance = new PerfCounters.PerfCountersInstance();

		// Token: 0x04000559 RID: 1369
		private static PerfInstanceDataHandle _instance = null;

		// Token: 0x0400055A RID: 1370
		private static IntPtr _global = IntPtr.Zero;

		// Token: 0x0400055B RID: 1371
		private static IntPtr _stateService = IntPtr.Zero;

		// Token: 0x020008E9 RID: 2281
		private sealed class PerfCountersInstance : IPerfCounters
		{
			// Token: 0x0600685D RID: 26717 RVA: 0x00173E54 File Offset: 0x00172054
			public void IncrementCounter(AppPerfCounter counter)
			{
				PerfCounters.IncrementCounter(counter);
			}

			// Token: 0x0600685E RID: 26718 RVA: 0x00173E5C File Offset: 0x0017205C
			public void IncrementCounter(AppPerfCounter counter, int value)
			{
				PerfCounters.IncrementCounterEx(counter, value);
			}

			// Token: 0x0600685F RID: 26719 RVA: 0x00173E65 File Offset: 0x00172065
			public void DecrementCounter(AppPerfCounter counter)
			{
				PerfCounters.DecrementCounter(counter);
			}

			// Token: 0x06006860 RID: 26720 RVA: 0x00173E6D File Offset: 0x0017206D
			public void SetCounter(AppPerfCounter counter, int value)
			{
				PerfCounters.SetCounter(counter, value);
			}
		}
	}
}

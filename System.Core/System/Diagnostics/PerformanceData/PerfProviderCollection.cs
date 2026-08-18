using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Diagnostics.PerformanceData
{
	// Token: 0x020002A6 RID: 678
	internal static class PerfProviderCollection
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000596A8 File Offset: 0x000578A8
		private static object s_lockObject
		{
			get
			{
				if (PerfProviderCollection.s_hiddenInternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref PerfProviderCollection.s_hiddenInternalSyncObject, value, null);
				}
				return PerfProviderCollection.s_hiddenInternalSyncObject;
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000596D4 File Offset: 0x000578D4
		[SecurityCritical]
		internal static PerfProvider QueryProvider(Guid providerGuid)
		{
			object s_lockObject = PerfProviderCollection.s_lockObject;
			PerfProvider result;
			lock (s_lockObject)
			{
				foreach (PerfProvider perfProvider in PerfProviderCollection.s_providerList)
				{
					if (perfProvider.m_providerGuid == providerGuid)
					{
						return perfProvider;
					}
				}
				PerfProvider perfProvider2 = new PerfProvider(providerGuid);
				PerfProviderCollection.s_providerList.Add(perfProvider2);
				result = perfProvider2;
			}
			return result;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00059774 File Offset: 0x00057974
		[SecurityCritical]
		internal static void RemoveProvider(Guid providerGuid)
		{
			object s_lockObject = PerfProviderCollection.s_lockObject;
			lock (s_lockObject)
			{
				PerfProvider perfProvider = null;
				foreach (PerfProvider perfProvider2 in PerfProviderCollection.s_providerList)
				{
					if (perfProvider2.m_providerGuid == providerGuid)
					{
						perfProvider = perfProvider2;
					}
				}
				if (perfProvider != null)
				{
					perfProvider.m_hProvider.Dispose();
					PerfProviderCollection.s_providerList.Remove(perfProvider);
				}
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00059818 File Offset: 0x00057A18
		internal static void RegisterCounterSet(Guid counterSetGuid)
		{
			object s_lockObject = PerfProviderCollection.s_lockObject;
			lock (s_lockObject)
			{
				if (PerfProviderCollection.s_counterSetList.ContainsKey(counterSetGuid))
				{
					throw new ArgumentException(SR.GetString("Perflib_Argument_CounterSetAlreadyRegister", new object[]
					{
						counterSetGuid
					}), "CounterSetGuid");
				}
				PerfProviderCollection.s_counterSetList.Add(counterSetGuid, 0);
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00059898 File Offset: 0x00057A98
		internal static void UnregisterCounterSet(Guid counterSetGuid)
		{
			object s_lockObject = PerfProviderCollection.s_lockObject;
			lock (s_lockObject)
			{
				PerfProviderCollection.s_counterSetList.Remove(counterSetGuid);
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000598E4 File Offset: 0x00057AE4
		internal static bool ValidateCounterType(CounterType inCounterType)
		{
			foreach (CounterType counterType in PerfProviderCollection.s_counterTypes)
			{
				if (counterType == inCounterType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00059910 File Offset: 0x00057B10
		internal static bool ValidateCounterSetInstanceType(CounterSetInstanceType inCounterSetInstanceType)
		{
			foreach (CounterSetInstanceType counterSetInstanceType in PerfProviderCollection.s_counterSetInstanceTypes)
			{
				if (counterSetInstanceType == inCounterSetInstanceType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000BF0 RID: 3056
		private static object s_hiddenInternalSyncObject;

		// Token: 0x04000BF1 RID: 3057
		private static List<PerfProvider> s_providerList = new List<PerfProvider>();

		// Token: 0x04000BF2 RID: 3058
		private static Dictionary<object, int> s_counterSetList = new Dictionary<object, int>();

		// Token: 0x04000BF3 RID: 3059
		private static CounterType[] s_counterTypes = (CounterType[])Enum.GetValues(typeof(CounterType));

		// Token: 0x04000BF4 RID: 3060
		private static CounterSetInstanceType[] s_counterSetInstanceTypes = (CounterSetInstanceType[])Enum.GetValues(typeof(CounterSetInstanceType));
	}
}

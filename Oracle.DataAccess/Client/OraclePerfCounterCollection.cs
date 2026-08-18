using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000053 RID: 83
	internal sealed class OraclePerfCounterCollection
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0002A518 File Offset: 0x00029518
		private static void CleanUp()
		{
			OraclePerfCounterCollection.HardConnectsPerSecond.Dispose();
			OraclePerfCounterCollection.HardDisconnectsPerSecond.Dispose();
			OraclePerfCounterCollection.SoftConnectsPerSecond.Dispose();
			OraclePerfCounterCollection.SoftDisconnectsPerSecond.Dispose();
			OraclePerfCounterCollection.NumberOfActiveConnectionPools.Dispose();
			OraclePerfCounterCollection.NumberOfActiveConnections.Dispose();
			OraclePerfCounterCollection.NumberOfFreeConnections.Dispose();
			OraclePerfCounterCollection.NumberOfInactiveConnectionPools.Dispose();
			OraclePerfCounterCollection.NumberOfNonPooledConnections.Dispose();
			OraclePerfCounterCollection.NumberOfPooledConnections.Dispose();
			OraclePerfCounterCollection.NumberOfReclaimedConnections.Dispose();
			OraclePerfCounterCollection.NumberOfStasisConnections.Dispose();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0002A5A0 File Offset: 0x000295A0
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		static OraclePerfCounterCollection()
		{
			string text = string.Empty;
			try
			{
				if (AppDomain.CurrentDomain.IsDefaultAppDomain())
				{
					AppDomain.CurrentDomain.ProcessExit += OraclePerfCounterCollection.DomainUnloadOrProcessExit;
				}
				else
				{
					AppDomain.CurrentDomain.DomainUnload += OraclePerfCounterCollection.DomainUnloadOrProcessExit;
				}
				text = string.Concat(new object[]
				{
					AppDomain.CurrentDomain.FriendlyName,
					" [",
					Process.GetCurrentProcess().Id,
					", ",
					AppDomain.CurrentDomain.Id,
					"]"
				});
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) OraclePerfCounterCollection::OraclePerfCounterCollection() - " + ex.Message + "\n"
					});
				}
			}
			text = text.Replace('/', '_');
			try
			{
				OraclePerfCounterCollection.m_categoryHelp = OpoErrResManager.GetErrorMesg(-2801, new string[0]);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) OraclePerfCounterCollection::OraclePerfCounterCollection() categoryHelp - " + ex2.Message + "\n"
					});
				}
			}
			OraclePerfCounterCollection.HardConnectsPerSecond = new OraclePerfCounter("Oracle Data Provider for .NET", "HardConnectsPerSecond", OpoErrResManager.GetErrorMesg(-2802, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64, text);
			OraclePerfCounterCollection.HardDisconnectsPerSecond = new OraclePerfCounter("Oracle Data Provider for .NET", "HardDisconnectsPerSecond", OpoErrResManager.GetErrorMesg(-2803, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64, text);
			OraclePerfCounterCollection.SoftConnectsPerSecond = new OraclePerfCounter("Oracle Data Provider for .NET", "SoftConnectsPerSecond", OpoErrResManager.GetErrorMesg(-2804, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64, text);
			OraclePerfCounterCollection.SoftDisconnectsPerSecond = new OraclePerfCounter("Oracle Data Provider for .NET", "SoftDisconnectsPerSecond", OpoErrResManager.GetErrorMesg(-2805, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64, text);
			OraclePerfCounterCollection.NumberOfActiveConnectionPools = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfActiveConnectionPools", OpoErrResManager.GetErrorMesg(-2806, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfActiveConnections = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfActiveConnections", OpoErrResManager.GetErrorMesg(-2807, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfFreeConnections = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfFreeConnections", OpoErrResManager.GetErrorMesg(-2808, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfInactiveConnectionPools = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfInactiveConnectionPools", OpoErrResManager.GetErrorMesg(-2809, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfNonPooledConnections = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfNonPooledConnections", OpoErrResManager.GetErrorMesg(-2810, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfPooledConnections = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfPooledConnections", OpoErrResManager.GetErrorMesg(-2811, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfReclaimedConnections = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfReclaimedConnections", OpoErrResManager.GetErrorMesg(-2812, new string[0]), PerformanceCounterType.NumberOfItems64, text);
			OraclePerfCounterCollection.NumberOfStasisConnections = new OraclePerfCounter("Oracle Data Provider for .NET", "NumberOfStasisConnections", OpoErrResManager.GetErrorMesg(-2813, new string[0]), PerformanceCounterType.NumberOfItems64, text);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0002A8E4 File Offset: 0x000298E4
		private static void DomainUnloadOrProcessExit(object sender, EventArgs e)
		{
			OraclePerfCounterCollection.CleanUp();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0002A8EC File Offset: 0x000298EC
		private static bool CreateCounters(string[] args)
		{
			OraclePerfCounterCollection.DeleteCounters(args);
			bool result;
			try
			{
				CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
				if (OraclePerfCounterCollection.HardConnectsPerSecond.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.HardConnectsPerSecond.CreationData);
				}
				if (OraclePerfCounterCollection.HardDisconnectsPerSecond.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.HardDisconnectsPerSecond.CreationData);
				}
				if (OraclePerfCounterCollection.SoftConnectsPerSecond.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.SoftConnectsPerSecond.CreationData);
				}
				if (OraclePerfCounterCollection.SoftDisconnectsPerSecond.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.SoftDisconnectsPerSecond.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfActiveConnectionPools.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfActiveConnectionPools.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfActiveConnections.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfActiveConnections.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfFreeConnections.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfFreeConnections.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfInactiveConnectionPools.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfInactiveConnectionPools.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfNonPooledConnections.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfNonPooledConnections.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfPooledConnections.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfPooledConnections.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfReclaimedConnections.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfReclaimedConnections.CreationData);
				}
				if (OraclePerfCounterCollection.NumberOfStasisConnections.CreationData != null)
				{
					counterCreationDataCollection.Add(OraclePerfCounterCollection.NumberOfStasisConnections.CreationData);
				}
				PerformanceCounterCategory.Create("Oracle Data Provider for .NET", OraclePerfCounterCollection.m_categoryHelp, PerformanceCounterCategoryType.MultiInstance, counterCreationDataCollection);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0002AA9C File Offset: 0x00029A9C
		private static bool DeleteCounters(string[] args)
		{
			bool result;
			try
			{
				PerformanceCounterCategory.Delete("Oracle Data Provider for .NET");
				result = true;
			}
			catch (InvalidOperationException)
			{
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0400027C RID: 636
		private const string m_categoryName = "Oracle Data Provider for .NET";

		// Token: 0x0400027D RID: 637
		private static string m_categoryHelp;

		// Token: 0x0400027E RID: 638
		internal static readonly OraclePerfCounter HardConnectsPerSecond;

		// Token: 0x0400027F RID: 639
		internal static readonly OraclePerfCounter HardDisconnectsPerSecond;

		// Token: 0x04000280 RID: 640
		internal static readonly OraclePerfCounter SoftConnectsPerSecond;

		// Token: 0x04000281 RID: 641
		internal static readonly OraclePerfCounter SoftDisconnectsPerSecond;

		// Token: 0x04000282 RID: 642
		internal static readonly OraclePerfCounter NumberOfActiveConnectionPools;

		// Token: 0x04000283 RID: 643
		internal static readonly OraclePerfCounter NumberOfActiveConnections;

		// Token: 0x04000284 RID: 644
		internal static readonly OraclePerfCounter NumberOfFreeConnections;

		// Token: 0x04000285 RID: 645
		internal static readonly OraclePerfCounter NumberOfInactiveConnectionPools;

		// Token: 0x04000286 RID: 646
		internal static readonly OraclePerfCounter NumberOfNonPooledConnections;

		// Token: 0x04000287 RID: 647
		internal static readonly OraclePerfCounter NumberOfPooledConnections;

		// Token: 0x04000288 RID: 648
		internal static readonly OraclePerfCounter NumberOfReclaimedConnections;

		// Token: 0x04000289 RID: 649
		internal static readonly OraclePerfCounter NumberOfStasisConnections;
	}
}

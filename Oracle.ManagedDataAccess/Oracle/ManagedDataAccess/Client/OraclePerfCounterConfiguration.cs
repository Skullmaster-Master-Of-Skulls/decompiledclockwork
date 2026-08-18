using System;
using System.Diagnostics;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000075 RID: 117
	internal class OraclePerfCounterConfiguration
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x00038908 File Offset: 0x00036B08
		private static bool CreateCounters(string[] args)
		{
			OraclePerfCounterConfiguration.DeleteCounters(args);
			try
			{
				if (!PerformanceCounterCategory.Exists("ODP.NET, Managed Driver"))
				{
					CounterCreationDataCollection counterData = new CounterCreationDataCollection();
					OraclePerfCounterConfiguration.CreateCounterDataList(ref counterData);
					string categoryHelp;
					try
					{
						categoryHelp = OracleStringResourceManager.GetErrorMesg(-2801, new string[0]);
					}
					catch (Exception)
					{
						categoryHelp = string.Empty;
					}
					PerformanceCounterCategory.Create("ODP.NET, Managed Driver", categoryHelp, PerformanceCounterCategoryType.MultiInstance, counterData);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00038988 File Offset: 0x00036B88
		private static bool DeleteCounters(string[] args)
		{
			try
			{
				if (PerformanceCounterCategory.Exists("ODP.NET, Managed Driver"))
				{
					PerformanceCounterCategory.Delete("ODP.NET, Managed Driver");
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000389C8 File Offset: 0x00036BC8
		private static void CreateCounterDataList(ref CounterCreationDataCollection list)
		{
			list.Clear();
			list.Add(new CounterCreationData("HardConnectsPerSecond", OracleStringResourceManager.GetErrorMesg(-2802, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64));
			list.Add(new CounterCreationData("HardDisconnectsPerSecond", OracleStringResourceManager.GetErrorMesg(-2803, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64));
			list.Add(new CounterCreationData("SoftConnectsPerSecond", OracleStringResourceManager.GetErrorMesg(-2804, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64));
			list.Add(new CounterCreationData("SoftDisconnectsPerSecond", OracleStringResourceManager.GetErrorMesg(-2805, new string[0]), PerformanceCounterType.RateOfCountsPerSecond64));
			list.Add(new CounterCreationData("NumberOfActiveConnectionPools", OracleStringResourceManager.GetErrorMesg(-2806, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfActiveConnections", OracleStringResourceManager.GetErrorMesg(-2807, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfFreeConnections", OracleStringResourceManager.GetErrorMesg(-2808, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfInactiveConnectionPools", OracleStringResourceManager.GetErrorMesg(-2809, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfNonPooledConnections", OracleStringResourceManager.GetErrorMesg(-2810, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfPooledConnections", OracleStringResourceManager.GetErrorMesg(-2811, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfReclaimedConnections", OracleStringResourceManager.GetErrorMesg(-2812, new string[0]), PerformanceCounterType.NumberOfItems64));
			list.Add(new CounterCreationData("NumberOfStasisConnections", OracleStringResourceManager.GetErrorMesg(-2813, new string[0]), PerformanceCounterType.NumberOfItems64));
		}
	}
}

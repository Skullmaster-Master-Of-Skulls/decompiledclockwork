using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000074 RID: 116
	internal static class OraclePerfCounterCollection
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x0003830C File Offset: 0x0003650C
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		static OraclePerfCounterCollection()
		{
			try
			{
				OraclePerfCounterCollection.HookToDomainUnloadOrProcessExitEvent();
				OraclePerfCounterCollection.InitializePerformanceCounters();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0003835C File Offset: 0x0003655C
		public static void Initialize()
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00038360 File Offset: 0x00036560
		internal static OraclePerfCounter Increment(OraclePerfParams.CounterIndex iCounter, string poolName, string dbInstanceName)
		{
			OraclePerfCounter oraclePerfCounter = null;
			if (OraclePerfCounterCollection.m_countersList[(int)iCounter] != null)
			{
				string text = OraclePerfCounterCollection.CreateInstanceName(OraclePerfParams.m_appDomainPfcInstanceName, poolName, dbInstanceName);
				if (!OraclePerfCounterCollection.m_countersList[(int)iCounter].TryGetValue(text, out oraclePerfCounter))
				{
					oraclePerfCounter = OraclePerfCounterCollection.CreatePerformanceCounters(iCounter, text);
				}
				oraclePerfCounter.Increment();
			}
			return oraclePerfCounter;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000383A8 File Offset: 0x000365A8
		private static string CreateInstanceName(string appDomain, string poolName, string dbInstanceName)
		{
			poolName = OraclePerfCounterCollection.ConstructFieldName(poolName, 70);
			dbInstanceName = OraclePerfCounterCollection.ConstructFieldName(dbInstanceName, 16);
			return new StringBuilder().Append(appDomain).Append(poolName).Append(dbInstanceName).ToString();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000383E8 File Offset: 0x000365E8
		private static string ConstructFieldName(string fieldName, int lengthLimit)
		{
			if (fieldName != null)
			{
				if (fieldName.Length > lengthLimit)
				{
					fieldName = fieldName.Substring(0, lengthLimit - 3) + "...";
				}
				fieldName = new StringBuilder().Append("[").Append(fieldName).Append("]").ToString();
			}
			return fieldName;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00038440 File Offset: 0x00036640
		internal static OraclePerfCounter IncrementBy(OraclePerfParams.CounterIndex iCounter, int value, string poolName, string dbInstanceName)
		{
			OraclePerfCounter oraclePerfCounter = null;
			if (OraclePerfCounterCollection.m_countersList[(int)iCounter] != null)
			{
				string text = OraclePerfCounterCollection.CreateInstanceName(OraclePerfParams.m_appDomainPfcInstanceName, poolName, dbInstanceName);
				if (!OraclePerfCounterCollection.m_countersList[(int)iCounter].TryGetValue(text, out oraclePerfCounter))
				{
					oraclePerfCounter = OraclePerfCounterCollection.CreatePerformanceCounters(iCounter, text);
				}
				oraclePerfCounter.IncrementBy(value);
			}
			return oraclePerfCounter;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00038488 File Offset: 0x00036688
		internal static OraclePerfCounter Decrement(OraclePerfParams.CounterIndex iCounter, string poolName, string dbInstanceName)
		{
			OraclePerfCounter oraclePerfCounter = null;
			if (OraclePerfCounterCollection.m_countersList[(int)iCounter] != null)
			{
				string text = OraclePerfCounterCollection.CreateInstanceName(OraclePerfParams.m_appDomainPfcInstanceName, poolName, dbInstanceName);
				if (!OraclePerfCounterCollection.m_countersList[(int)iCounter].TryGetValue(text, out oraclePerfCounter))
				{
					oraclePerfCounter = OraclePerfCounterCollection.CreatePerformanceCounters(iCounter, text);
				}
				if (oraclePerfCounter != null)
				{
					oraclePerfCounter.Decrement();
				}
			}
			return oraclePerfCounter;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000384D4 File Offset: 0x000366D4
		private static OraclePerfCounter CreatePerformanceCounters(OraclePerfParams.CounterIndex iCount, string pfcInstanceName)
		{
			Dictionary<string, OraclePerfCounter> dictionary = OraclePerfCounterCollection.m_countersList[(int)iCount];
			string[] array = new string[3];
			int num = pfcInstanceName.IndexOf(']');
			if (num != -1)
			{
				array[0] = pfcInstanceName.Substring(0, num + 1);
				num = pfcInstanceName.IndexOf(']', num + 1);
				if (num != -1)
				{
					array[1] = pfcInstanceName.Substring(0, num + 1);
				}
			}
			array[2] = pfcInstanceName;
			OraclePerfCounter oraclePerfCounter = null;
			foreach (string text in array)
			{
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				OraclePerfCounter oraclePerfCounter2;
				if (!dictionary.TryGetValue(text, out oraclePerfCounter2))
				{
					lock (OraclePerfCounterCollection.syncLock)
					{
						if (!dictionary.TryGetValue(text, out oraclePerfCounter2))
						{
							oraclePerfCounter2 = new OraclePerfCounter(OraclePerfCounterCollection.m_counterNames[(int)iCount], text);
							oraclePerfCounter2.m_higherLevelCounter = oraclePerfCounter;
							dictionary.Add(text, oraclePerfCounter2);
						}
					}
				}
				oraclePerfCounter = oraclePerfCounter2;
			}
			return oraclePerfCounter;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000385C4 File Offset: 0x000367C4
		private static void Dispose()
		{
			for (int i = 0; i < 12; i++)
			{
				foreach (OraclePerfCounter oraclePerfCounter in OraclePerfCounterCollection.m_countersList[i].Values)
				{
					if (oraclePerfCounter != null)
					{
						try
						{
							oraclePerfCounter.Dispose();
						}
						catch (Exception)
						{
						}
					}
				}
				OraclePerfCounterCollection.m_countersList[i].Clear();
				OraclePerfCounterCollection.m_countersList[i] = null;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00038654 File Offset: 0x00036854
		private static void HookToDomainUnloadOrProcessExitEvent()
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				AppDomain.CurrentDomain.ProcessExit += OraclePerfCounterCollection.DomainUnloadOrProcessExit;
				return;
			}
			AppDomain.CurrentDomain.DomainUnload += OraclePerfCounterCollection.DomainUnloadOrProcessExit;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00038690 File Offset: 0x00036890
		private static void DomainUnloadOrProcessExit(object sender, EventArgs e)
		{
			OraclePerfCounterCollection.Dispose();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00038698 File Offset: 0x00036898
		private static void InitializePerformanceCounters()
		{
			if (!string.IsNullOrEmpty("ODP.NET, Managed Driver") && !string.IsNullOrEmpty(OraclePerfParams.m_appDomainPfcInstanceName))
			{
				string appDomainPfcInstanceName = OraclePerfParams.m_appDomainPfcInstanceName;
				OraclePerfCounterCollection.m_countersList[0] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[0].Add(appDomainPfcInstanceName, new OraclePerfCounter("HardConnectsPerSecond"));
				OraclePerfCounterCollection.m_counterNames[0] = "HardConnectsPerSecond";
				OraclePerfCounterCollection.m_countersList[1] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[1].Add(appDomainPfcInstanceName, new OraclePerfCounter("HardDisconnectsPerSecond"));
				OraclePerfCounterCollection.m_counterNames[1] = "HardDisconnectsPerSecond";
				OraclePerfCounterCollection.m_countersList[2] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[2].Add(appDomainPfcInstanceName, new OraclePerfCounter("SoftConnectsPerSecond"));
				OraclePerfCounterCollection.m_counterNames[2] = "SoftConnectsPerSecond";
				OraclePerfCounterCollection.m_countersList[3] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[3].Add(appDomainPfcInstanceName, new OraclePerfCounter("SoftDisconnectsPerSecond"));
				OraclePerfCounterCollection.m_counterNames[3] = "SoftDisconnectsPerSecond";
				OraclePerfCounterCollection.m_countersList[4] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[4].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfActiveConnectionPools"));
				OraclePerfCounterCollection.m_counterNames[4] = "NumberOfActiveConnectionPools";
				OraclePerfCounterCollection.m_countersList[5] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[5].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfActiveConnections"));
				OraclePerfCounterCollection.m_counterNames[5] = "NumberOfActiveConnections";
				OraclePerfCounterCollection.m_countersList[6] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[6].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfFreeConnections"));
				OraclePerfCounterCollection.m_counterNames[6] = "NumberOfFreeConnections";
				OraclePerfCounterCollection.m_countersList[7] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[7].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfInactiveConnectionPools"));
				OraclePerfCounterCollection.m_counterNames[7] = "NumberOfInactiveConnectionPools";
				OraclePerfCounterCollection.m_countersList[8] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[8].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfNonPooledConnections"));
				OraclePerfCounterCollection.m_counterNames[8] = "NumberOfNonPooledConnections";
				OraclePerfCounterCollection.m_countersList[9] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[9].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfPooledConnections"));
				OraclePerfCounterCollection.m_counterNames[9] = "NumberOfPooledConnections";
				OraclePerfCounterCollection.m_countersList[10] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[10].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfReclaimedConnections"));
				OraclePerfCounterCollection.m_counterNames[10] = "NumberOfReclaimedConnections";
				OraclePerfCounterCollection.m_countersList[11] = new Dictionary<string, OraclePerfCounter>();
				OraclePerfCounterCollection.m_countersList[11].Add(appDomainPfcInstanceName, new OraclePerfCounter("NumberOfStasisConnections"));
				OraclePerfCounterCollection.m_counterNames[11] = "NumberOfStasisConnections";
			}
		}

		// Token: 0x0400069D RID: 1693
		private static readonly Dictionary<string, OraclePerfCounter>[] m_countersList = new Dictionary<string, OraclePerfCounter>[12];

		// Token: 0x0400069E RID: 1694
		private static readonly string[] m_counterNames = new string[12];

		// Token: 0x0400069F RID: 1695
		private static object syncLock = new object();
	}
}

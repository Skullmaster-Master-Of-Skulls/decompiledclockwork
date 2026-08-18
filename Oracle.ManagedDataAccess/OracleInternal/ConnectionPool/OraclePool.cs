using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000D2 RID: 210
	internal class OraclePool : Pool<OraclePoolManager, OraclePool, OracleConnectionImpl>
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00054DF0 File Offset: 0x00052FF0
		static OraclePool()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				OraclePerfParams.Counter performanceCounters = (OraclePerfParams.Counter)ConfigBaseClass.m_PerformanceCounters;
				OraclePool.m_bPerfCounterEnabled = (performanceCounters > OraclePerfParams.Counter.None);
				OraclePool.m_bPerfHardConnectsPerSecond = ((ushort)(performanceCounters & OraclePerfParams.Counter.HardConnectsPerSecond) != 0);
				OraclePool.m_bPerfHardDisconnectsPerSecond = ((ushort)(performanceCounters & OraclePerfParams.Counter.HardDisconnectsPerSecond) != 0);
				OraclePool.m_bPerfSoftConnectsPerSecond = ((ushort)(performanceCounters & OraclePerfParams.Counter.SoftConnectsPerSecond) != 0);
				OraclePool.m_bPerfSoftDisconnectsPerSecond = ((ushort)(performanceCounters & OraclePerfParams.Counter.SoftDisconnectsPerSecond) != 0);
				OraclePool.m_bPerfNumberOfActiveConnectionPools = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfActiveConnectionPools) != 0);
				OraclePool.m_bPerfNumberOfInactiveConnectionPools = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfInactiveConnectionPools) != 0);
				OraclePool.m_bPerfNumberOfActiveConnections = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfActiveConnections) != 0);
				OraclePool.m_bPerfNumberOfFreeConnections = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfFreeConnections) != 0);
				OraclePool.m_bPerfNumberOfPooledConnections = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfPooledConnections) != 0);
				OraclePool.m_bPerfNumberOfNonPooledConnections = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfNonPooledConnections) != 0);
				OraclePool.m_bPerfNumberOfReclaimedConnections = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfReclaimedConnections) != 0);
				OraclePool.m_bPerfNumberOfStasisConnections = ((ushort)(performanceCounters & OraclePerfParams.Counter.NumberOfStasisConnections) != 0);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00054F38 File Offset: 0x00053138
		public static void PerformanceCounterDecrement(OraclePerfParams.CounterIndex counterIndex, OracleConnectionImpl pr, OraclePool op)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (op == null)
				{
					OraclePerfCounterCollection.Decrement(counterIndex, pr.m_cs.m_poolName, OraclePool.GetInstanceName(pr));
				}
				else
				{
					op.m_perfCounterCache[(int)counterIndex].Decrement();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00054FD4 File Offset: 0x000531D4
		public static void PerformanceCounterIncrement(OraclePerfParams.CounterIndex counterIndex, OracleConnectionImpl pr, OraclePool op)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (op == null || op.m_perfCounterCache[(int)counterIndex] == null)
				{
					OraclePerfCounter oraclePerfCounter = OraclePerfCounterCollection.Increment(counterIndex, pr.m_cs.m_poolName, OraclePool.GetInstanceName(pr));
					if (op != null)
					{
						op.m_perfCounterCache[(int)counterIndex] = oraclePerfCounter;
					}
				}
				else
				{
					op.m_perfCounterCache[(int)counterIndex].Increment();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00055088 File Offset: 0x00053288
		private static string GetInstanceName(OracleConnectionImpl pr)
		{
			string result = null;
			if (pr.m_cs.m_haEvents || pr.m_cs.m_loadBalancing)
			{
				result = pr.m_instanceName;
			}
			return result;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000550BC File Offset: 0x000532BC
		public override void PutNewPR(OracleConnectionImpl pr, bool bForPoolPopulation)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				base.PutNewPR(pr, bForPoolPopulation);
				if (OraclePool.m_bPerfCounterEnabled)
				{
					if (bForPoolPopulation && OraclePool.m_bPerfNumberOfFreeConnections)
					{
						OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfFreeConnections, pr, this);
					}
					if (OraclePool.m_bPerfNumberOfPooledConnections)
					{
						OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfPooledConnections, pr, this);
					}
					if (!this.m_bIsPoolActive)
					{
						if (OraclePool.m_bPerfNumberOfActiveConnectionPools)
						{
							OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfActiveConnectionPools, pr, this);
						}
						if (OraclePool.m_bPerfNumberOfInactiveConnectionPools)
						{
							OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfInactiveConnectionPools, pr, this);
						}
						this.m_bIsPoolActive = true;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00055188 File Offset: 0x00053388
		public override OracleConnectionImpl Get(CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			OracleConnectionImpl result;
			try
			{
				OracleConnectionImpl oracleConnectionImpl = base.Get(criteriaCtx);
				if (OraclePool.m_bPerfCounterEnabled && oracleConnectionImpl != null && OraclePool.m_bPerfNumberOfFreeConnections)
				{
					OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfFreeConnections, oracleConnectionImpl, this);
				}
				result = oracleConnectionImpl;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x04000AF9 RID: 2809
		public bool m_bIsPoolActive;

		// Token: 0x04000AFA RID: 2810
		public static bool m_bPerfCounterEnabled;

		// Token: 0x04000AFB RID: 2811
		public static bool m_bPerfHardConnectsPerSecond;

		// Token: 0x04000AFC RID: 2812
		public static bool m_bPerfHardDisconnectsPerSecond;

		// Token: 0x04000AFD RID: 2813
		public static bool m_bPerfSoftConnectsPerSecond;

		// Token: 0x04000AFE RID: 2814
		public static bool m_bPerfSoftDisconnectsPerSecond;

		// Token: 0x04000AFF RID: 2815
		public static bool m_bPerfNumberOfActiveConnectionPools;

		// Token: 0x04000B00 RID: 2816
		public static bool m_bPerfNumberOfInactiveConnectionPools;

		// Token: 0x04000B01 RID: 2817
		public static bool m_bPerfNumberOfActiveConnections;

		// Token: 0x04000B02 RID: 2818
		public static bool m_bPerfNumberOfFreeConnections;

		// Token: 0x04000B03 RID: 2819
		public static bool m_bPerfNumberOfPooledConnections;

		// Token: 0x04000B04 RID: 2820
		public static bool m_bPerfNumberOfNonPooledConnections;

		// Token: 0x04000B05 RID: 2821
		public static bool m_bPerfNumberOfReclaimedConnections;

		// Token: 0x04000B06 RID: 2822
		public static bool m_bPerfNumberOfStasisConnections;

		// Token: 0x04000B07 RID: 2823
		private OraclePerfCounter[] m_perfCounterCache = new OraclePerfCounter[12];
	}
}

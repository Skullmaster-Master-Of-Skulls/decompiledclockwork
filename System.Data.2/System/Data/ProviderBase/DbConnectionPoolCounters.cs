using System;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C4 RID: 708
	internal abstract class DbConnectionPoolCounters
	{
		// Token: 0x06002AEE RID: 10990 RVA: 0x0011A2CC File Offset: 0x001196CC
		protected DbConnectionPoolCounters() : this(null, null)
		{
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0011A2E4 File Offset: 0x001196E4
		protected DbConnectionPoolCounters(string categoryName, string categoryHelp)
		{
			AppDomain.CurrentDomain.DomainUnload += this.UnloadEventHandler;
			AppDomain.CurrentDomain.ProcessExit += this.ExitEventHandler;
			AppDomain.CurrentDomain.UnhandledException += this.ExceptionEventHandler;
			string instanceName = null;
			if (!ADP.IsEmpty(categoryName) && ADP.IsPlatformNT5)
			{
				instanceName = this.GetInstanceName();
			}
			this.HardConnectsPerSecond = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.HardConnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.HardConnectsPerSecond.CounterType);
			this.HardDisconnectsPerSecond = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.HardDisconnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.HardDisconnectsPerSecond.CounterType);
			this.NumberOfNonPooledConnections = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfNonPooledConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfNonPooledConnections.CounterType);
			this.NumberOfPooledConnections = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfPooledConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfPooledConnections.CounterType);
			this.NumberOfActiveConnectionPoolGroups = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPoolGroups.CounterName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPoolGroups.CounterType);
			this.NumberOfInactiveConnectionPoolGroups = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPoolGroups.CounterName, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPoolGroups.CounterType);
			this.NumberOfActiveConnectionPools = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPools.CounterName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPools.CounterType);
			this.NumberOfInactiveConnectionPools = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPools.CounterName, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPools.CounterType);
			this.NumberOfStasisConnections = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfStasisConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfStasisConnections.CounterType);
			this.NumberOfReclaimedConnections = new DbConnectionPoolCounters.Counter(categoryName, instanceName, DbConnectionPoolCounters.CreationData.NumberOfReclaimedConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfReclaimedConnections.CounterType);
			string categoryName2 = null;
			if (!ADP.IsEmpty(categoryName))
			{
				TraceSwitch traceSwitch = new TraceSwitch("ConnectionPoolPerformanceCounterDetail", "level of detail to track with connection pool performance counters");
				if (TraceLevel.Verbose == traceSwitch.Level)
				{
					categoryName2 = categoryName;
				}
			}
			this.SoftConnectsPerSecond = new DbConnectionPoolCounters.Counter(categoryName2, instanceName, DbConnectionPoolCounters.CreationData.SoftConnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.SoftConnectsPerSecond.CounterType);
			this.SoftDisconnectsPerSecond = new DbConnectionPoolCounters.Counter(categoryName2, instanceName, DbConnectionPoolCounters.CreationData.SoftDisconnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.SoftDisconnectsPerSecond.CounterType);
			this.NumberOfActiveConnections = new DbConnectionPoolCounters.Counter(categoryName2, instanceName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnections.CounterType);
			this.NumberOfFreeConnections = new DbConnectionPoolCounters.Counter(categoryName2, instanceName, DbConnectionPoolCounters.CreationData.NumberOfFreeConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfFreeConnections.CounterType);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0011A548 File Offset: 0x00119948
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private string GetAssemblyName()
		{
			string result = null;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (null != entryAssembly)
			{
				AssemblyName name = entryAssembly.GetName();
				if (name != null)
				{
					result = name.Name;
				}
			}
			return result;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0011A578 File Offset: 0x00119978
		private string GetInstanceName()
		{
			string text = this.GetAssemblyName();
			if (ADP.IsEmpty(text))
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				if (currentDomain != null)
				{
					text = currentDomain.FriendlyName;
				}
			}
			int currentProcessId = SafeNativeMethods.GetCurrentProcessId();
			string text2 = string.Format(null, "{0}[{1}]", new object[]
			{
				text,
				currentProcessId
			});
			text2 = text2.Replace('(', '[').Replace(')', ']').Replace('#', '_').Replace('/', '_').Replace('\\', '_');
			if (text2.Length > 127)
			{
				int num = (127 - "[...]".Length) / 2;
				int num2 = 127 - num - "[...]".Length;
				text2 = string.Format(null, "{0}{1}{2}", new object[]
				{
					text2.Substring(0, num),
					"[...]",
					text2.Substring(text2.Length - num2, num2)
				});
			}
			return text2;
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0011A664 File Offset: 0x00119A64
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Dispose()
		{
			this.SafeDispose(this.HardConnectsPerSecond);
			this.SafeDispose(this.HardDisconnectsPerSecond);
			this.SafeDispose(this.SoftConnectsPerSecond);
			this.SafeDispose(this.SoftDisconnectsPerSecond);
			this.SafeDispose(this.NumberOfNonPooledConnections);
			this.SafeDispose(this.NumberOfPooledConnections);
			this.SafeDispose(this.NumberOfActiveConnectionPoolGroups);
			this.SafeDispose(this.NumberOfInactiveConnectionPoolGroups);
			this.SafeDispose(this.NumberOfActiveConnectionPools);
			this.SafeDispose(this.NumberOfActiveConnections);
			this.SafeDispose(this.NumberOfFreeConnections);
			this.SafeDispose(this.NumberOfStasisConnections);
			this.SafeDispose(this.NumberOfReclaimedConnections);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0011A710 File Offset: 0x00119B10
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void SafeDispose(DbConnectionPoolCounters.Counter counter)
		{
			if (counter != null)
			{
				counter.Dispose();
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0011A728 File Offset: 0x00119B28
		[PrePrepareMethod]
		private void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e != null && e.IsTerminating)
			{
				this.Dispose();
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0011A748 File Offset: 0x00119B48
		[PrePrepareMethod]
		private void ExitEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0011A75C File Offset: 0x00119B5C
		[PrePrepareMethod]
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x04001B67 RID: 7015
		private const int CounterInstanceNameMaxLength = 127;

		// Token: 0x04001B68 RID: 7016
		internal readonly DbConnectionPoolCounters.Counter HardConnectsPerSecond;

		// Token: 0x04001B69 RID: 7017
		internal readonly DbConnectionPoolCounters.Counter HardDisconnectsPerSecond;

		// Token: 0x04001B6A RID: 7018
		internal readonly DbConnectionPoolCounters.Counter SoftConnectsPerSecond;

		// Token: 0x04001B6B RID: 7019
		internal readonly DbConnectionPoolCounters.Counter SoftDisconnectsPerSecond;

		// Token: 0x04001B6C RID: 7020
		internal readonly DbConnectionPoolCounters.Counter NumberOfNonPooledConnections;

		// Token: 0x04001B6D RID: 7021
		internal readonly DbConnectionPoolCounters.Counter NumberOfPooledConnections;

		// Token: 0x04001B6E RID: 7022
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPoolGroups;

		// Token: 0x04001B6F RID: 7023
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPoolGroups;

		// Token: 0x04001B70 RID: 7024
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPools;

		// Token: 0x04001B71 RID: 7025
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPools;

		// Token: 0x04001B72 RID: 7026
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnections;

		// Token: 0x04001B73 RID: 7027
		internal readonly DbConnectionPoolCounters.Counter NumberOfFreeConnections;

		// Token: 0x04001B74 RID: 7028
		internal readonly DbConnectionPoolCounters.Counter NumberOfStasisConnections;

		// Token: 0x04001B75 RID: 7029
		internal readonly DbConnectionPoolCounters.Counter NumberOfReclaimedConnections;

		// Token: 0x0200042E RID: 1070
		private static class CreationData
		{
			// Token: 0x040022F8 RID: 8952
			internal static readonly CounterCreationData HardConnectsPerSecond = new CounterCreationData("HardConnectsPerSecond", "The number of actual connections per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040022F9 RID: 8953
			internal static readonly CounterCreationData HardDisconnectsPerSecond = new CounterCreationData("HardDisconnectsPerSecond", "The number of actual disconnects per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040022FA RID: 8954
			internal static readonly CounterCreationData SoftConnectsPerSecond = new CounterCreationData("SoftConnectsPerSecond", "The number of connections we get from the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040022FB RID: 8955
			internal static readonly CounterCreationData SoftDisconnectsPerSecond = new CounterCreationData("SoftDisconnectsPerSecond", "The number of connections we return to the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040022FC RID: 8956
			internal static readonly CounterCreationData NumberOfNonPooledConnections = new CounterCreationData("NumberOfNonPooledConnections", "The number of connections that are not using connection pooling", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040022FD RID: 8957
			internal static readonly CounterCreationData NumberOfPooledConnections = new CounterCreationData("NumberOfPooledConnections", "The number of connections that are managed by the connection pooler", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040022FE RID: 8958
			internal static readonly CounterCreationData NumberOfActiveConnectionPoolGroups = new CounterCreationData("NumberOfActiveConnectionPoolGroups", "The number of unique connection strings", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040022FF RID: 8959
			internal static readonly CounterCreationData NumberOfInactiveConnectionPoolGroups = new CounterCreationData("NumberOfInactiveConnectionPoolGroups", "The number of unique connection strings waiting for pruning", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04002300 RID: 8960
			internal static readonly CounterCreationData NumberOfActiveConnectionPools = new CounterCreationData("NumberOfActiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04002301 RID: 8961
			internal static readonly CounterCreationData NumberOfInactiveConnectionPools = new CounterCreationData("NumberOfInactiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04002302 RID: 8962
			internal static readonly CounterCreationData NumberOfActiveConnections = new CounterCreationData("NumberOfActiveConnections", "The number of connections currently in-use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04002303 RID: 8963
			internal static readonly CounterCreationData NumberOfFreeConnections = new CounterCreationData("NumberOfFreeConnections", "The number of connections currently available for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04002304 RID: 8964
			internal static readonly CounterCreationData NumberOfStasisConnections = new CounterCreationData("NumberOfStasisConnections", "The number of connections currently waiting to be made ready for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04002305 RID: 8965
			internal static readonly CounterCreationData NumberOfReclaimedConnections = new CounterCreationData("NumberOfReclaimedConnections", "The number of connections we reclaim from GC'd external connections", PerformanceCounterType.NumberOfItems32);
		}

		// Token: 0x0200042F RID: 1071
		internal sealed class Counter
		{
			// Token: 0x06003620 RID: 13856 RVA: 0x00148DC0 File Offset: 0x001481C0
			internal Counter(string categoryName, string instanceName, string counterName, PerformanceCounterType counterType)
			{
				if (ADP.IsPlatformNT5)
				{
					try
					{
						if (!ADP.IsEmpty(categoryName) && !ADP.IsEmpty(instanceName))
						{
							this._instance = new PerformanceCounter
							{
								CategoryName = categoryName,
								CounterName = counterName,
								InstanceName = instanceName,
								InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
								ReadOnly = false,
								RawValue = 0L
							};
						}
					}
					catch (InvalidOperationException e)
					{
						ADP.TraceExceptionWithoutRethrow(e);
					}
				}
			}

			// Token: 0x06003621 RID: 13857 RVA: 0x00148E4C File Offset: 0x0014824C
			internal void Decrement()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Decrement();
				}
			}

			// Token: 0x06003622 RID: 13858 RVA: 0x00148E6C File Offset: 0x0014826C
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			internal void Dispose()
			{
				PerformanceCounter instance = this._instance;
				this._instance = null;
				if (instance != null)
				{
					instance.RemoveInstance();
				}
			}

			// Token: 0x06003623 RID: 13859 RVA: 0x00148E90 File Offset: 0x00148290
			internal void Increment()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Increment();
				}
			}

			// Token: 0x04002306 RID: 8966
			private PerformanceCounter _instance;
		}
	}
}

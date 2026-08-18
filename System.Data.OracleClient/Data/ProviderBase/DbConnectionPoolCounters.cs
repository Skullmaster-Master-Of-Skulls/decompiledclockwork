using System;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000057 RID: 87
	internal abstract class DbConnectionPoolCounters
	{
		// Token: 0x0600038D RID: 909 RVA: 0x00063E64 File Offset: 0x00063264
		protected DbConnectionPoolCounters() : this(null, null)
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00063E84 File Offset: 0x00063284
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

		// Token: 0x0600038F RID: 911 RVA: 0x000640F4 File Offset: 0x000634F4
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private string GetAssemblyName()
		{
			string result = null;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				AssemblyName name = entryAssembly.GetName();
				if (name != null)
				{
					result = name.Name;
				}
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00064124 File Offset: 0x00063524
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
			return text2.Replace('(', '[').Replace(')', ']').Replace('#', '_').Replace('/', '_').Replace('\\', '_');
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000641B4 File Offset: 0x000635B4
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

		// Token: 0x06000392 RID: 914 RVA: 0x00064264 File Offset: 0x00063664
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void SafeDispose(DbConnectionPoolCounters.Counter counter)
		{
			if (counter != null)
			{
				counter.Dispose();
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00064284 File Offset: 0x00063684
		[PrePrepareMethod]
		private void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e != null && e.IsTerminating)
			{
				this.Dispose();
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000642A4 File Offset: 0x000636A4
		[PrePrepareMethod]
		private void ExitEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000642C4 File Offset: 0x000636C4
		[PrePrepareMethod]
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x0400039E RID: 926
		internal readonly DbConnectionPoolCounters.Counter HardConnectsPerSecond;

		// Token: 0x0400039F RID: 927
		internal readonly DbConnectionPoolCounters.Counter HardDisconnectsPerSecond;

		// Token: 0x040003A0 RID: 928
		internal readonly DbConnectionPoolCounters.Counter SoftConnectsPerSecond;

		// Token: 0x040003A1 RID: 929
		internal readonly DbConnectionPoolCounters.Counter SoftDisconnectsPerSecond;

		// Token: 0x040003A2 RID: 930
		internal readonly DbConnectionPoolCounters.Counter NumberOfNonPooledConnections;

		// Token: 0x040003A3 RID: 931
		internal readonly DbConnectionPoolCounters.Counter NumberOfPooledConnections;

		// Token: 0x040003A4 RID: 932
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPoolGroups;

		// Token: 0x040003A5 RID: 933
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPoolGroups;

		// Token: 0x040003A6 RID: 934
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPools;

		// Token: 0x040003A7 RID: 935
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPools;

		// Token: 0x040003A8 RID: 936
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnections;

		// Token: 0x040003A9 RID: 937
		internal readonly DbConnectionPoolCounters.Counter NumberOfFreeConnections;

		// Token: 0x040003AA RID: 938
		internal readonly DbConnectionPoolCounters.Counter NumberOfStasisConnections;

		// Token: 0x040003AB RID: 939
		internal readonly DbConnectionPoolCounters.Counter NumberOfReclaimedConnections;

		// Token: 0x02000058 RID: 88
		private static class CreationData
		{
			// Token: 0x040003AC RID: 940
			internal static readonly CounterCreationData HardConnectsPerSecond = new CounterCreationData("HardConnectsPerSecond", "The number of actual connections per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040003AD RID: 941
			internal static readonly CounterCreationData HardDisconnectsPerSecond = new CounterCreationData("HardDisconnectsPerSecond", "The number of actual disconnects per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040003AE RID: 942
			internal static readonly CounterCreationData SoftConnectsPerSecond = new CounterCreationData("SoftConnectsPerSecond", "The number of connections we get from the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040003AF RID: 943
			internal static readonly CounterCreationData SoftDisconnectsPerSecond = new CounterCreationData("SoftDisconnectsPerSecond", "The number of connections we return to the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040003B0 RID: 944
			internal static readonly CounterCreationData NumberOfNonPooledConnections = new CounterCreationData("NumberOfNonPooledConnections", "The number of connections that are not using connection pooling", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B1 RID: 945
			internal static readonly CounterCreationData NumberOfPooledConnections = new CounterCreationData("NumberOfPooledConnections", "The number of connections that are managed by the connection pooler", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B2 RID: 946
			internal static readonly CounterCreationData NumberOfActiveConnectionPoolGroups = new CounterCreationData("NumberOfActiveConnectionPoolGroups", "The number of unique connection strings", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B3 RID: 947
			internal static readonly CounterCreationData NumberOfInactiveConnectionPoolGroups = new CounterCreationData("NumberOfInactiveConnectionPoolGroups", "The number of unique connection strings waiting for pruning", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B4 RID: 948
			internal static readonly CounterCreationData NumberOfActiveConnectionPools = new CounterCreationData("NumberOfActiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B5 RID: 949
			internal static readonly CounterCreationData NumberOfInactiveConnectionPools = new CounterCreationData("NumberOfInactiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B6 RID: 950
			internal static readonly CounterCreationData NumberOfActiveConnections = new CounterCreationData("NumberOfActiveConnections", "The number of connections currently in-use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B7 RID: 951
			internal static readonly CounterCreationData NumberOfFreeConnections = new CounterCreationData("NumberOfFreeConnections", "The number of connections currently available for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B8 RID: 952
			internal static readonly CounterCreationData NumberOfStasisConnections = new CounterCreationData("NumberOfStasisConnections", "The number of connections currently waiting to be made ready for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040003B9 RID: 953
			internal static readonly CounterCreationData NumberOfReclaimedConnections = new CounterCreationData("NumberOfReclaimedConnections", "The number of connections we reclaim from GC'd external connections", PerformanceCounterType.NumberOfItems32);
		}

		// Token: 0x02000059 RID: 89
		internal sealed class Counter
		{
			// Token: 0x06000397 RID: 919 RVA: 0x00064454 File Offset: 0x00063854
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

			// Token: 0x06000398 RID: 920 RVA: 0x000644E4 File Offset: 0x000638E4
			internal void Decrement()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Decrement();
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x00064504 File Offset: 0x00063904
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

			// Token: 0x0600039A RID: 922 RVA: 0x00064534 File Offset: 0x00063934
			internal void Increment()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Increment();
				}
			}

			// Token: 0x040003BA RID: 954
			private PerformanceCounter _instance;
		}
	}
}

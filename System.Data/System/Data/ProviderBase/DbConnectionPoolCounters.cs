using System;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000273 RID: 627
	internal abstract class DbConnectionPoolCounters
	{
		// Token: 0x0600213A RID: 8506 RVA: 0x00284948 File Offset: 0x00283D48
		protected DbConnectionPoolCounters() : this(null, null)
		{
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00284968 File Offset: 0x00283D68
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

		// Token: 0x0600213C RID: 8508 RVA: 0x00284BD8 File Offset: 0x00283FD8
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

		// Token: 0x0600213D RID: 8509 RVA: 0x00284C08 File Offset: 0x00284008
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

		// Token: 0x0600213E RID: 8510 RVA: 0x00284C98 File Offset: 0x00284098
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

		// Token: 0x0600213F RID: 8511 RVA: 0x00284D48 File Offset: 0x00284148
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void SafeDispose(DbConnectionPoolCounters.Counter counter)
		{
			if (counter != null)
			{
				counter.Dispose();
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00284D68 File Offset: 0x00284168
		[PrePrepareMethod]
		private void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e != null && e.IsTerminating)
			{
				this.Dispose();
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00284D88 File Offset: 0x00284188
		[PrePrepareMethod]
		private void ExitEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00284DA8 File Offset: 0x002841A8
		[PrePrepareMethod]
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x0400158E RID: 5518
		internal readonly DbConnectionPoolCounters.Counter HardConnectsPerSecond;

		// Token: 0x0400158F RID: 5519
		internal readonly DbConnectionPoolCounters.Counter HardDisconnectsPerSecond;

		// Token: 0x04001590 RID: 5520
		internal readonly DbConnectionPoolCounters.Counter SoftConnectsPerSecond;

		// Token: 0x04001591 RID: 5521
		internal readonly DbConnectionPoolCounters.Counter SoftDisconnectsPerSecond;

		// Token: 0x04001592 RID: 5522
		internal readonly DbConnectionPoolCounters.Counter NumberOfNonPooledConnections;

		// Token: 0x04001593 RID: 5523
		internal readonly DbConnectionPoolCounters.Counter NumberOfPooledConnections;

		// Token: 0x04001594 RID: 5524
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPoolGroups;

		// Token: 0x04001595 RID: 5525
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPoolGroups;

		// Token: 0x04001596 RID: 5526
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPools;

		// Token: 0x04001597 RID: 5527
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPools;

		// Token: 0x04001598 RID: 5528
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnections;

		// Token: 0x04001599 RID: 5529
		internal readonly DbConnectionPoolCounters.Counter NumberOfFreeConnections;

		// Token: 0x0400159A RID: 5530
		internal readonly DbConnectionPoolCounters.Counter NumberOfStasisConnections;

		// Token: 0x0400159B RID: 5531
		internal readonly DbConnectionPoolCounters.Counter NumberOfReclaimedConnections;

		// Token: 0x02000274 RID: 628
		private static class CreationData
		{
			// Token: 0x0400159C RID: 5532
			internal static readonly CounterCreationData HardConnectsPerSecond = new CounterCreationData("HardConnectsPerSecond", "The number of actual connections per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400159D RID: 5533
			internal static readonly CounterCreationData HardDisconnectsPerSecond = new CounterCreationData("HardDisconnectsPerSecond", "The number of actual disconnects per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400159E RID: 5534
			internal static readonly CounterCreationData SoftConnectsPerSecond = new CounterCreationData("SoftConnectsPerSecond", "The number of connections we get from the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400159F RID: 5535
			internal static readonly CounterCreationData SoftDisconnectsPerSecond = new CounterCreationData("SoftDisconnectsPerSecond", "The number of connections we return to the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x040015A0 RID: 5536
			internal static readonly CounterCreationData NumberOfNonPooledConnections = new CounterCreationData("NumberOfNonPooledConnections", "The number of connections that are not using connection pooling", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A1 RID: 5537
			internal static readonly CounterCreationData NumberOfPooledConnections = new CounterCreationData("NumberOfPooledConnections", "The number of connections that are managed by the connection pooler", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A2 RID: 5538
			internal static readonly CounterCreationData NumberOfActiveConnectionPoolGroups = new CounterCreationData("NumberOfActiveConnectionPoolGroups", "The number of unique connection strings", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A3 RID: 5539
			internal static readonly CounterCreationData NumberOfInactiveConnectionPoolGroups = new CounterCreationData("NumberOfInactiveConnectionPoolGroups", "The number of unique connection strings waiting for pruning", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A4 RID: 5540
			internal static readonly CounterCreationData NumberOfActiveConnectionPools = new CounterCreationData("NumberOfActiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A5 RID: 5541
			internal static readonly CounterCreationData NumberOfInactiveConnectionPools = new CounterCreationData("NumberOfInactiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A6 RID: 5542
			internal static readonly CounterCreationData NumberOfActiveConnections = new CounterCreationData("NumberOfActiveConnections", "The number of connections currently in-use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A7 RID: 5543
			internal static readonly CounterCreationData NumberOfFreeConnections = new CounterCreationData("NumberOfFreeConnections", "The number of connections currently available for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A8 RID: 5544
			internal static readonly CounterCreationData NumberOfStasisConnections = new CounterCreationData("NumberOfStasisConnections", "The number of connections currently waiting to be made ready for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x040015A9 RID: 5545
			internal static readonly CounterCreationData NumberOfReclaimedConnections = new CounterCreationData("NumberOfReclaimedConnections", "The number of connections we reclaim from GC'd external connections", PerformanceCounterType.NumberOfItems32);
		}

		// Token: 0x02000275 RID: 629
		internal sealed class Counter
		{
			// Token: 0x06002144 RID: 8516 RVA: 0x00284F38 File Offset: 0x00284338
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

			// Token: 0x06002145 RID: 8517 RVA: 0x00284FC8 File Offset: 0x002843C8
			internal void Decrement()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Decrement();
				}
			}

			// Token: 0x06002146 RID: 8518 RVA: 0x00284FE8 File Offset: 0x002843E8
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

			// Token: 0x06002147 RID: 8519 RVA: 0x00285018 File Offset: 0x00284418
			internal void Increment()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Increment();
				}
			}

			// Token: 0x040015AA RID: 5546
			private PerformanceCounter _instance;
		}
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Configuration;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001CD RID: 461
	internal sealed class NetworkingPerfCounters
	{
		// Token: 0x06001237 RID: 4663 RVA: 0x00061104 File Offset: 0x0005F304
		private NetworkingPerfCounters()
		{
			this.enabled = SettingsSectionInternal.Section.PerformanceCountersEnabled;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0006111C File Offset: 0x0005F31C
		public static NetworkingPerfCounters Instance
		{
			get
			{
				if (NetworkingPerfCounters.instance == null)
				{
					object obj = NetworkingPerfCounters.lockObject;
					lock (obj)
					{
						if (NetworkingPerfCounters.instance == null)
						{
							NetworkingPerfCounters.CreateInstance();
						}
					}
				}
				return NetworkingPerfCounters.instance;
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00061174 File Offset: 0x0005F374
		public static long GetTimestamp()
		{
			return Stopwatch.GetTimestamp();
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0006117B File Offset: 0x0005F37B
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00061183 File Offset: 0x0005F383
		public void Increment(NetworkingPerfCounterName perfCounter)
		{
			this.Increment(perfCounter, 1L);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00061190 File Offset: 0x0005F390
		public void Increment(NetworkingPerfCounterName perfCounter, long amount)
		{
			if (this.CounterAvailable())
			{
				try
				{
					NetworkingPerfCounters.CounterPair counterPair = this.counters[(int)perfCounter];
					counterPair.InstanceCounter.IncrementBy(amount);
					counterPair.GlobalCounter.IncrementBy(amount);
				}
				catch (InvalidOperationException e)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Increment", e);
					}
				}
				catch (Win32Exception e2)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Increment", e2);
					}
				}
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00061224 File Offset: 0x0005F424
		public void Decrement(NetworkingPerfCounterName perfCounter)
		{
			this.Increment(perfCounter, -1L);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0006122F File Offset: 0x0005F42F
		public void Decrement(NetworkingPerfCounterName perfCounter, long amount)
		{
			this.Increment(perfCounter, -amount);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0006123C File Offset: 0x0005F43C
		public void IncrementAverage(NetworkingPerfCounterName perfCounter, long startTimestamp)
		{
			if (this.CounterAvailable())
			{
				long timestamp = NetworkingPerfCounters.GetTimestamp();
				long amount = (timestamp - startTimestamp) * 1000L / Stopwatch.Frequency;
				this.Increment(perfCounter, amount);
				this.Increment(perfCounter + 1, 1L);
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00061280 File Offset: 0x0005F480
		private void Initialize(object state)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_perfcounter_initialization_started"));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PermissionState.Unrestricted);
			performanceCounterPermission.Assert();
			try
			{
				if (!PerformanceCounterCategory.Exists(".NET CLR Networking 4.0.0.0"))
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_perfcounter_nocategory", new object[]
						{
							".NET CLR Networking 4.0.0.0"
						}));
					}
				}
				else
				{
					string instanceName = NetworkingPerfCounters.GetInstanceName();
					this.counters = new NetworkingPerfCounters.CounterPair[NetworkingPerfCounters.counterNames.Length];
					for (int i = 0; i < NetworkingPerfCounters.counterNames.Length; i++)
					{
						this.counters[i] = NetworkingPerfCounters.CreateCounterPair(NetworkingPerfCounters.counterNames[i], instanceName);
					}
					AppDomain.CurrentDomain.DomainUnload += this.UnloadEventHandler;
					AppDomain.CurrentDomain.ProcessExit += this.ExitEventHandler;
					AppDomain.CurrentDomain.UnhandledException += this.ExceptionEventHandler;
					this.initSuccessful = true;
				}
			}
			catch (Win32Exception e)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Initialize", e);
				}
				this.Cleanup();
			}
			catch (InvalidOperationException e2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Initialize", e2);
				}
				this.Cleanup();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				this.initDone = true;
				if (Logging.On)
				{
					if (this.initSuccessful)
					{
						Logging.PrintInfo(Logging.Web, SR.GetString("net_perfcounter_initialized_success"));
					}
					else
					{
						Logging.PrintInfo(Logging.Web, SR.GetString("net_perfcounter_initialized_error"));
					}
				}
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0006145C File Offset: 0x0005F65C
		private static void CreateInstance()
		{
			NetworkingPerfCounters.instance = new NetworkingPerfCounters();
			if (NetworkingPerfCounters.instance.Enabled && !ThreadPool.QueueUserWorkItem(new WaitCallback(NetworkingPerfCounters.instance.Initialize)) && Logging.On)
			{
				Logging.PrintError(Logging.Web, SR.GetString("net_perfcounter_cant_queue_workitem"));
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000614B8 File Offset: 0x0005F6B8
		private static NetworkingPerfCounters.CounterPair CreateCounterPair(string counterName, string instanceName)
		{
			PerformanceCounter globalCounter = new PerformanceCounter(".NET CLR Networking 4.0.0.0", counterName, "_Global_", false);
			return new NetworkingPerfCounters.CounterPair(new PerformanceCounter
			{
				CategoryName = ".NET CLR Networking 4.0.0.0",
				CounterName = counterName,
				InstanceName = instanceName,
				InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
				ReadOnly = false,
				RawValue = 0L
			}, globalCounter);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00061513 File Offset: 0x0005F713
		private void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.IsTerminating)
			{
				this.Cleanup();
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00061523 File Offset: 0x0005F723
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			this.Cleanup();
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0006152B File Offset: 0x0005F72B
		private void ExitEventHandler(object sender, EventArgs e)
		{
			this.Cleanup();
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00061534 File Offset: 0x0005F734
		private void Cleanup()
		{
			object obj = NetworkingPerfCounters.lockObject;
			lock (obj)
			{
				if (!this.cleanupCalled)
				{
					this.cleanupCalled = true;
					if (this.counters != null)
					{
						foreach (NetworkingPerfCounters.CounterPair counterPair in this.counters)
						{
							if (!Environment.HasShutdownStarted && counterPair != null)
							{
								try
								{
									counterPair.InstanceCounter.RemoveInstance();
								}
								catch (InvalidOperationException e)
								{
									if (Logging.On)
									{
										Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Cleanup", e);
									}
								}
								catch (Win32Exception e2)
								{
									if (Logging.On)
									{
										Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Cleanup", e2);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00061618 File Offset: 0x0005F818
		private static string GetInstanceName()
		{
			string text = NetworkingPerfCounters.ReplaceInvalidChars(AppDomain.CurrentDomain.FriendlyName);
			string text2 = VersioningHelper.MakeVersionSafeName(string.Empty, ResourceScope.Machine, ResourceScope.AppDomain);
			string text3 = text + text2;
			if (text3.Length > 127)
			{
				text3 = text.Substring(0, 127 - text2.Length) + text2;
			}
			return text3;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0006166C File Offset: 0x0005F86C
		private static string ReplaceInvalidChars(string instanceName)
		{
			StringBuilder stringBuilder = new StringBuilder(instanceName);
			int i = 0;
			while (i < stringBuilder.Length)
			{
				char c = stringBuilder[i];
				if (c <= '(')
				{
					if (c == '#')
					{
						goto IL_4B;
					}
					if (c == '(')
					{
						stringBuilder[i] = '[';
					}
				}
				else if (c != ')')
				{
					if (c == '/' || c == '\\')
					{
						goto IL_4B;
					}
				}
				else
				{
					stringBuilder[i] = ']';
				}
				IL_54:
				i++;
				continue;
				IL_4B:
				stringBuilder[i] = '_';
				goto IL_54;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000616E0 File Offset: 0x0005F8E0
		private bool CounterAvailable()
		{
			return this.enabled && !this.cleanupCalled && this.initDone && this.initSuccessful;
		}

		// Token: 0x0400149C RID: 5276
		private const int instanceNameMaxLength = 127;

		// Token: 0x0400149D RID: 5277
		private const string categoryName = ".NET CLR Networking 4.0.0.0";

		// Token: 0x0400149E RID: 5278
		private const string globalInstanceName = "_Global_";

		// Token: 0x0400149F RID: 5279
		private static readonly string[] counterNames = new string[]
		{
			"Connections Established",
			"Bytes Received",
			"Bytes Sent",
			"Datagrams Received",
			"Datagrams Sent",
			"HttpWebRequests Created/Sec",
			"HttpWebRequests Average Lifetime",
			"HttpWebRequests Average Lifetime Base",
			"HttpWebRequests Queued/Sec",
			"HttpWebRequests Average Queue Time",
			"HttpWebRequests Average Queue Time Base",
			"HttpWebRequests Aborted/Sec",
			"HttpWebRequests Failed/Sec"
		};

		// Token: 0x040014A0 RID: 5280
		private static volatile NetworkingPerfCounters instance;

		// Token: 0x040014A1 RID: 5281
		private static object lockObject = new object();

		// Token: 0x040014A2 RID: 5282
		private volatile bool initDone;

		// Token: 0x040014A3 RID: 5283
		private bool initSuccessful;

		// Token: 0x040014A4 RID: 5284
		private NetworkingPerfCounters.CounterPair[] counters;

		// Token: 0x040014A5 RID: 5285
		private bool enabled;

		// Token: 0x040014A6 RID: 5286
		private volatile bool cleanupCalled;

		// Token: 0x02000752 RID: 1874
		private class CounterPair
		{
			// Token: 0x17000F16 RID: 3862
			// (get) Token: 0x06004201 RID: 16897 RVA: 0x00112520 File Offset: 0x00110720
			public PerformanceCounter InstanceCounter
			{
				get
				{
					return this.instanceCounter;
				}
			}

			// Token: 0x17000F17 RID: 3863
			// (get) Token: 0x06004202 RID: 16898 RVA: 0x00112528 File Offset: 0x00110728
			public PerformanceCounter GlobalCounter
			{
				get
				{
					return this.globalCounter;
				}
			}

			// Token: 0x06004203 RID: 16899 RVA: 0x00112530 File Offset: 0x00110730
			public CounterPair(PerformanceCounter instanceCounter, PerformanceCounter globalCounter)
			{
				this.instanceCounter = instanceCounter;
				this.globalCounter = globalCounter;
			}

			// Token: 0x0400320D RID: 12813
			private PerformanceCounter instanceCounter;

			// Token: 0x0400320E RID: 12814
			private PerformanceCounter globalCounter;
		}
	}
}

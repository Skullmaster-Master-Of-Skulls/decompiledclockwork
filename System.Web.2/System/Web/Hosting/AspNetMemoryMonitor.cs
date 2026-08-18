using System;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	// Token: 0x02000788 RID: 1928
	public sealed class AspNetMemoryMonitor : IApplicationMonitor, IDisposable, IObservable<RecycleLimitInfo>, IObservable<LowPhysicalMemoryInfo>
	{
		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x06005C58 RID: 23640 RVA: 0x0013F854 File Offset: 0x0013DA54
		internal static long ConfiguredProcessMemoryLimit
		{
			get
			{
				long num = AspNetMemoryMonitor.s_configuredProcessMemoryLimit;
				if (num == 0L)
				{
					if (UnsafeNativeMethods.GetModuleHandle("aspnet_wp.exe") != IntPtr.Zero)
					{
						num = (long)UnsafeNativeMethods.PMGetMemoryLimitInMB() << 20;
					}
					else if (UnsafeNativeMethods.GetModuleHandle("w3wp.exe") != IntPtr.Zero)
					{
						IServerConfig instance = ServerConfig.GetInstance();
						num = instance.GetW3WPMemoryLimitInKB() << 10;
					}
					Interlocked.Exchange(ref AspNetMemoryMonitor.s_configuredProcessMemoryLimit, num);
				}
				return num;
			}
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x06005C59 RID: 23641 RVA: 0x0013F8C0 File Offset: 0x0013DAC0
		internal static long ProcessPrivateBytesLimit
		{
			get
			{
				long num = AspNetMemoryMonitor.s_processPrivateBytesLimit;
				if (num == -1L)
				{
					num = AspNetMemoryMonitor.ConfiguredProcessMemoryLimit;
					if (num == 0L)
					{
						bool flag = IntPtr.Size == 8;
						if (AspNetMemoryMonitor.s_totalPhysical != 0L)
						{
							long val;
							if (flag)
							{
								val = 1099511627776L;
							}
							else if (AspNetMemoryMonitor.s_totalVirtual > (long)((ulong)-2147483648))
							{
								val = 1887436800L;
							}
							else
							{
								val = 838860800L;
							}
							long val2 = HostingEnvironment.IsHosted ? (AspNetMemoryMonitor.s_totalPhysical * 3L / 5L) : AspNetMemoryMonitor.s_totalPhysical;
							num = Math.Min(val2, val);
						}
						else
						{
							num = (flag ? 1099511627776L : 838860800L);
						}
					}
					Interlocked.Exchange(ref AspNetMemoryMonitor.s_processPrivateBytesLimit, num);
				}
				return num;
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x0013F966 File Offset: 0x0013DB66
		internal static long PhysicalMemoryPercentageLimit
		{
			get
			{
				if (AspNetMemoryMonitor._firstMemoryMonitor != null && AspNetMemoryMonitor._firstMemoryMonitor._lowMemoryMonitor != null)
				{
					return (long)AspNetMemoryMonitor._firstMemoryMonitor._lowMemoryMonitor.PressureHigh;
				}
				return 0L;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x06005C5B RID: 23643 RVA: 0x0013F98E File Offset: 0x0013DB8E
		// (set) Token: 0x06005C5C RID: 23644 RVA: 0x0013F996 File Offset: 0x0013DB96
		public IObserver<LowPhysicalMemoryInfo> DefaultLowPhysicalMemoryObserver
		{
			get
			{
				return this._defaultLowMemObserver;
			}
			set
			{
				if (this._defaultLowMemSubscription != null)
				{
					this._defaultLowMemSubscription.Dispose();
					this._defaultLowMemSubscription = null;
				}
				this._defaultLowMemObserver = null;
				if (value != null)
				{
					this._defaultLowMemObserver = value;
					this._defaultLowMemSubscription = this.Subscribe(value);
				}
			}
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x06005C5D RID: 23645 RVA: 0x0013F9D0 File Offset: 0x0013DBD0
		// (set) Token: 0x06005C5E RID: 23646 RVA: 0x0013F9D8 File Offset: 0x0013DBD8
		public IObserver<RecycleLimitInfo> DefaultRecycleLimitObserver
		{
			get
			{
				return this._defaultRecycleObserver;
			}
			set
			{
				if (this._defaultRecycleSubscription != null)
				{
					this._defaultRecycleSubscription.Dispose();
					this._defaultRecycleSubscription = null;
				}
				this._defaultRecycleObserver = null;
				if (value != null)
				{
					this._defaultRecycleObserver = value;
					this._defaultRecycleSubscription = this.Subscribe(value);
				}
			}
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x0013FA14 File Offset: 0x0013DC14
		static AspNetMemoryMonitor()
		{
			UnsafeNativeMethods.MEMORYSTATUSEX memorystatusex = default(UnsafeNativeMethods.MEMORYSTATUSEX);
			memorystatusex.Init();
			if (UnsafeNativeMethods.GlobalMemoryStatusEx(ref memorystatusex) != 0)
			{
				AspNetMemoryMonitor.s_totalPhysical = memorystatusex.ullTotalPhys;
				AspNetMemoryMonitor.s_totalVirtual = memorystatusex.ullTotalVirtual;
			}
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x0013FA64 File Offset: 0x0013DC64
		internal AspNetMemoryMonitor()
		{
			this._recycleMonitor = new RecycleLimitMonitor();
			this.DefaultRecycleLimitObserver = new RecycleLimitObserver();
			this._lowMemoryMonitor = new LowPhysicalMemoryMonitor();
			this.DefaultLowPhysicalMemoryObserver = new LowPhysicalMemoryObserver();
			if (AspNetMemoryMonitor._firstMemoryMonitor == null)
			{
				AspNetMemoryMonitor._firstMemoryMonitor = this;
			}
		}

		// Token: 0x06005C61 RID: 23649 RVA: 0x0013FAB0 File Offset: 0x0013DCB0
		public IDisposable Subscribe(IObserver<LowPhysicalMemoryInfo> observer)
		{
			if (this._lowMemoryMonitor != null)
			{
				this._lowMemoryMonitor.Subscribe(observer);
			}
			return new AspNetMemoryMonitor.Unsubscriber(delegate()
			{
				this._lowMemoryMonitor.Unsubscribe(observer);
			});
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x0013FAFC File Offset: 0x0013DCFC
		public IDisposable Subscribe(IObserver<RecycleLimitInfo> observer)
		{
			if (this._recycleMonitor != null)
			{
				this._recycleMonitor.Subscribe(observer);
			}
			return new AspNetMemoryMonitor.Unsubscriber(delegate()
			{
				this._recycleMonitor.Unsubscribe(observer);
			});
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x0013FB47 File Offset: 0x0013DD47
		public void Start()
		{
			this._recycleMonitor.Start();
			this._lowMemoryMonitor.Start();
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x0013FB5F File Offset: 0x0013DD5F
		public void Stop()
		{
			this._recycleMonitor.Stop();
			this._lowMemoryMonitor.Stop();
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x0013FB77 File Offset: 0x0013DD77
		public void Dispose()
		{
			this.DefaultLowPhysicalMemoryObserver = null;
			this.DefaultRecycleLimitObserver = null;
			this._recycleMonitor.Dispose();
		}

		// Token: 0x0400309D RID: 12445
		internal const long TERABYTE = 1099511627776L;

		// Token: 0x0400309E RID: 12446
		internal const long GIGABYTE = 1073741824L;

		// Token: 0x0400309F RID: 12447
		internal const long MEGABYTE = 1048576L;

		// Token: 0x040030A0 RID: 12448
		internal const long KILOBYTE = 1024L;

		// Token: 0x040030A1 RID: 12449
		internal const long PRIVATE_BYTES_LIMIT_2GB = 838860800L;

		// Token: 0x040030A2 RID: 12450
		internal const long PRIVATE_BYTES_LIMIT_3GB = 1887436800L;

		// Token: 0x040030A3 RID: 12451
		internal const long PRIVATE_BYTES_LIMIT_64BIT = 1099511627776L;

		// Token: 0x040030A4 RID: 12452
		internal static long s_totalPhysical;

		// Token: 0x040030A5 RID: 12453
		internal static long s_totalVirtual;

		// Token: 0x040030A6 RID: 12454
		internal static long s_processPrivateBytesLimit = -1L;

		// Token: 0x040030A7 RID: 12455
		internal static long s_configuredProcessMemoryLimit = 0L;

		// Token: 0x040030A8 RID: 12456
		private static AspNetMemoryMonitor _firstMemoryMonitor = null;

		// Token: 0x040030A9 RID: 12457
		private RecycleLimitMonitor _recycleMonitor;

		// Token: 0x040030AA RID: 12458
		private IObserver<RecycleLimitInfo> _defaultRecycleObserver;

		// Token: 0x040030AB RID: 12459
		private IDisposable _defaultRecycleSubscription;

		// Token: 0x040030AC RID: 12460
		private LowPhysicalMemoryMonitor _lowMemoryMonitor;

		// Token: 0x040030AD RID: 12461
		private IObserver<LowPhysicalMemoryInfo> _defaultLowMemObserver;

		// Token: 0x040030AE RID: 12462
		private IDisposable _defaultLowMemSubscription;

		// Token: 0x02000A4C RID: 2636
		private class Unsubscriber : IDisposable
		{
			// Token: 0x06006EA9 RID: 28329 RVA: 0x0018A3DE File Offset: 0x001885DE
			public Unsubscriber(Action unsubscribeAction)
			{
				this._unsub = unsubscribeAction;
			}

			// Token: 0x06006EAA RID: 28330 RVA: 0x0018A3ED File Offset: 0x001885ED
			public void Dispose()
			{
				if (this._unsub != null)
				{
					this._unsub();
				}
			}

			// Token: 0x04003B3A RID: 15162
			private Action _unsub;
		}
	}
}

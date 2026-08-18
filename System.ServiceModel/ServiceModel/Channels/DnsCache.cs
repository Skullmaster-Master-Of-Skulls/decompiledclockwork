using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000777 RID: 1911
	internal static class DnsCache
	{
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x0600490D RID: 18701 RVA: 0x0010D764 File Offset: 0x0010B964
		private static object ThisLock
		{
			get
			{
				return DnsCache.resolveCache;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x0010D76C File Offset: 0x0010B96C
		public static string MachineName
		{
			get
			{
				if (DnsCache.machineName == null)
				{
					object thisLock = DnsCache.ThisLock;
					lock (thisLock)
					{
						if (DnsCache.machineName == null)
						{
							try
							{
								DnsCache.machineName = Dns.GetHostEntry(string.Empty).HostName;
							}
							catch (SocketException exception)
							{
								DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
								DnsCache.machineName = UnsafeNativeMethods.GetComputerName(ComputerNameFormat.PhysicalNetBIOS);
							}
						}
					}
				}
				return DnsCache.machineName;
			}
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x0010D7FC File Offset: 0x0010B9FC
		public static IPHostEntry Resolve(Uri uri)
		{
			string dnsSafeHost = uri.DnsSafeHost;
			IPHostEntry iphostEntry = null;
			DateTime utcNow = DateTime.UtcNow;
			object thisLock = DnsCache.ThisLock;
			lock (thisLock)
			{
				DnsCache.DnsCacheEntry dnsCacheEntry;
				if (DnsCache.resolveCache.TryGetValue(dnsSafeHost, out dnsCacheEntry))
				{
					if (utcNow.Subtract(dnsCacheEntry.TimeStamp) > DnsCache.cacheTimeout)
					{
						DnsCache.resolveCache.Remove(dnsSafeHost);
					}
					else
					{
						if (dnsCacheEntry.HostEntry == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("DnsResolveFailed", new object[]
							{
								dnsSafeHost
							})));
						}
						iphostEntry = dnsCacheEntry.HostEntry;
					}
				}
			}
			if (iphostEntry == null)
			{
				SocketException ex = null;
				try
				{
					iphostEntry = Dns.GetHostEntry(dnsSafeHost);
				}
				catch (SocketException ex2)
				{
					ex = ex2;
				}
				object thisLock2 = DnsCache.ThisLock;
				lock (thisLock2)
				{
					DnsCache.resolveCache.Remove(dnsSafeHost);
					DnsCache.resolveCache.Add(dnsSafeHost, new DnsCache.DnsCacheEntry(iphostEntry, utcNow));
				}
				if (ex != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("DnsResolveFailed", new object[]
					{
						dnsSafeHost
					}), ex));
				}
			}
			return iphostEntry;
		}

		// Token: 0x04002E19 RID: 11801
		private const int mruWatermark = 64;

		// Token: 0x04002E1A RID: 11802
		private static MruCache<string, DnsCache.DnsCacheEntry> resolveCache = new MruCache<string, DnsCache.DnsCacheEntry>(64);

		// Token: 0x04002E1B RID: 11803
		private static readonly TimeSpan cacheTimeout = TimeSpan.FromSeconds(2.0);

		// Token: 0x04002E1C RID: 11804
		private static volatile string machineName;

		// Token: 0x02000CED RID: 3309
		private class DnsCacheEntry
		{
			// Token: 0x06007A68 RID: 31336 RVA: 0x001C812D File Offset: 0x001C632D
			public DnsCacheEntry(IPHostEntry hostEntry, DateTime timeStamp)
			{
				this.hostEntry = hostEntry;
				this.timeStamp = timeStamp;
			}

			// Token: 0x17001BB5 RID: 7093
			// (get) Token: 0x06007A69 RID: 31337 RVA: 0x001C8143 File Offset: 0x001C6343
			public IPHostEntry HostEntry
			{
				get
				{
					return this.hostEntry;
				}
			}

			// Token: 0x17001BB6 RID: 7094
			// (get) Token: 0x06007A6A RID: 31338 RVA: 0x001C814B File Offset: 0x001C634B
			public DateTime TimeStamp
			{
				get
				{
					return this.timeStamp;
				}
			}

			// Token: 0x04004601 RID: 17921
			private IPHostEntry hostEntry;

			// Token: 0x04004602 RID: 17922
			private DateTime timeStamp;
		}
	}
}

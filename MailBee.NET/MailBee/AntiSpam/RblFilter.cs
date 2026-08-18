using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using a.d;
using MailBee.DnsMX;
using MailBee.Mime;

namespace MailBee.AntiSpam
{
	// Token: 0x0200012C RID: 300
	public class RblFilter : IComponent
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0002CC52 File Offset: 0x0002BC52
		public RblFilter() : this(null)
		{
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002CC5B File Offset: 0x0002BC5B
		public RblFilter(string licenseKey)
		{
			RblFilter.a(licenseKey);
			this.g = new n(null, null, this);
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002CC77 File Offset: 0x0002BC77
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002CC83 File Offset: 0x0002BC83
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x0002CC95 File Offset: 0x0002BC95
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.g.bp().d();
			}
			set
			{
				this.g.bp().a(value);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0002CCA8 File Offset: 0x0002BCA8
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002CCAF File Offset: 0x0002BCAF
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002CCB8 File Offset: 0x0002BCB8
		protected virtual void Dispose(bool disposing)
		{
			if (!this.h)
			{
				if (disposing)
				{
					this.g.bo();
					if (this.a != null)
					{
						this.a(this, EventArgs.Empty);
					}
				}
				this.h = true;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000983 RID: 2435 RVA: 0x0002CCF0 File Offset: 0x0002BCF0
		// (remove) Token: 0x06000984 RID: 2436 RVA: 0x0002CD28 File Offset: 0x0002BD28
		public event EventHandler Disposed
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.a;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.a, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.a;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.a, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0002CD5D File Offset: 0x0002BD5D
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0002CD65 File Offset: 0x0002BD65
		public virtual ISite Site
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002CD6E File Offset: 0x0002BD6E
		public bool IsBusy
		{
			get
			{
				return this.g.bc();
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002CD7B File Offset: 0x0002BD7B
		public void Abort()
		{
			this.g.bd();
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0002CD88 File Offset: 0x0002BD88
		public bool IsAborted
		{
			get
			{
				return this.g.bf();
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0002CD95 File Offset: 0x0002BD95
		public Logger Log
		{
			get
			{
				return this.g.bi();
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0002CDA2 File Offset: 0x0002BDA2
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0002CDAF File Offset: 0x0002BDAF
		public bool RaiseEvents
		{
			get
			{
				return this.g.bq();
			}
			set
			{
				this.g.k(value);
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002CDBD File Offset: 0x0002BDBD
		internal bool b()
		{
			return this.c != null;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002CDC8 File Offset: 0x0002BDC8
		protected internal void OnErrorOccurred(ErrorEventArgs args)
		{
			this.g.bp().a(this.c, this, args);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002CDE2 File Offset: 0x0002BDE2
		internal bool c()
		{
			return this.d != null;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002CDED File Offset: 0x0002BDED
		protected internal void OnLogNewEntry(LogNewEntryEventArgs args)
		{
			this.g.bp().a(this.d, this, args);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002CE07 File Offset: 0x0002BE07
		internal bool a()
		{
			return this.e != null;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002CE12 File Offset: 0x0002BE12
		protected internal void OnDataReceived(DataTransferEventArgs args)
		{
			this.g.bp().a(this.e, this, args);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002CE2C File Offset: 0x0002BE2C
		internal bool d()
		{
			return this.f != null;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002CE37 File Offset: 0x0002BE37
		protected internal void OnDataSent(DataTransferEventArgs args)
		{
			this.g.bp().a(this.f, this, args);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002CE51 File Offset: 0x0002BE51
		private static void a(string A_0)
		{
			Global.a(typeof(BayesFilter), A_0);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002CE63 File Offset: 0x0002BE63
		public void ResetState()
		{
			this.g.cb();
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0002CE70 File Offset: 0x0002BE70
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0002CE7D File Offset: 0x0002BE7D
		public int MaxThreadCount
		{
			get
			{
				return this.g.m();
			}
			set
			{
				this.g.a(value);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0002CE8B File Offset: 0x0002BE8B
		public DnsServerCollection DnsServers
		{
			get
			{
				return this.g.aq();
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002CE98 File Offset: 0x0002BE98
		public bool IsIPAddressInRbl(string ipString, string rblHost)
		{
			return this.g.a(true, ipString, rblHost);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002CEA8 File Offset: 0x0002BEA8
		public bool IsMailOriginatingIPAddressInRbl(MailMessage msg, int receivedIndex, string rblHost)
		{
			if (msg == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (receivedIndex < 0 || receivedIndex >= msg.TimeStamps.Count)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			string ip = msg.TimeStamps[receivedIndex].IP;
			return !(ip == string.Empty) && this.g.a(true, ip, rblHost);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002CF09 File Offset: 0x0002BF09
		public RblStatusCollection GetRblStatusesOfIPAddress(string ipString, string[] rblHosts)
		{
			return this.g.b(true, ipString, rblHosts);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002CF1C File Offset: 0x0002BF1C
		public RblStatusCollection GetRblStatusesOfMailOriginatingIPAddress(MailMessage msg, int receivedIndex, string[] rblHosts)
		{
			if (msg == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (receivedIndex < 0 || receivedIndex >= msg.TimeStamps.Count)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			string ip = msg.TimeStamps[receivedIndex].IP;
			if (ip == string.Empty)
			{
				return null;
			}
			return this.g.b(true, ip, rblHosts);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002CF7D File Offset: 0x0002BF7D
		public Task<bool> IsIPAddressInRblAsync(string ipString, string rblHost)
		{
			return this.g.b(true, ipString, rblHost);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002CF90 File Offset: 0x0002BF90
		public Task<bool> IsMailOriginatingIPAddressInRblAsync(MailMessage msg, int receivedIndex, string rblHost)
		{
			string ip = msg.TimeStamps[receivedIndex].IP;
			if (ip == string.Empty)
			{
				return Task.FromResult<bool>(false);
			}
			return this.g.b(true, ip, rblHost);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002CFD1 File Offset: 0x0002BFD1
		public Task<RblStatusCollection> GetRblStatusesOfIPAddressAsync(string ipString, string[] rblHosts)
		{
			return this.g.a(true, ipString, rblHosts);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002CFE4 File Offset: 0x0002BFE4
		public Task<RblStatusCollection> GetRblStatusesOfMailOriginatingIPAddressAsync(MailMessage msg, int receivedIndex, string[] rblHosts)
		{
			string ip = msg.TimeStamps[receivedIndex].IP;
			if (ip == string.Empty)
			{
				return Task.FromResult<RblStatusCollection>(null);
			}
			return this.g.a(true, ip, rblHosts);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060009A2 RID: 2466 RVA: 0x0002D028 File Offset: 0x0002C028
		// (remove) Token: 0x060009A3 RID: 2467 RVA: 0x0002D060 File Offset: 0x0002C060
		public event ErrorEventHandler ErrorOccurred
		{
			[CompilerGenerated]
			add
			{
				ErrorEventHandler errorEventHandler = this.c;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Combine(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.c, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ErrorEventHandler errorEventHandler = this.c;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Remove(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.c, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060009A4 RID: 2468 RVA: 0x0002D098 File Offset: 0x0002C098
		// (remove) Token: 0x060009A5 RID: 2469 RVA: 0x0002D0D0 File Offset: 0x0002C0D0
		public event LogNewEntryEventHandler LogNewEntry
		{
			[CompilerGenerated]
			add
			{
				LogNewEntryEventHandler logNewEntryEventHandler = this.d;
				LogNewEntryEventHandler logNewEntryEventHandler2;
				do
				{
					logNewEntryEventHandler2 = logNewEntryEventHandler;
					LogNewEntryEventHandler value2 = (LogNewEntryEventHandler)Delegate.Combine(logNewEntryEventHandler2, value);
					logNewEntryEventHandler = Interlocked.CompareExchange<LogNewEntryEventHandler>(ref this.d, value2, logNewEntryEventHandler2);
				}
				while (logNewEntryEventHandler != logNewEntryEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				LogNewEntryEventHandler logNewEntryEventHandler = this.d;
				LogNewEntryEventHandler logNewEntryEventHandler2;
				do
				{
					logNewEntryEventHandler2 = logNewEntryEventHandler;
					LogNewEntryEventHandler value2 = (LogNewEntryEventHandler)Delegate.Remove(logNewEntryEventHandler2, value);
					logNewEntryEventHandler = Interlocked.CompareExchange<LogNewEntryEventHandler>(ref this.d, value2, logNewEntryEventHandler2);
				}
				while (logNewEntryEventHandler != logNewEntryEventHandler2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060009A6 RID: 2470 RVA: 0x0002D108 File Offset: 0x0002C108
		// (remove) Token: 0x060009A7 RID: 2471 RVA: 0x0002D140 File Offset: 0x0002C140
		public event DataTransferEventHandler DataReceived
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.e;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.e, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.e;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.e, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060009A8 RID: 2472 RVA: 0x0002D178 File Offset: 0x0002C178
		// (remove) Token: 0x060009A9 RID: 2473 RVA: 0x0002D1B0 File Offset: 0x0002C1B0
		public event DataTransferEventHandler DataSent
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.f;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.f, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.f;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.f, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x04000798 RID: 1944
		[CompilerGenerated]
		private EventHandler a;

		// Token: 0x04000799 RID: 1945
		private ISite b;

		// Token: 0x0400079A RID: 1946
		[CompilerGenerated]
		private ErrorEventHandler c;

		// Token: 0x0400079B RID: 1947
		[CompilerGenerated]
		private LogNewEntryEventHandler d;

		// Token: 0x0400079C RID: 1948
		[CompilerGenerated]
		private DataTransferEventHandler e;

		// Token: 0x0400079D RID: 1949
		[CompilerGenerated]
		private DataTransferEventHandler f;

		// Token: 0x0400079E RID: 1950
		private n g;

		// Token: 0x0400079F RID: 1951
		private bool h;
	}
}

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using a;
using a.d;
using MailBee.AddressCheck;
using MailBee.DnsMX;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000140 RID: 320
	public class Smtp : IComponent
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x0002F334 File Offset: 0x0002E334
		public Smtp() : this(null)
		{
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002F33D File Offset: 0x0002E33D
		public Smtp(string licenseKey) : this(licenseKey, false)
		{
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002F348 File Offset: 0x0002E348
		internal Smtp(string A_0, bool A_1)
		{
			this.ad = new global::a.d.n(this, null, null);
			if (!A_1)
			{
				Smtp.a(A_0);
			}
			this.b = null;
			this.ae = false;
			this.a = null;
			this.c = null;
			this.d = null;
			this.e = null;
			this.f = null;
			this.g = null;
			this.h = null;
			this.i = null;
			this.j = null;
			this.k = null;
			this.l = null;
			this.m = null;
			this.n = null;
			this.o = null;
			this.p = null;
			this.q = null;
			this.r = null;
			this.s = null;
			this.t = null;
			this.u = null;
			this.v = null;
			this.w = null;
			this.x = null;
			this.z = null;
			this.aa = null;
			this.ab = null;
			this.ac = null;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002F43D File Offset: 0x0002E43D
		public Task<bool> AddAttachmentAsync(string filename)
		{
			return this.ad.p().Attachments.AddAsync(filename);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002F455 File Offset: 0x0002E455
		public Task<bool> ConnectAsync()
		{
			return this.ad.v();
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002F462 File Offset: 0x0002E462
		public Task<bool> DisconnectAsync()
		{
			return this.ad.my();
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002F46F File Offset: 0x0002E46F
		public Task<bool> HelloAsync()
		{
			return this.ad.x();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002F47C File Offset: 0x0002E47C
		public Task<bool> LoginAsync()
		{
			return this.ad.i();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002F489 File Offset: 0x0002E489
		public Task<bool> AuthPopBeforeSmtpAsync(string pop3ServerName, int pop3ServerPort, string pop3AccountName, string pop3Password)
		{
			return this.ad.c(pop3ServerName, pop3ServerPort, pop3AccountName, pop3Password);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002F49B File Offset: 0x0002E49B
		public Task<bool> StartTlsAsync()
		{
			return this.ad.m0();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002F4A8 File Offset: 0x0002E4A8
		public Task<bool> NoopAsync()
		{
			return this.ad.mz();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002F4B5 File Offset: 0x0002E4B5
		public Task<bool> ExecuteCustomCommandAsync(string commandString)
		{
			return this.ad.j(commandString);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002F4C3 File Offset: 0x0002E4C3
		public Task<string[]> GetMXHostsAsync(string domain)
		{
			return this.ad.i(domain);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002F4D1 File Offset: 0x0002E4D1
		public Task<string[]> GetPtrDataAsync(string ipString)
		{
			return this.ad.m(ipString);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002F4DF File Offset: 0x0002E4DF
		public Task<string[]> GetTxtDataAsync(string domain)
		{
			return this.ad.k(domain);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002F4ED File Offset: 0x0002E4ED
		public Task<bool> RelayFromEmlFileAsync(string filename, string senderEmail, EmailAddressCollection recipients)
		{
			return this.ad.d(filename, senderEmail, recipients);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002F4FD File Offset: 0x0002E4FD
		public Task<bool> RelayFromEmlFileAsync(string filename, string senderEmail, string recipientEmails)
		{
			return this.ad.d(filename, senderEmail, EmailAddressCollection.a(recipientEmails, null));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002F513 File Offset: 0x0002E513
		public Task<bool> SendAsync(string senderEmail, EmailAddressCollection recipients)
		{
			return this.ad.c(senderEmail, recipients);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002F522 File Offset: 0x0002E522
		public Task<bool> SendAsync(string senderEmail, string recipientEmails)
		{
			return this.ad.c(senderEmail, (recipientEmails == null) ? null : EmailAddressCollection.a(recipientEmails, null));
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002F53D File Offset: 0x0002E53D
		public Task<bool> SendAsync()
		{
			return this.ad.ak();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002F54A File Offset: 0x0002E54A
		public Task<bool> SendJobsAsync()
		{
			return this.ad.ab();
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002F557 File Offset: 0x0002E557
		public Task<bool> SendMailMergeAsync(string senderEmailPattern, EmailAddressCollection recipientsPattern, DataTable mergeTable)
		{
			this.ad.a(null, senderEmailPattern, recipientsPattern, mergeTable, null);
			return this.ad.ab();
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002F574 File Offset: 0x0002E574
		public Task<bool> SendMailMergeAsync(string senderEmailPattern, EmailAddressCollection recipientsPattern, IDataReader mergeDataReader)
		{
			this.ad.a(null, senderEmailPattern, recipientsPattern, null, mergeDataReader);
			return this.ad.ab();
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002F591 File Offset: 0x0002E591
		public Task<string> SubmitToPickupFolderAsync(string pickupFolderName, bool doubleFirstDotAtLine)
		{
			return this.SubmitToPickupFolderAsync(pickupFolderName, null, null, null, doubleFirstDotAtLine);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002F59E File Offset: 0x0002E59E
		public Task<string> SubmitToPickupFolderAsync(string pickupFolderName, string filename, string senderEmail, string recipientEmails, bool doubleFirstDotAtLine)
		{
			return this.SubmitToPickupFolderAsync(pickupFolderName, filename, senderEmail, (recipientEmails == null) ? null : EmailAddressCollection.a(recipientEmails, null), doubleFirstDotAtLine);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002F5BA File Offset: 0x0002E5BA
		public Task<string> SubmitToPickupFolderAsync(string pickupFolderName, string filename, string senderEmail, EmailAddressCollection recipients, bool doubleFirstDotAtLine)
		{
			return this.ad.b(pickupFolderName, filename, senderEmail, recipients, doubleFirstDotAtLine);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002F5CE File Offset: 0x0002E5CE
		public Task<bool> SubmitJobsToPickupFolderAsync(string pickupFolderName, bool doubleFirstDotAtLine)
		{
			return this.ad.b(pickupFolderName, doubleFirstDotAtLine);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002F5DD File Offset: 0x0002E5DD
		public Task<TestSendResult> TestSendAsync(SendFailureThreshold failureThreshold)
		{
			return this.ad.a(failureThreshold);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002F5EB File Offset: 0x0002E5EB
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0002F5F7 File Offset: 0x0002E5F7
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(Smtp));
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0002F60E File Offset: 0x0002E60E
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002F61A File Offset: 0x0002E61A
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0002F62C File Offset: 0x0002E62C
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.ad.bp().d();
			}
			set
			{
				this.ad.bp().a(value);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0002F63F File Offset: 0x0002E63F
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002F646 File Offset: 0x0002E646
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002F64F File Offset: 0x0002E64F
		protected virtual void Dispose(bool disposing)
		{
			if (!this.ae)
			{
				if (disposing)
				{
					this.ad.bo();
					if (this.a != null)
					{
						this.a(this, EventArgs.Empty);
					}
				}
				this.ae = true;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000A71 RID: 2673 RVA: 0x0002F688 File Offset: 0x0002E688
		// (remove) Token: 0x06000A72 RID: 2674 RVA: 0x0002F6C0 File Offset: 0x0002E6C0
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002F6F5 File Offset: 0x0002E6F5
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0002F6FD File Offset: 0x0002E6FD
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

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002F706 File Offset: 0x0002E706
		internal bool aa()
		{
			return this.c != null;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002F711 File Offset: 0x0002E711
		protected internal void OnErrorOccurred(ErrorEventArgs args)
		{
			this.ad.bp().a(this.c, this, args);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002F72B File Offset: 0x0002E72B
		internal bool g()
		{
			return this.d != null;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002F736 File Offset: 0x0002E736
		protected internal void OnLogNewEntry(LogNewEntryEventArgs args)
		{
			this.ad.bp().a(this.d, this, args);
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0002F750 File Offset: 0x0002E750
		public bool IsBusy
		{
			get
			{
				return this.ad.bc();
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002F75D File Offset: 0x0002E75D
		public void Abort()
		{
			this.ad.bd();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002F76A File Offset: 0x0002E76A
		[Obsolete("This method is obsolete in .NET 4.5+.")]
		public void Wait()
		{
			this.ad.bg();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002F777 File Offset: 0x0002E777
		[Obsolete("This method is obsolete in .NET 4.5+.")]
		public bool Wait(int timeoutInterval)
		{
			return this.ad.g(timeoutInterval);
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002F785 File Offset: 0x0002E785
		public bool IsAborted
		{
			get
			{
				return this.ad.bf();
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002F792 File Offset: 0x0002E792
		public string GetErrorDescription()
		{
			return this.ad.l1();
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002F79F File Offset: 0x0002E79F
		public int LastResult
		{
			get
			{
				return this.ad.l2();
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002F7AC File Offset: 0x0002E7AC
		public Logger Log
		{
			get
			{
				return this.ad.bi();
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002F7B9 File Offset: 0x0002E7B9
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0002F7C6 File Offset: 0x0002E7C6
		public bool RaiseEvents
		{
			get
			{
				return this.ad.bq();
			}
			set
			{
				this.ad.k(value);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002F7D4 File Offset: 0x0002E7D4
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002F7E1 File Offset: 0x0002E7E1
		public bool RaiseEventsViaMessageLoop
		{
			get
			{
				return this.ad.bb();
			}
			set
			{
				this.ad.j(value);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002F7EF File Offset: 0x0002E7EF
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002F7FC File Offset: 0x0002E7FC
		public Encoding RequestEncoding
		{
			get
			{
				return this.ad.bk();
			}
			set
			{
				this.ad.lt(value);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002F80A File Offset: 0x0002E80A
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002F817 File Offset: 0x0002E817
		public Encoding ResponseEncoding
		{
			get
			{
				return this.ad.bm();
			}
			set
			{
				this.ad.lu(value);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002F825 File Offset: 0x0002E825
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002F832 File Offset: 0x0002E832
		public bool ThrowExceptions
		{
			get
			{
				return this.ad.be();
			}
			set
			{
				this.ad.ls(value);
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002F840 File Offset: 0x0002E840
		internal bool a()
		{
			return this.e != null;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002F84B File Offset: 0x0002E84B
		protected internal void OnDataReceived(DataTransferEventArgs args)
		{
			this.ad.bp().a(this.e, this, args);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002F865 File Offset: 0x0002E865
		internal bool b()
		{
			return this.f != null;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002F870 File Offset: 0x0002E870
		protected internal void OnDataSent(DataTransferEventArgs args)
		{
			this.ad.bp().a(this.f, this, args);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002F88A File Offset: 0x0002E88A
		public Socket GetSocket()
		{
			return this.ad.lv();
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002F897 File Offset: 0x0002E897
		public Stream GetStream()
		{
			return this.ad.ba();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002F8A4 File Offset: 0x0002E8A4
		public int GetSocketError()
		{
			return this.ad.lw();
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002F8B1 File Offset: 0x0002E8B1
		internal bool o()
		{
			return this.g != null;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002F8BC File Offset: 0x0002E8BC
		protected internal void OnLowLevelDataReceived(DataTransferEventArgs args)
		{
			this.ad.bp().a(this.g, this, args);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002F8D6 File Offset: 0x0002E8D6
		internal bool y()
		{
			return this.h != null;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002F8E1 File Offset: 0x0002E8E1
		protected internal void OnLowLevelDataSent(DataTransferEventArgs args)
		{
			this.ad.bp().a(this.h, this, args);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002F8FB File Offset: 0x0002E8FB
		internal bool i()
		{
			return this.i != null;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002F906 File Offset: 0x0002E906
		protected internal void OnHostResolved(HostResolvedEventArgs args)
		{
			this.ad.bp().a(this.i, this, args);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002F920 File Offset: 0x0002E920
		internal bool h()
		{
			return this.j != null;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002F92B File Offset: 0x0002E92B
		protected internal void OnSocketCreating(SocketCreatingEventArgs args)
		{
			this.ad.bp().a(this.j, this, args);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002F945 File Offset: 0x0002E945
		internal bool x()
		{
			return this.k != null;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002F950 File Offset: 0x0002E950
		protected internal void OnSocketConnected(SocketConnectedEventArgs args)
		{
			this.ad.bp().a(this.k, this, args);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002F96A File Offset: 0x0002E96A
		internal bool p()
		{
			return this.l != null;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002F975 File Offset: 0x0002E975
		protected internal void OnConnected(ConnectedEventArgs args)
		{
			this.ad.bp().a(this.l, this, args);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002F98F File Offset: 0x0002E98F
		internal bool d()
		{
			return this.m != null;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002F99A File Offset: 0x0002E99A
		protected internal void OnDisconnected(DisconnectedEventArgs args)
		{
			this.ad.bp().a(this.m, this, args);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002F9B4 File Offset: 0x0002E9B4
		internal bool s()
		{
			return this.n != null;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002F9BF File Offset: 0x0002E9BF
		protected internal void OnTlsStarted(TlsStartedEventArgs args)
		{
			this.ad.bp().a(this.n, this, args);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002F9D9 File Offset: 0x0002E9D9
		internal bool u()
		{
			return this.o != null;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002F9E4 File Offset: 0x0002E9E4
		protected internal void OnLoggedIn(LoggedInEventArgs args)
		{
			this.ad.bp().a(this.o, this, args);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002F9FE File Offset: 0x0002E9FE
		public bool Disconnect()
		{
			return this.ad.lo(true);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002FA0C File Offset: 0x0002EA0C
		[Obsolete("This method is obsolete in .NET 4.5+. Use DisconnectAsync instead.")]
		public IAsyncResult BeginDisconnect(AsyncCallback callback, object state)
		{
			return this.ad.lp(callback, state);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002FA1B File Offset: 0x0002EA1B
		public bool EndDisconnect()
		{
			return this.ad.az();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002FA28 File Offset: 0x0002EA28
		public bool Noop()
		{
			return this.ad.lq(true);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002FA36 File Offset: 0x0002EA36
		public void ResetState()
		{
			this.ad.cb();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002FA43 File Offset: 0x0002EA43
		public StringDictionary GetExtensions()
		{
			return this.ad.ke();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002FA50 File Offset: 0x0002EA50
		public string GetExtension(string name)
		{
			return this.ad.kf(name);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002FA5E File Offset: 0x0002EA5E
		public string GetExtensionValue(string name)
		{
			return this.ad.kg(name);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002FA6C File Offset: 0x0002EA6C
		public string GetServerResponse()
		{
			return this.ad.l0();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002FA79 File Offset: 0x0002EA79
		public AuthenticationMethods GetSupportedAuthMethods()
		{
			return this.ad.kh();
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002FA86 File Offset: 0x0002EA86
		public bool IsConnected
		{
			get
			{
				return this.ad.lx();
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002FA93 File Offset: 0x0002EA93
		public bool IsSslConnection
		{
			get
			{
				return this.ad.ly();
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002FAA0 File Offset: 0x0002EAA0
		public bool IsLoggedIn
		{
			get
			{
				return this.ad.lz();
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002FAAD File Offset: 0x0002EAAD
		internal bool q()
		{
			return this.p != null;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002FAB8 File Offset: 0x0002EAB8
		protected internal void OnSendingMessage(SmtpSendingMessageEventArgs args)
		{
			this.ad.bp().a(this.p, this, args);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002FAD2 File Offset: 0x0002EAD2
		internal bool n()
		{
			return this.q != null;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002FADD File Offset: 0x0002EADD
		protected internal void OnMessageSenderSubmitted(SmtpMessageSenderSubmittedEventArgs args)
		{
			this.ad.bp().a(this.q, this, args);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002FAF7 File Offset: 0x0002EAF7
		internal bool e()
		{
			return this.r != null;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002FB02 File Offset: 0x0002EB02
		protected internal void OnMessageRecipientSubmitted(SmtpMessageRecipientSubmittedEventArgs args)
		{
			this.ad.bp().a(this.r, this, args);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002FB1C File Offset: 0x0002EB1C
		internal bool f()
		{
			return this.s != null;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002FB27 File Offset: 0x0002EB27
		protected internal void OnMessageDataChunkSent(SmtpMessageDataChunkSentEventArgs args)
		{
			this.ad.bp().a(this.s, this, args);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002FB41 File Offset: 0x0002EB41
		internal bool k()
		{
			return this.t != null;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002FB4C File Offset: 0x0002EB4C
		protected internal void OnMessageSubmittedToServer(SmtpMessageSubmittedToServerEventArgs args)
		{
			this.ad.bp().a(this.t, this, args);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002FB66 File Offset: 0x0002EB66
		internal bool r()
		{
			return this.u != null;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002FB71 File Offset: 0x0002EB71
		protected internal void OnMessageSent(SmtpMessageSentEventArgs args)
		{
			this.ad.bp().a(this.u, this, args);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002FB8B File Offset: 0x0002EB8B
		internal bool v()
		{
			return this.v != null;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002FB96 File Offset: 0x0002EB96
		protected internal void OnMessageNotSent(SmtpMessageNotSentEventArgs args)
		{
			this.ad.bp().a(this.v, this, args);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002FBB0 File Offset: 0x0002EBB0
		internal bool c()
		{
			return this.w != null;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002FBBB File Offset: 0x0002EBBB
		protected internal void OnTransientErrorOccurred(SmtpTransientErrorOccurredEventArgs args)
		{
			this.ad.bp().a(this.w, this, args);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002FBD5 File Offset: 0x0002EBD5
		internal bool w()
		{
			return this.x != null;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002FBE0 File Offset: 0x0002EBE0
		protected internal void OnMergingMessage(SmtpMergingMessageEventArgs args)
		{
			this.ad.bp().a(this.x, this, args);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002FBFA File Offset: 0x0002EBFA
		internal bool j()
		{
			return this.z != null;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002FC05 File Offset: 0x0002EC05
		protected internal void OnSubmittingMessageToPickupFolder(SmtpSubmittingMessageToPickupFolderEventArgs args)
		{
			this.ad.bp().a(this.z, this, args);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002FC1F File Offset: 0x0002EC1F
		internal bool m()
		{
			return this.aa != null;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002FC2A File Offset: 0x0002EC2A
		protected internal void OnMessageSubmittedToPickupFolder(SmtpMessageSubmittedToPickupFolderEventArgs args)
		{
			this.ad.bp().a(this.aa, this, args);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002FC44 File Offset: 0x0002EC44
		internal bool l()
		{
			return this.y != null;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002FC4F File Offset: 0x0002EC4F
		protected internal void OnFinishingJob(SmtpFinishingJobEventArgs args)
		{
			this.ad.bp().a(this.y, this, args);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002FC69 File Offset: 0x0002EC69
		internal bool t()
		{
			return this.ab != null;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002FC74 File Offset: 0x0002EC74
		protected internal void OnMessageMXLookupDone(SmtpMessageMXLookupDoneEventArgs args)
		{
			this.ad.bp().a(this.ab, this, args);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002FC8E File Offset: 0x0002EC8E
		internal bool z()
		{
			return this.ac != null;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002FC99 File Offset: 0x0002EC99
		protected internal void OnMessageDirectSendDone(SmtpMessageDirectSendDoneEventArgs args)
		{
			this.ad.bp().a(this.ac, this, args);
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002FCB3 File Offset: 0x0002ECB3
		public bool IsSmtpContext
		{
			get
			{
				return this.ad.am();
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002FCC0 File Offset: 0x0002ECC0
		public bool AuthPopBeforeSmtp(string pop3ServerName, int pop3ServerPort, string pop3AccountName, string pop3Password)
		{
			return this.ad.a(true, pop3ServerName, pop3ServerPort, pop3AccountName, pop3Password);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002FCD3 File Offset: 0x0002ECD3
		[Obsolete("This method is obsolete in .NET 4.5+. Use AuthPopBeforeSmtpAsync instead.")]
		public IAsyncResult BeginAuthPopBeforeSmtp(string pop3ServerName, int pop3ServerPort, string pop3AccountName, string pop3Password, AsyncCallback callback, object state)
		{
			return this.ad.a(pop3ServerName, pop3ServerPort, pop3AccountName, pop3Password, callback, state);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002FCE9 File Offset: 0x0002ECE9
		public bool EndAuthPopBeforeSmtp()
		{
			return this.ad.at();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002FCF6 File Offset: 0x0002ECF6
		public bool Connect()
		{
			return this.ad.a(true);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002FD04 File Offset: 0x0002ED04
		[Obsolete("This method is obsolete in .NET 4.5+. Use ConnectAsync instead.")]
		public IAsyncResult BeginConnect(AsyncCallback callback, object state)
		{
			return this.ad.a(callback, state);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002FD13 File Offset: 0x0002ED13
		public bool EndConnect()
		{
			return this.ad.u();
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002FD20 File Offset: 0x0002ED20
		public bool Hello()
		{
			return this.ad.f(true);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002FD2E File Offset: 0x0002ED2E
		[Obsolete("This method is obsolete in .NET 4.5+. Use HelloAsync instead.")]
		public IAsyncResult BeginHello(AsyncCallback callback, object state)
		{
			return this.ad.b(callback, state);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002FD3D File Offset: 0x0002ED3D
		public bool EndHello()
		{
			return this.ad.aj();
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002FD4A File Offset: 0x0002ED4A
		public bool StartTls()
		{
			return this.ad.lr(true);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002FD58 File Offset: 0x0002ED58
		[Obsolete("This method is obsolete in .NET 4.5+. Use StartTlsAsync instead.")]
		public IAsyncResult BeginStartTls(AsyncCallback callback, object state)
		{
			return this.ad.e(callback, state);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002FD67 File Offset: 0x0002ED67
		public bool EndStartTls()
		{
			return this.ad.a8();
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002FD74 File Offset: 0x0002ED74
		public bool Login()
		{
			return this.ad.c(true);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002FD82 File Offset: 0x0002ED82
		[Obsolete("This method is obsolete in .NET 4.5+. Use LoginAsync instead.")]
		public IAsyncResult BeginLogin(AsyncCallback callback, object state)
		{
			return this.ad.c(callback, state);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002FD91 File Offset: 0x0002ED91
		public bool EndLogin()
		{
			return this.ad.ac();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002FD9E File Offset: 0x0002ED9E
		public bool ExecuteCustomCommand(string commandString)
		{
			return this.ad.c(true, commandString);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002FDAD File Offset: 0x0002EDAD
		[Obsolete("This method is obsolete in .NET 4.5+. Use ExecuteCustomCommandAsync instead.")]
		public IAsyncResult BeginExecuteCustomCommand(string commandString, AsyncCallback callback, object state)
		{
			return this.ad.a(commandString, callback, state);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002FDBD File Offset: 0x0002EDBD
		public bool EndExecuteCustomCommand()
		{
			return this.ad.ap();
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002FDCA File Offset: 0x0002EDCA
		public object JobsSyncRoot
		{
			get
			{
				return this.ad.q();
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002FDD7 File Offset: 0x0002EDD7
		public SendMailJobCollection JobsPending
		{
			get
			{
				return this.ad.ah();
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002FDE4 File Offset: 0x0002EDE4
		public SendMailJobCollection JobsRunning
		{
			get
			{
				return this.ad.ax();
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002FDF1 File Offset: 0x0002EDF1
		public SendMailJobCollection JobsFailed
		{
			get
			{
				return this.ad.ar();
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002FDFE File Offset: 0x0002EDFE
		public SendMailJobCollection JobsSuccessful
		{
			get
			{
				return this.ad.s();
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0002FE0B File Offset: 0x0002EE0B
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0002FE18 File Offset: 0x0002EE18
		public int MaxThreadCount
		{
			get
			{
				return this.ad.m();
			}
			set
			{
				this.ad.a(value);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0002FE26 File Offset: 0x0002EE26
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0002FE33 File Offset: 0x0002EE33
		public Smtp8bitDataConversion Conversion8BitTo7bit
		{
			get
			{
				return this.ad.ae();
			}
			set
			{
				this.ad.a(value);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002FE41 File Offset: 0x0002EE41
		public DeliveryNotificationOptions DeliveryNotification
		{
			get
			{
				return this.ad.o();
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002FE4E File Offset: 0x0002EE4E
		public DirectSendServerConfig DirectSendDefaults
		{
			get
			{
				return this.ad.aa();
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002FE5B File Offset: 0x0002EE5B
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0002FE68 File Offset: 0x0002EE68
		public MailMessage Message
		{
			get
			{
				return this.ad.p();
			}
			set
			{
				this.ad.a(value);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002FE76 File Offset: 0x0002EE76
		public EmailAddressCollection GetAcceptedRecipients()
		{
			return this.ad.k();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002FE83 File Offset: 0x0002EE83
		public EmailAddressCollection GetRefusedRecipients()
		{
			return this.ad.w();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002FE90 File Offset: 0x0002EE90
		public int GetServerResponseCode()
		{
			return this.ad.au();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002FE9D File Offset: 0x0002EE9D
		public int GetMaxMessageSize()
		{
			return this.ad.t();
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002FEAA File Offset: 0x0002EEAA
		public DnsServerCollection DnsServers
		{
			get
			{
				return this.ad.aq();
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002FEB7 File Offset: 0x0002EEB7
		public SmtpServerCollection SmtpServers
		{
			get
			{
				return this.ad.av();
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002FEC4 File Offset: 0x0002EEC4
		public int GetCurrentSmtpServerIndex()
		{
			return this.ad.ag();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002FED1 File Offset: 0x0002EED1
		public void ResetMessage()
		{
			this.ad.al();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002FEDE File Offset: 0x0002EEDE
		public bool RelayFromEmlFile(string filename, string senderEmail, EmailAddressCollection recipients)
		{
			return this.ad.a(true, filename, senderEmail, recipients);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002FEEF File Offset: 0x0002EEEF
		public bool RelayFromEmlFile(string filename, string senderEmail, string recipientEmails)
		{
			return this.ad.a(true, filename, senderEmail, EmailAddressCollection.a(recipientEmails, null));
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002FF06 File Offset: 0x0002EF06
		[Obsolete("This method is obsolete in .NET 4.5+. Use RelayFromEmlFileAsync instead.")]
		public IAsyncResult BeginRelayFromEmlFile(string filename, string senderEmail, EmailAddressCollection recipients, AsyncCallback callback, object state)
		{
			return this.ad.a(filename, senderEmail, recipients, callback, state);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002FF1A File Offset: 0x0002EF1A
		public bool EndRelayFromEmlFile()
		{
			return this.ad.ay();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002FF27 File Offset: 0x0002EF27
		public bool Send()
		{
			return this.ad.d(true);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002FF35 File Offset: 0x0002EF35
		public bool Send(string senderEmail, EmailAddressCollection recipients)
		{
			return this.ad.a(true, senderEmail, recipients);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002FF45 File Offset: 0x0002EF45
		public bool Send(string senderEmail, string recipientEmails)
		{
			return this.ad.a(true, senderEmail, (recipientEmails == null) ? null : EmailAddressCollection.a(recipientEmails, null));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002FF61 File Offset: 0x0002EF61
		[Obsolete("This method is obsolete in .NET 4.5+. Use SendAsync instead.")]
		public IAsyncResult BeginSend(string senderEmail, EmailAddressCollection recipients, AsyncCallback callback, object state)
		{
			return this.ad.a(senderEmail, recipients, callback, state);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002FF73 File Offset: 0x0002EF73
		public bool EndSend()
		{
			return this.ad.aw();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002FF80 File Offset: 0x0002EF80
		public string[] GetMXHosts(string domain)
		{
			return this.ad.d(true, domain);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002FF8F File Offset: 0x0002EF8F
		public string[] GetTxtData(string domain)
		{
			return this.ad.b(true, domain);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002FF9E File Offset: 0x0002EF9E
		public string[] GetPtrData(string ipString)
		{
			return this.ad.a(true, ipString);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002FFAD File Offset: 0x0002EFAD
		public void AddJob(string tag, string senderEmail, EmailAddressCollection recipients)
		{
			this.ad.c(tag, senderEmail, recipients);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002FFBD File Offset: 0x0002EFBD
		public void AddJob(string tag, MailMessage msg, string senderEmail, EmailAddressCollection recipients)
		{
			this.ad.a(tag, msg, senderEmail, recipients);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002FFCF File Offset: 0x0002EFCF
		public void AddJob(string tag, string msgFilename, bool preferXSenderXReceiver, string senderEmail, EmailAddressCollection recipients)
		{
			this.ad.a(tag, msgFilename, preferXSenderXReceiver, senderEmail, recipients);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002FFE3 File Offset: 0x0002EFE3
		public void AddJob(string tag, string senderEmailPattern, EmailAddressCollection recipientsPattern, DataTable mergeTable)
		{
			this.ad.a(tag, senderEmailPattern, recipientsPattern, mergeTable, null);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002FFF6 File Offset: 0x0002EFF6
		public void AddJob(string tag, string senderEmailPattern, EmailAddressCollection recipientsPattern, IDataReader mergeDataReader)
		{
			this.ad.a(tag, senderEmailPattern, recipientsPattern, null, mergeDataReader);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0003000C File Offset: 0x0002F00C
		public void AddJob(string tag, string senderEmailPattern, EmailAddressCollection recipientsPattern, DataTable mergeTable, object mergeRowIndices, bool keepProducedJobs, bool keepMergedData)
		{
			this.ad.a(tag, senderEmailPattern, recipientsPattern, mergeTable, mergeRowIndices, null, true, true, true, AddressValidationLevel.OK, null, -1, null, keepProducedJobs, keepMergedData);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00030038 File Offset: 0x0002F038
		public void AddJob(string tag, string senderEmailPattern, EmailAddressCollection recipientsPattern, IDataReader mergeDataReader, bool keepProducedJobs, bool keepMergedData)
		{
			this.ad.a(tag, senderEmailPattern, recipientsPattern, null, null, mergeDataReader, true, true, true, AddressValidationLevel.OK, null, -1, null, keepProducedJobs, keepMergedData);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00030062 File Offset: 0x0002F062
		public void RetryFailedJobs()
		{
			this.ad.af();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003006F File Offset: 0x0002F06F
		public bool SendJobs()
		{
			return this.ad.y();
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0003007C File Offset: 0x0002F07C
		[Obsolete("This method is obsolete in .NET 4.5+. Use SendJobsAsync instead.")]
		public IAsyncResult BeginSendJobs(AsyncCallback callback, object state)
		{
			return this.ad.d(callback, state);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0003008B File Offset: 0x0002F08B
		public bool EndSendJobs()
		{
			return this.ad.@as();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00030098 File Offset: 0x0002F098
		public void StopJobs()
		{
			this.ad.r();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000300A5 File Offset: 0x0002F0A5
		public bool SendMailMerge(string senderEmailPattern, EmailAddressCollection recipientsPattern, DataTable mergeTable)
		{
			this.ad.a(null, senderEmailPattern, recipientsPattern, mergeTable, null);
			return this.ad.y();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000300C2 File Offset: 0x0002F0C2
		public bool SendMailMerge(string senderEmailPattern, EmailAddressCollection recipientsPattern, IDataReader mergeDataReader)
		{
			this.ad.a(null, senderEmailPattern, recipientsPattern, null, mergeDataReader);
			return this.ad.y();
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x000300DF File Offset: 0x0002F0DF
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x000300EC File Offset: 0x0002F0EC
		public bool StopJobsOnError
		{
			get
			{
				return this.ad.ad();
			}
			set
			{
				this.ad.b(value);
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000300FA File Offset: 0x0002F0FA
		public string SubmitToPickupFolder(string pickupFolderName, string filename, string senderEmail, EmailAddressCollection recipients, bool doubleFirstDotAtLine)
		{
			return this.ad.c(pickupFolderName, filename, senderEmail, recipients, doubleFirstDotAtLine);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003010E File Offset: 0x0002F10E
		public string SubmitToPickupFolder(string pickupFolderName, string filename, string senderEmail, string recipientEmails, bool doubleFirstDotAtLine)
		{
			return this.SubmitToPickupFolder(pickupFolderName, filename, senderEmail, (recipientEmails == null) ? null : EmailAddressCollection.a(recipientEmails, null), doubleFirstDotAtLine);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0003012A File Offset: 0x0002F12A
		public string SubmitToPickupFolder(string pickupFolderName, bool doubleFirstDotAtLine)
		{
			return this.SubmitToPickupFolder(pickupFolderName, null, null, null, doubleFirstDotAtLine);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00030137 File Offset: 0x0002F137
		public bool SubmitJobsToPickupFolder(string pickupFolderName, bool doubleFirstDotAtLine)
		{
			return this.ad.a(pickupFolderName, doubleFirstDotAtLine);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00030146 File Offset: 0x0002F146
		public TestSendResult TestSend(SendFailureThreshold failureThreshold)
		{
			return this.ad.a(true, failureThreshold);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00030155 File Offset: 0x0002F155
		public static bool ValidateEmailAddressSyntax(string email)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			return Regex.IsMatch(email, "^(([\\w]+['\\.\\-+])+[\\w]+|([\\w]+))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\\w-]*\\.)+[a-zA-Z]{2,9})$");
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0003016D File Offset: 0x0002F16D
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0003017F File Offset: 0x0002F17F
		public EmailAddress From
		{
			get
			{
				return this.ad.p().From;
			}
			set
			{
				this.ad.p().From = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00030192 File Offset: 0x0002F192
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x000301A4 File Offset: 0x0002F1A4
		public EmailAddressCollection To
		{
			get
			{
				return this.ad.p().To;
			}
			set
			{
				this.ad.p().To = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000301B7 File Offset: 0x0002F1B7
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x000301C9 File Offset: 0x0002F1C9
		public EmailAddressCollection Cc
		{
			get
			{
				return this.ad.p().Cc;
			}
			set
			{
				this.ad.p().Cc = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000301DC File Offset: 0x0002F1DC
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x000301EE File Offset: 0x0002F1EE
		public EmailAddressCollection Bcc
		{
			get
			{
				return this.ad.p().Bcc;
			}
			set
			{
				this.ad.p().Bcc = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00030201 File Offset: 0x0002F201
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00030213 File Offset: 0x0002F213
		public EmailAddressCollection ReplyTo
		{
			get
			{
				return this.ad.p().ReplyTo;
			}
			set
			{
				this.ad.p().ReplyTo = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00030226 File Offset: 0x0002F226
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00030238 File Offset: 0x0002F238
		public string Subject
		{
			get
			{
				return this.ad.p().Subject;
			}
			set
			{
				this.ad.p().Subject = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0003024B File Offset: 0x0002F24B
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0003025D File Offset: 0x0002F25D
		public string Charset
		{
			get
			{
				return this.ad.p().Charset;
			}
			set
			{
				this.ad.p().Charset = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00030270 File Offset: 0x0002F270
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00030282 File Offset: 0x0002F282
		public string BodyPlainText
		{
			get
			{
				return this.ad.p().BodyPlainText;
			}
			set
			{
				this.ad.p().BodyPlainText = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00030295 File Offset: 0x0002F295
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x000302A7 File Offset: 0x0002F2A7
		public string BodyHtmlText
		{
			get
			{
				return this.ad.p().BodyHtmlText;
			}
			set
			{
				this.ad.p().BodyHtmlText = value;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000302BA File Offset: 0x0002F2BA
		public bool AddAttachment(string filename)
		{
			return this.ad.p().Attachments.Add(filename);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000302D2 File Offset: 0x0002F2D2
		private static void a(string A_0)
		{
			Global.a(typeof(Smtp), A_0);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000302E4 File Offset: 0x0002F2E4
		public static void QuickSend(MailMessage message)
		{
			Smtp.a(null);
			ae a_ = new ae();
			global::a.d.f f = new global::a.d.f(null, null, new Logger(null), 0);
			f.a(new DirectSendServerConfig());
			f.a(new DnsServerCollection());
			f.a(new SmtpServerCollection());
			if (!f.d().Autodetect())
			{
				throw new MailBeeSystemSettingsException(214);
			}
			EmailAddressCollection allRecipients = message.GetAllRecipients();
			string text = null;
			EmailAddressCollection emailAddressCollection = global::a.d.a.a(message, ref text);
			f.a(message, message.From.Email, allRecipients, null, Smtp8bitDataConversion.DoNothing, true, true, true, SendFailureThreshold.Default, -1, a_, false, null, null, null);
			if (emailAddressCollection != null)
			{
				message.Bcc = emailAddressCollection;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00030380 File Offset: 0x0002F380
		public static void QuickSend(string from, string to, string subject, string plainTextBody, string htmlBody, string charset, string attachmentFilename)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.From.AsString = from;
			mailMessage.To.AsString = to;
			mailMessage.Subject = subject;
			if (plainTextBody != null)
			{
				mailMessage.BodyParts.Plain.Text = plainTextBody;
			}
			if (htmlBody != null)
			{
				mailMessage.BodyParts.Html.Text = htmlBody;
			}
			if (charset != null)
			{
				mailMessage.Charset = charset;
			}
			if (attachmentFilename != null)
			{
				mailMessage.Attachments.Add(attachmentFilename);
			}
			Smtp.QuickSend(mailMessage);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00030400 File Offset: 0x0002F400
		public static void QuickSend(string from, string to, string subject, string body)
		{
			Smtp.QuickSend(from, to, subject, body, null, null, null);
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000B2E RID: 2862 RVA: 0x00030410 File Offset: 0x0002F410
		// (remove) Token: 0x06000B2F RID: 2863 RVA: 0x00030448 File Offset: 0x0002F448
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

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000B30 RID: 2864 RVA: 0x00030480 File Offset: 0x0002F480
		// (remove) Token: 0x06000B31 RID: 2865 RVA: 0x000304B8 File Offset: 0x0002F4B8
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000B32 RID: 2866 RVA: 0x000304F0 File Offset: 0x0002F4F0
		// (remove) Token: 0x06000B33 RID: 2867 RVA: 0x00030528 File Offset: 0x0002F528
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

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000B34 RID: 2868 RVA: 0x00030560 File Offset: 0x0002F560
		// (remove) Token: 0x06000B35 RID: 2869 RVA: 0x00030598 File Offset: 0x0002F598
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

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000B36 RID: 2870 RVA: 0x000305D0 File Offset: 0x0002F5D0
		// (remove) Token: 0x06000B37 RID: 2871 RVA: 0x00030608 File Offset: 0x0002F608
		public event DataTransferEventHandler LowLevelDataReceived
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.g;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.g, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.g;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.g, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000B38 RID: 2872 RVA: 0x00030640 File Offset: 0x0002F640
		// (remove) Token: 0x06000B39 RID: 2873 RVA: 0x00030678 File Offset: 0x0002F678
		public event DataTransferEventHandler LowLevelDataSent
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.h;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.h, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.h;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.h, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000B3A RID: 2874 RVA: 0x000306B0 File Offset: 0x0002F6B0
		// (remove) Token: 0x06000B3B RID: 2875 RVA: 0x000306E8 File Offset: 0x0002F6E8
		public event HostResolvedEventHandler HostResolved
		{
			[CompilerGenerated]
			add
			{
				HostResolvedEventHandler hostResolvedEventHandler = this.i;
				HostResolvedEventHandler hostResolvedEventHandler2;
				do
				{
					hostResolvedEventHandler2 = hostResolvedEventHandler;
					HostResolvedEventHandler value2 = (HostResolvedEventHandler)Delegate.Combine(hostResolvedEventHandler2, value);
					hostResolvedEventHandler = Interlocked.CompareExchange<HostResolvedEventHandler>(ref this.i, value2, hostResolvedEventHandler2);
				}
				while (hostResolvedEventHandler != hostResolvedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				HostResolvedEventHandler hostResolvedEventHandler = this.i;
				HostResolvedEventHandler hostResolvedEventHandler2;
				do
				{
					hostResolvedEventHandler2 = hostResolvedEventHandler;
					HostResolvedEventHandler value2 = (HostResolvedEventHandler)Delegate.Remove(hostResolvedEventHandler2, value);
					hostResolvedEventHandler = Interlocked.CompareExchange<HostResolvedEventHandler>(ref this.i, value2, hostResolvedEventHandler2);
				}
				while (hostResolvedEventHandler != hostResolvedEventHandler2);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000B3C RID: 2876 RVA: 0x00030720 File Offset: 0x0002F720
		// (remove) Token: 0x06000B3D RID: 2877 RVA: 0x00030758 File Offset: 0x0002F758
		public event SocketCreatingEventHandler SocketCreating
		{
			[CompilerGenerated]
			add
			{
				SocketCreatingEventHandler socketCreatingEventHandler = this.j;
				SocketCreatingEventHandler socketCreatingEventHandler2;
				do
				{
					socketCreatingEventHandler2 = socketCreatingEventHandler;
					SocketCreatingEventHandler value2 = (SocketCreatingEventHandler)Delegate.Combine(socketCreatingEventHandler2, value);
					socketCreatingEventHandler = Interlocked.CompareExchange<SocketCreatingEventHandler>(ref this.j, value2, socketCreatingEventHandler2);
				}
				while (socketCreatingEventHandler != socketCreatingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SocketCreatingEventHandler socketCreatingEventHandler = this.j;
				SocketCreatingEventHandler socketCreatingEventHandler2;
				do
				{
					socketCreatingEventHandler2 = socketCreatingEventHandler;
					SocketCreatingEventHandler value2 = (SocketCreatingEventHandler)Delegate.Remove(socketCreatingEventHandler2, value);
					socketCreatingEventHandler = Interlocked.CompareExchange<SocketCreatingEventHandler>(ref this.j, value2, socketCreatingEventHandler2);
				}
				while (socketCreatingEventHandler != socketCreatingEventHandler2);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000B3E RID: 2878 RVA: 0x00030790 File Offset: 0x0002F790
		// (remove) Token: 0x06000B3F RID: 2879 RVA: 0x000307C8 File Offset: 0x0002F7C8
		public event SocketConnectedEventHandler SocketConnected
		{
			[CompilerGenerated]
			add
			{
				SocketConnectedEventHandler socketConnectedEventHandler = this.k;
				SocketConnectedEventHandler socketConnectedEventHandler2;
				do
				{
					socketConnectedEventHandler2 = socketConnectedEventHandler;
					SocketConnectedEventHandler value2 = (SocketConnectedEventHandler)Delegate.Combine(socketConnectedEventHandler2, value);
					socketConnectedEventHandler = Interlocked.CompareExchange<SocketConnectedEventHandler>(ref this.k, value2, socketConnectedEventHandler2);
				}
				while (socketConnectedEventHandler != socketConnectedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SocketConnectedEventHandler socketConnectedEventHandler = this.k;
				SocketConnectedEventHandler socketConnectedEventHandler2;
				do
				{
					socketConnectedEventHandler2 = socketConnectedEventHandler;
					SocketConnectedEventHandler value2 = (SocketConnectedEventHandler)Delegate.Remove(socketConnectedEventHandler2, value);
					socketConnectedEventHandler = Interlocked.CompareExchange<SocketConnectedEventHandler>(ref this.k, value2, socketConnectedEventHandler2);
				}
				while (socketConnectedEventHandler != socketConnectedEventHandler2);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000B40 RID: 2880 RVA: 0x00030800 File Offset: 0x0002F800
		// (remove) Token: 0x06000B41 RID: 2881 RVA: 0x00030838 File Offset: 0x0002F838
		public event ConnectedEventHandler Connected
		{
			[CompilerGenerated]
			add
			{
				ConnectedEventHandler connectedEventHandler = this.l;
				ConnectedEventHandler connectedEventHandler2;
				do
				{
					connectedEventHandler2 = connectedEventHandler;
					ConnectedEventHandler value2 = (ConnectedEventHandler)Delegate.Combine(connectedEventHandler2, value);
					connectedEventHandler = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.l, value2, connectedEventHandler2);
				}
				while (connectedEventHandler != connectedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ConnectedEventHandler connectedEventHandler = this.l;
				ConnectedEventHandler connectedEventHandler2;
				do
				{
					connectedEventHandler2 = connectedEventHandler;
					ConnectedEventHandler value2 = (ConnectedEventHandler)Delegate.Remove(connectedEventHandler2, value);
					connectedEventHandler = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.l, value2, connectedEventHandler2);
				}
				while (connectedEventHandler != connectedEventHandler2);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000B42 RID: 2882 RVA: 0x00030870 File Offset: 0x0002F870
		// (remove) Token: 0x06000B43 RID: 2883 RVA: 0x000308A8 File Offset: 0x0002F8A8
		public event DisconnectedEventHandler Disconnected
		{
			[CompilerGenerated]
			add
			{
				DisconnectedEventHandler disconnectedEventHandler = this.m;
				DisconnectedEventHandler disconnectedEventHandler2;
				do
				{
					disconnectedEventHandler2 = disconnectedEventHandler;
					DisconnectedEventHandler value2 = (DisconnectedEventHandler)Delegate.Combine(disconnectedEventHandler2, value);
					disconnectedEventHandler = Interlocked.CompareExchange<DisconnectedEventHandler>(ref this.m, value2, disconnectedEventHandler2);
				}
				while (disconnectedEventHandler != disconnectedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DisconnectedEventHandler disconnectedEventHandler = this.m;
				DisconnectedEventHandler disconnectedEventHandler2;
				do
				{
					disconnectedEventHandler2 = disconnectedEventHandler;
					DisconnectedEventHandler value2 = (DisconnectedEventHandler)Delegate.Remove(disconnectedEventHandler2, value);
					disconnectedEventHandler = Interlocked.CompareExchange<DisconnectedEventHandler>(ref this.m, value2, disconnectedEventHandler2);
				}
				while (disconnectedEventHandler != disconnectedEventHandler2);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000B44 RID: 2884 RVA: 0x000308E0 File Offset: 0x0002F8E0
		// (remove) Token: 0x06000B45 RID: 2885 RVA: 0x00030918 File Offset: 0x0002F918
		public event TlsStartedEventHandler TlsStarted
		{
			[CompilerGenerated]
			add
			{
				TlsStartedEventHandler tlsStartedEventHandler = this.n;
				TlsStartedEventHandler tlsStartedEventHandler2;
				do
				{
					tlsStartedEventHandler2 = tlsStartedEventHandler;
					TlsStartedEventHandler value2 = (TlsStartedEventHandler)Delegate.Combine(tlsStartedEventHandler2, value);
					tlsStartedEventHandler = Interlocked.CompareExchange<TlsStartedEventHandler>(ref this.n, value2, tlsStartedEventHandler2);
				}
				while (tlsStartedEventHandler != tlsStartedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				TlsStartedEventHandler tlsStartedEventHandler = this.n;
				TlsStartedEventHandler tlsStartedEventHandler2;
				do
				{
					tlsStartedEventHandler2 = tlsStartedEventHandler;
					TlsStartedEventHandler value2 = (TlsStartedEventHandler)Delegate.Remove(tlsStartedEventHandler2, value);
					tlsStartedEventHandler = Interlocked.CompareExchange<TlsStartedEventHandler>(ref this.n, value2, tlsStartedEventHandler2);
				}
				while (tlsStartedEventHandler != tlsStartedEventHandler2);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000B46 RID: 2886 RVA: 0x00030950 File Offset: 0x0002F950
		// (remove) Token: 0x06000B47 RID: 2887 RVA: 0x00030988 File Offset: 0x0002F988
		public event LoggedInEventHandler LoggedIn
		{
			[CompilerGenerated]
			add
			{
				LoggedInEventHandler loggedInEventHandler = this.o;
				LoggedInEventHandler loggedInEventHandler2;
				do
				{
					loggedInEventHandler2 = loggedInEventHandler;
					LoggedInEventHandler value2 = (LoggedInEventHandler)Delegate.Combine(loggedInEventHandler2, value);
					loggedInEventHandler = Interlocked.CompareExchange<LoggedInEventHandler>(ref this.o, value2, loggedInEventHandler2);
				}
				while (loggedInEventHandler != loggedInEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				LoggedInEventHandler loggedInEventHandler = this.o;
				LoggedInEventHandler loggedInEventHandler2;
				do
				{
					loggedInEventHandler2 = loggedInEventHandler;
					LoggedInEventHandler value2 = (LoggedInEventHandler)Delegate.Remove(loggedInEventHandler2, value);
					loggedInEventHandler = Interlocked.CompareExchange<LoggedInEventHandler>(ref this.o, value2, loggedInEventHandler2);
				}
				while (loggedInEventHandler != loggedInEventHandler2);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000B48 RID: 2888 RVA: 0x000309C0 File Offset: 0x0002F9C0
		// (remove) Token: 0x06000B49 RID: 2889 RVA: 0x000309F8 File Offset: 0x0002F9F8
		public event SmtpSendingMessageEventHandler SendingMessage
		{
			[CompilerGenerated]
			add
			{
				SmtpSendingMessageEventHandler smtpSendingMessageEventHandler = this.p;
				SmtpSendingMessageEventHandler smtpSendingMessageEventHandler2;
				do
				{
					smtpSendingMessageEventHandler2 = smtpSendingMessageEventHandler;
					SmtpSendingMessageEventHandler value2 = (SmtpSendingMessageEventHandler)Delegate.Combine(smtpSendingMessageEventHandler2, value);
					smtpSendingMessageEventHandler = Interlocked.CompareExchange<SmtpSendingMessageEventHandler>(ref this.p, value2, smtpSendingMessageEventHandler2);
				}
				while (smtpSendingMessageEventHandler != smtpSendingMessageEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpSendingMessageEventHandler smtpSendingMessageEventHandler = this.p;
				SmtpSendingMessageEventHandler smtpSendingMessageEventHandler2;
				do
				{
					smtpSendingMessageEventHandler2 = smtpSendingMessageEventHandler;
					SmtpSendingMessageEventHandler value2 = (SmtpSendingMessageEventHandler)Delegate.Remove(smtpSendingMessageEventHandler2, value);
					smtpSendingMessageEventHandler = Interlocked.CompareExchange<SmtpSendingMessageEventHandler>(ref this.p, value2, smtpSendingMessageEventHandler2);
				}
				while (smtpSendingMessageEventHandler != smtpSendingMessageEventHandler2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B4A RID: 2890 RVA: 0x00030A30 File Offset: 0x0002FA30
		// (remove) Token: 0x06000B4B RID: 2891 RVA: 0x00030A68 File Offset: 0x0002FA68
		public event SmtpMessageSenderSubmittedEventHandler MessageSenderSubmitted
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageSenderSubmittedEventHandler smtpMessageSenderSubmittedEventHandler = this.q;
				SmtpMessageSenderSubmittedEventHandler smtpMessageSenderSubmittedEventHandler2;
				do
				{
					smtpMessageSenderSubmittedEventHandler2 = smtpMessageSenderSubmittedEventHandler;
					SmtpMessageSenderSubmittedEventHandler value2 = (SmtpMessageSenderSubmittedEventHandler)Delegate.Combine(smtpMessageSenderSubmittedEventHandler2, value);
					smtpMessageSenderSubmittedEventHandler = Interlocked.CompareExchange<SmtpMessageSenderSubmittedEventHandler>(ref this.q, value2, smtpMessageSenderSubmittedEventHandler2);
				}
				while (smtpMessageSenderSubmittedEventHandler != smtpMessageSenderSubmittedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageSenderSubmittedEventHandler smtpMessageSenderSubmittedEventHandler = this.q;
				SmtpMessageSenderSubmittedEventHandler smtpMessageSenderSubmittedEventHandler2;
				do
				{
					smtpMessageSenderSubmittedEventHandler2 = smtpMessageSenderSubmittedEventHandler;
					SmtpMessageSenderSubmittedEventHandler value2 = (SmtpMessageSenderSubmittedEventHandler)Delegate.Remove(smtpMessageSenderSubmittedEventHandler2, value);
					smtpMessageSenderSubmittedEventHandler = Interlocked.CompareExchange<SmtpMessageSenderSubmittedEventHandler>(ref this.q, value2, smtpMessageSenderSubmittedEventHandler2);
				}
				while (smtpMessageSenderSubmittedEventHandler != smtpMessageSenderSubmittedEventHandler2);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000B4C RID: 2892 RVA: 0x00030AA0 File Offset: 0x0002FAA0
		// (remove) Token: 0x06000B4D RID: 2893 RVA: 0x00030AD8 File Offset: 0x0002FAD8
		public event SmtpMessageRecipientSubmittedEventHandler MessageRecipientSubmitted
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageRecipientSubmittedEventHandler smtpMessageRecipientSubmittedEventHandler = this.r;
				SmtpMessageRecipientSubmittedEventHandler smtpMessageRecipientSubmittedEventHandler2;
				do
				{
					smtpMessageRecipientSubmittedEventHandler2 = smtpMessageRecipientSubmittedEventHandler;
					SmtpMessageRecipientSubmittedEventHandler value2 = (SmtpMessageRecipientSubmittedEventHandler)Delegate.Combine(smtpMessageRecipientSubmittedEventHandler2, value);
					smtpMessageRecipientSubmittedEventHandler = Interlocked.CompareExchange<SmtpMessageRecipientSubmittedEventHandler>(ref this.r, value2, smtpMessageRecipientSubmittedEventHandler2);
				}
				while (smtpMessageRecipientSubmittedEventHandler != smtpMessageRecipientSubmittedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageRecipientSubmittedEventHandler smtpMessageRecipientSubmittedEventHandler = this.r;
				SmtpMessageRecipientSubmittedEventHandler smtpMessageRecipientSubmittedEventHandler2;
				do
				{
					smtpMessageRecipientSubmittedEventHandler2 = smtpMessageRecipientSubmittedEventHandler;
					SmtpMessageRecipientSubmittedEventHandler value2 = (SmtpMessageRecipientSubmittedEventHandler)Delegate.Remove(smtpMessageRecipientSubmittedEventHandler2, value);
					smtpMessageRecipientSubmittedEventHandler = Interlocked.CompareExchange<SmtpMessageRecipientSubmittedEventHandler>(ref this.r, value2, smtpMessageRecipientSubmittedEventHandler2);
				}
				while (smtpMessageRecipientSubmittedEventHandler != smtpMessageRecipientSubmittedEventHandler2);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000B4E RID: 2894 RVA: 0x00030B10 File Offset: 0x0002FB10
		// (remove) Token: 0x06000B4F RID: 2895 RVA: 0x00030B48 File Offset: 0x0002FB48
		public event SmtpMessageDataChunkSentEventHandler MessageDataChunkSent
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageDataChunkSentEventHandler smtpMessageDataChunkSentEventHandler = this.s;
				SmtpMessageDataChunkSentEventHandler smtpMessageDataChunkSentEventHandler2;
				do
				{
					smtpMessageDataChunkSentEventHandler2 = smtpMessageDataChunkSentEventHandler;
					SmtpMessageDataChunkSentEventHandler value2 = (SmtpMessageDataChunkSentEventHandler)Delegate.Combine(smtpMessageDataChunkSentEventHandler2, value);
					smtpMessageDataChunkSentEventHandler = Interlocked.CompareExchange<SmtpMessageDataChunkSentEventHandler>(ref this.s, value2, smtpMessageDataChunkSentEventHandler2);
				}
				while (smtpMessageDataChunkSentEventHandler != smtpMessageDataChunkSentEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageDataChunkSentEventHandler smtpMessageDataChunkSentEventHandler = this.s;
				SmtpMessageDataChunkSentEventHandler smtpMessageDataChunkSentEventHandler2;
				do
				{
					smtpMessageDataChunkSentEventHandler2 = smtpMessageDataChunkSentEventHandler;
					SmtpMessageDataChunkSentEventHandler value2 = (SmtpMessageDataChunkSentEventHandler)Delegate.Remove(smtpMessageDataChunkSentEventHandler2, value);
					smtpMessageDataChunkSentEventHandler = Interlocked.CompareExchange<SmtpMessageDataChunkSentEventHandler>(ref this.s, value2, smtpMessageDataChunkSentEventHandler2);
				}
				while (smtpMessageDataChunkSentEventHandler != smtpMessageDataChunkSentEventHandler2);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000B50 RID: 2896 RVA: 0x00030B80 File Offset: 0x0002FB80
		// (remove) Token: 0x06000B51 RID: 2897 RVA: 0x00030BB8 File Offset: 0x0002FBB8
		public event SmtpMessageSubmittedToServerEventHandler MessageSubmittedToServer
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageSubmittedToServerEventHandler smtpMessageSubmittedToServerEventHandler = this.t;
				SmtpMessageSubmittedToServerEventHandler smtpMessageSubmittedToServerEventHandler2;
				do
				{
					smtpMessageSubmittedToServerEventHandler2 = smtpMessageSubmittedToServerEventHandler;
					SmtpMessageSubmittedToServerEventHandler value2 = (SmtpMessageSubmittedToServerEventHandler)Delegate.Combine(smtpMessageSubmittedToServerEventHandler2, value);
					smtpMessageSubmittedToServerEventHandler = Interlocked.CompareExchange<SmtpMessageSubmittedToServerEventHandler>(ref this.t, value2, smtpMessageSubmittedToServerEventHandler2);
				}
				while (smtpMessageSubmittedToServerEventHandler != smtpMessageSubmittedToServerEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageSubmittedToServerEventHandler smtpMessageSubmittedToServerEventHandler = this.t;
				SmtpMessageSubmittedToServerEventHandler smtpMessageSubmittedToServerEventHandler2;
				do
				{
					smtpMessageSubmittedToServerEventHandler2 = smtpMessageSubmittedToServerEventHandler;
					SmtpMessageSubmittedToServerEventHandler value2 = (SmtpMessageSubmittedToServerEventHandler)Delegate.Remove(smtpMessageSubmittedToServerEventHandler2, value);
					smtpMessageSubmittedToServerEventHandler = Interlocked.CompareExchange<SmtpMessageSubmittedToServerEventHandler>(ref this.t, value2, smtpMessageSubmittedToServerEventHandler2);
				}
				while (smtpMessageSubmittedToServerEventHandler != smtpMessageSubmittedToServerEventHandler2);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000B52 RID: 2898 RVA: 0x00030BF0 File Offset: 0x0002FBF0
		// (remove) Token: 0x06000B53 RID: 2899 RVA: 0x00030C28 File Offset: 0x0002FC28
		public event SmtpMessageSentEventHandler MessageSent
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageSentEventHandler smtpMessageSentEventHandler = this.u;
				SmtpMessageSentEventHandler smtpMessageSentEventHandler2;
				do
				{
					smtpMessageSentEventHandler2 = smtpMessageSentEventHandler;
					SmtpMessageSentEventHandler value2 = (SmtpMessageSentEventHandler)Delegate.Combine(smtpMessageSentEventHandler2, value);
					smtpMessageSentEventHandler = Interlocked.CompareExchange<SmtpMessageSentEventHandler>(ref this.u, value2, smtpMessageSentEventHandler2);
				}
				while (smtpMessageSentEventHandler != smtpMessageSentEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageSentEventHandler smtpMessageSentEventHandler = this.u;
				SmtpMessageSentEventHandler smtpMessageSentEventHandler2;
				do
				{
					smtpMessageSentEventHandler2 = smtpMessageSentEventHandler;
					SmtpMessageSentEventHandler value2 = (SmtpMessageSentEventHandler)Delegate.Remove(smtpMessageSentEventHandler2, value);
					smtpMessageSentEventHandler = Interlocked.CompareExchange<SmtpMessageSentEventHandler>(ref this.u, value2, smtpMessageSentEventHandler2);
				}
				while (smtpMessageSentEventHandler != smtpMessageSentEventHandler2);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000B54 RID: 2900 RVA: 0x00030C60 File Offset: 0x0002FC60
		// (remove) Token: 0x06000B55 RID: 2901 RVA: 0x00030C98 File Offset: 0x0002FC98
		public event SmtpMessageNotSentEventHandler MessageNotSent
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageNotSentEventHandler smtpMessageNotSentEventHandler = this.v;
				SmtpMessageNotSentEventHandler smtpMessageNotSentEventHandler2;
				do
				{
					smtpMessageNotSentEventHandler2 = smtpMessageNotSentEventHandler;
					SmtpMessageNotSentEventHandler value2 = (SmtpMessageNotSentEventHandler)Delegate.Combine(smtpMessageNotSentEventHandler2, value);
					smtpMessageNotSentEventHandler = Interlocked.CompareExchange<SmtpMessageNotSentEventHandler>(ref this.v, value2, smtpMessageNotSentEventHandler2);
				}
				while (smtpMessageNotSentEventHandler != smtpMessageNotSentEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageNotSentEventHandler smtpMessageNotSentEventHandler = this.v;
				SmtpMessageNotSentEventHandler smtpMessageNotSentEventHandler2;
				do
				{
					smtpMessageNotSentEventHandler2 = smtpMessageNotSentEventHandler;
					SmtpMessageNotSentEventHandler value2 = (SmtpMessageNotSentEventHandler)Delegate.Remove(smtpMessageNotSentEventHandler2, value);
					smtpMessageNotSentEventHandler = Interlocked.CompareExchange<SmtpMessageNotSentEventHandler>(ref this.v, value2, smtpMessageNotSentEventHandler2);
				}
				while (smtpMessageNotSentEventHandler != smtpMessageNotSentEventHandler2);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000B56 RID: 2902 RVA: 0x00030CD0 File Offset: 0x0002FCD0
		// (remove) Token: 0x06000B57 RID: 2903 RVA: 0x00030D08 File Offset: 0x0002FD08
		public event SmtpTransientErrorOccurredEventHandler TransientErrorOccurred
		{
			[CompilerGenerated]
			add
			{
				SmtpTransientErrorOccurredEventHandler smtpTransientErrorOccurredEventHandler = this.w;
				SmtpTransientErrorOccurredEventHandler smtpTransientErrorOccurredEventHandler2;
				do
				{
					smtpTransientErrorOccurredEventHandler2 = smtpTransientErrorOccurredEventHandler;
					SmtpTransientErrorOccurredEventHandler value2 = (SmtpTransientErrorOccurredEventHandler)Delegate.Combine(smtpTransientErrorOccurredEventHandler2, value);
					smtpTransientErrorOccurredEventHandler = Interlocked.CompareExchange<SmtpTransientErrorOccurredEventHandler>(ref this.w, value2, smtpTransientErrorOccurredEventHandler2);
				}
				while (smtpTransientErrorOccurredEventHandler != smtpTransientErrorOccurredEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpTransientErrorOccurredEventHandler smtpTransientErrorOccurredEventHandler = this.w;
				SmtpTransientErrorOccurredEventHandler smtpTransientErrorOccurredEventHandler2;
				do
				{
					smtpTransientErrorOccurredEventHandler2 = smtpTransientErrorOccurredEventHandler;
					SmtpTransientErrorOccurredEventHandler value2 = (SmtpTransientErrorOccurredEventHandler)Delegate.Remove(smtpTransientErrorOccurredEventHandler2, value);
					smtpTransientErrorOccurredEventHandler = Interlocked.CompareExchange<SmtpTransientErrorOccurredEventHandler>(ref this.w, value2, smtpTransientErrorOccurredEventHandler2);
				}
				while (smtpTransientErrorOccurredEventHandler != smtpTransientErrorOccurredEventHandler2);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000B58 RID: 2904 RVA: 0x00030D40 File Offset: 0x0002FD40
		// (remove) Token: 0x06000B59 RID: 2905 RVA: 0x00030D78 File Offset: 0x0002FD78
		public event SmtpMergingMessageEventHandler MergingMessage
		{
			[CompilerGenerated]
			add
			{
				SmtpMergingMessageEventHandler smtpMergingMessageEventHandler = this.x;
				SmtpMergingMessageEventHandler smtpMergingMessageEventHandler2;
				do
				{
					smtpMergingMessageEventHandler2 = smtpMergingMessageEventHandler;
					SmtpMergingMessageEventHandler value2 = (SmtpMergingMessageEventHandler)Delegate.Combine(smtpMergingMessageEventHandler2, value);
					smtpMergingMessageEventHandler = Interlocked.CompareExchange<SmtpMergingMessageEventHandler>(ref this.x, value2, smtpMergingMessageEventHandler2);
				}
				while (smtpMergingMessageEventHandler != smtpMergingMessageEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMergingMessageEventHandler smtpMergingMessageEventHandler = this.x;
				SmtpMergingMessageEventHandler smtpMergingMessageEventHandler2;
				do
				{
					smtpMergingMessageEventHandler2 = smtpMergingMessageEventHandler;
					SmtpMergingMessageEventHandler value2 = (SmtpMergingMessageEventHandler)Delegate.Remove(smtpMergingMessageEventHandler2, value);
					smtpMergingMessageEventHandler = Interlocked.CompareExchange<SmtpMergingMessageEventHandler>(ref this.x, value2, smtpMergingMessageEventHandler2);
				}
				while (smtpMergingMessageEventHandler != smtpMergingMessageEventHandler2);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000B5A RID: 2906 RVA: 0x00030DB0 File Offset: 0x0002FDB0
		// (remove) Token: 0x06000B5B RID: 2907 RVA: 0x00030DE8 File Offset: 0x0002FDE8
		public event SmtpFinishingJobEventHandler FinishingJob
		{
			[CompilerGenerated]
			add
			{
				SmtpFinishingJobEventHandler smtpFinishingJobEventHandler = this.y;
				SmtpFinishingJobEventHandler smtpFinishingJobEventHandler2;
				do
				{
					smtpFinishingJobEventHandler2 = smtpFinishingJobEventHandler;
					SmtpFinishingJobEventHandler value2 = (SmtpFinishingJobEventHandler)Delegate.Combine(smtpFinishingJobEventHandler2, value);
					smtpFinishingJobEventHandler = Interlocked.CompareExchange<SmtpFinishingJobEventHandler>(ref this.y, value2, smtpFinishingJobEventHandler2);
				}
				while (smtpFinishingJobEventHandler != smtpFinishingJobEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpFinishingJobEventHandler smtpFinishingJobEventHandler = this.y;
				SmtpFinishingJobEventHandler smtpFinishingJobEventHandler2;
				do
				{
					smtpFinishingJobEventHandler2 = smtpFinishingJobEventHandler;
					SmtpFinishingJobEventHandler value2 = (SmtpFinishingJobEventHandler)Delegate.Remove(smtpFinishingJobEventHandler2, value);
					smtpFinishingJobEventHandler = Interlocked.CompareExchange<SmtpFinishingJobEventHandler>(ref this.y, value2, smtpFinishingJobEventHandler2);
				}
				while (smtpFinishingJobEventHandler != smtpFinishingJobEventHandler2);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000B5C RID: 2908 RVA: 0x00030E20 File Offset: 0x0002FE20
		// (remove) Token: 0x06000B5D RID: 2909 RVA: 0x00030E58 File Offset: 0x0002FE58
		public event SmtpSubmittingMessageToPickupFolderEventHandler SubmittingMessageToPickupFolder
		{
			[CompilerGenerated]
			add
			{
				SmtpSubmittingMessageToPickupFolderEventHandler smtpSubmittingMessageToPickupFolderEventHandler = this.z;
				SmtpSubmittingMessageToPickupFolderEventHandler smtpSubmittingMessageToPickupFolderEventHandler2;
				do
				{
					smtpSubmittingMessageToPickupFolderEventHandler2 = smtpSubmittingMessageToPickupFolderEventHandler;
					SmtpSubmittingMessageToPickupFolderEventHandler value2 = (SmtpSubmittingMessageToPickupFolderEventHandler)Delegate.Combine(smtpSubmittingMessageToPickupFolderEventHandler2, value);
					smtpSubmittingMessageToPickupFolderEventHandler = Interlocked.CompareExchange<SmtpSubmittingMessageToPickupFolderEventHandler>(ref this.z, value2, smtpSubmittingMessageToPickupFolderEventHandler2);
				}
				while (smtpSubmittingMessageToPickupFolderEventHandler != smtpSubmittingMessageToPickupFolderEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpSubmittingMessageToPickupFolderEventHandler smtpSubmittingMessageToPickupFolderEventHandler = this.z;
				SmtpSubmittingMessageToPickupFolderEventHandler smtpSubmittingMessageToPickupFolderEventHandler2;
				do
				{
					smtpSubmittingMessageToPickupFolderEventHandler2 = smtpSubmittingMessageToPickupFolderEventHandler;
					SmtpSubmittingMessageToPickupFolderEventHandler value2 = (SmtpSubmittingMessageToPickupFolderEventHandler)Delegate.Remove(smtpSubmittingMessageToPickupFolderEventHandler2, value);
					smtpSubmittingMessageToPickupFolderEventHandler = Interlocked.CompareExchange<SmtpSubmittingMessageToPickupFolderEventHandler>(ref this.z, value2, smtpSubmittingMessageToPickupFolderEventHandler2);
				}
				while (smtpSubmittingMessageToPickupFolderEventHandler != smtpSubmittingMessageToPickupFolderEventHandler2);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000B5E RID: 2910 RVA: 0x00030E90 File Offset: 0x0002FE90
		// (remove) Token: 0x06000B5F RID: 2911 RVA: 0x00030EC8 File Offset: 0x0002FEC8
		public event SmtpMessageSubmittedToPickupFolderEventHandler MessageSubmittedToPickupFolder
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageSubmittedToPickupFolderEventHandler smtpMessageSubmittedToPickupFolderEventHandler = this.aa;
				SmtpMessageSubmittedToPickupFolderEventHandler smtpMessageSubmittedToPickupFolderEventHandler2;
				do
				{
					smtpMessageSubmittedToPickupFolderEventHandler2 = smtpMessageSubmittedToPickupFolderEventHandler;
					SmtpMessageSubmittedToPickupFolderEventHandler value2 = (SmtpMessageSubmittedToPickupFolderEventHandler)Delegate.Combine(smtpMessageSubmittedToPickupFolderEventHandler2, value);
					smtpMessageSubmittedToPickupFolderEventHandler = Interlocked.CompareExchange<SmtpMessageSubmittedToPickupFolderEventHandler>(ref this.aa, value2, smtpMessageSubmittedToPickupFolderEventHandler2);
				}
				while (smtpMessageSubmittedToPickupFolderEventHandler != smtpMessageSubmittedToPickupFolderEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageSubmittedToPickupFolderEventHandler smtpMessageSubmittedToPickupFolderEventHandler = this.aa;
				SmtpMessageSubmittedToPickupFolderEventHandler smtpMessageSubmittedToPickupFolderEventHandler2;
				do
				{
					smtpMessageSubmittedToPickupFolderEventHandler2 = smtpMessageSubmittedToPickupFolderEventHandler;
					SmtpMessageSubmittedToPickupFolderEventHandler value2 = (SmtpMessageSubmittedToPickupFolderEventHandler)Delegate.Remove(smtpMessageSubmittedToPickupFolderEventHandler2, value);
					smtpMessageSubmittedToPickupFolderEventHandler = Interlocked.CompareExchange<SmtpMessageSubmittedToPickupFolderEventHandler>(ref this.aa, value2, smtpMessageSubmittedToPickupFolderEventHandler2);
				}
				while (smtpMessageSubmittedToPickupFolderEventHandler != smtpMessageSubmittedToPickupFolderEventHandler2);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000B60 RID: 2912 RVA: 0x00030F00 File Offset: 0x0002FF00
		// (remove) Token: 0x06000B61 RID: 2913 RVA: 0x00030F38 File Offset: 0x0002FF38
		public event SmtpMessageMXLookupDoneEventHandler MessageMXLookupDone
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageMXLookupDoneEventHandler smtpMessageMXLookupDoneEventHandler = this.ab;
				SmtpMessageMXLookupDoneEventHandler smtpMessageMXLookupDoneEventHandler2;
				do
				{
					smtpMessageMXLookupDoneEventHandler2 = smtpMessageMXLookupDoneEventHandler;
					SmtpMessageMXLookupDoneEventHandler value2 = (SmtpMessageMXLookupDoneEventHandler)Delegate.Combine(smtpMessageMXLookupDoneEventHandler2, value);
					smtpMessageMXLookupDoneEventHandler = Interlocked.CompareExchange<SmtpMessageMXLookupDoneEventHandler>(ref this.ab, value2, smtpMessageMXLookupDoneEventHandler2);
				}
				while (smtpMessageMXLookupDoneEventHandler != smtpMessageMXLookupDoneEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageMXLookupDoneEventHandler smtpMessageMXLookupDoneEventHandler = this.ab;
				SmtpMessageMXLookupDoneEventHandler smtpMessageMXLookupDoneEventHandler2;
				do
				{
					smtpMessageMXLookupDoneEventHandler2 = smtpMessageMXLookupDoneEventHandler;
					SmtpMessageMXLookupDoneEventHandler value2 = (SmtpMessageMXLookupDoneEventHandler)Delegate.Remove(smtpMessageMXLookupDoneEventHandler2, value);
					smtpMessageMXLookupDoneEventHandler = Interlocked.CompareExchange<SmtpMessageMXLookupDoneEventHandler>(ref this.ab, value2, smtpMessageMXLookupDoneEventHandler2);
				}
				while (smtpMessageMXLookupDoneEventHandler != smtpMessageMXLookupDoneEventHandler2);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000B62 RID: 2914 RVA: 0x00030F70 File Offset: 0x0002FF70
		// (remove) Token: 0x06000B63 RID: 2915 RVA: 0x00030FA8 File Offset: 0x0002FFA8
		public event SmtpMessageDirectSendDoneEventHandler MessageDirectSendDone
		{
			[CompilerGenerated]
			add
			{
				SmtpMessageDirectSendDoneEventHandler smtpMessageDirectSendDoneEventHandler = this.ac;
				SmtpMessageDirectSendDoneEventHandler smtpMessageDirectSendDoneEventHandler2;
				do
				{
					smtpMessageDirectSendDoneEventHandler2 = smtpMessageDirectSendDoneEventHandler;
					SmtpMessageDirectSendDoneEventHandler value2 = (SmtpMessageDirectSendDoneEventHandler)Delegate.Combine(smtpMessageDirectSendDoneEventHandler2, value);
					smtpMessageDirectSendDoneEventHandler = Interlocked.CompareExchange<SmtpMessageDirectSendDoneEventHandler>(ref this.ac, value2, smtpMessageDirectSendDoneEventHandler2);
				}
				while (smtpMessageDirectSendDoneEventHandler != smtpMessageDirectSendDoneEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SmtpMessageDirectSendDoneEventHandler smtpMessageDirectSendDoneEventHandler = this.ac;
				SmtpMessageDirectSendDoneEventHandler smtpMessageDirectSendDoneEventHandler2;
				do
				{
					smtpMessageDirectSendDoneEventHandler2 = smtpMessageDirectSendDoneEventHandler;
					SmtpMessageDirectSendDoneEventHandler value2 = (SmtpMessageDirectSendDoneEventHandler)Delegate.Remove(smtpMessageDirectSendDoneEventHandler2, value);
					smtpMessageDirectSendDoneEventHandler = Interlocked.CompareExchange<SmtpMessageDirectSendDoneEventHandler>(ref this.ac, value2, smtpMessageDirectSendDoneEventHandler2);
				}
				while (smtpMessageDirectSendDoneEventHandler != smtpMessageDirectSendDoneEventHandler2);
			}
		}

		// Token: 0x04000809 RID: 2057
		[CompilerGenerated]
		private EventHandler a;

		// Token: 0x0400080A RID: 2058
		private ISite b;

		// Token: 0x0400080B RID: 2059
		[CompilerGenerated]
		private ErrorEventHandler c;

		// Token: 0x0400080C RID: 2060
		[CompilerGenerated]
		private LogNewEntryEventHandler d;

		// Token: 0x0400080D RID: 2061
		[CompilerGenerated]
		private DataTransferEventHandler e;

		// Token: 0x0400080E RID: 2062
		[CompilerGenerated]
		private DataTransferEventHandler f;

		// Token: 0x0400080F RID: 2063
		[CompilerGenerated]
		private DataTransferEventHandler g;

		// Token: 0x04000810 RID: 2064
		[CompilerGenerated]
		private DataTransferEventHandler h;

		// Token: 0x04000811 RID: 2065
		[CompilerGenerated]
		private HostResolvedEventHandler i;

		// Token: 0x04000812 RID: 2066
		[CompilerGenerated]
		private SocketCreatingEventHandler j;

		// Token: 0x04000813 RID: 2067
		[CompilerGenerated]
		private SocketConnectedEventHandler k;

		// Token: 0x04000814 RID: 2068
		[CompilerGenerated]
		private ConnectedEventHandler l;

		// Token: 0x04000815 RID: 2069
		[CompilerGenerated]
		private DisconnectedEventHandler m;

		// Token: 0x04000816 RID: 2070
		[CompilerGenerated]
		private TlsStartedEventHandler n;

		// Token: 0x04000817 RID: 2071
		[CompilerGenerated]
		private LoggedInEventHandler o;

		// Token: 0x04000818 RID: 2072
		[CompilerGenerated]
		private SmtpSendingMessageEventHandler p;

		// Token: 0x04000819 RID: 2073
		[CompilerGenerated]
		private SmtpMessageSenderSubmittedEventHandler q;

		// Token: 0x0400081A RID: 2074
		[CompilerGenerated]
		private SmtpMessageRecipientSubmittedEventHandler r;

		// Token: 0x0400081B RID: 2075
		[CompilerGenerated]
		private SmtpMessageDataChunkSentEventHandler s;

		// Token: 0x0400081C RID: 2076
		[CompilerGenerated]
		private SmtpMessageSubmittedToServerEventHandler t;

		// Token: 0x0400081D RID: 2077
		[CompilerGenerated]
		private SmtpMessageSentEventHandler u;

		// Token: 0x0400081E RID: 2078
		[CompilerGenerated]
		private SmtpMessageNotSentEventHandler v;

		// Token: 0x0400081F RID: 2079
		[CompilerGenerated]
		private SmtpTransientErrorOccurredEventHandler w;

		// Token: 0x04000820 RID: 2080
		[CompilerGenerated]
		private SmtpMergingMessageEventHandler x;

		// Token: 0x04000821 RID: 2081
		[CompilerGenerated]
		private SmtpFinishingJobEventHandler y;

		// Token: 0x04000822 RID: 2082
		[CompilerGenerated]
		private SmtpSubmittingMessageToPickupFolderEventHandler z;

		// Token: 0x04000823 RID: 2083
		[CompilerGenerated]
		private SmtpMessageSubmittedToPickupFolderEventHandler aa;

		// Token: 0x04000824 RID: 2084
		[CompilerGenerated]
		private SmtpMessageMXLookupDoneEventHandler ab;

		// Token: 0x04000825 RID: 2085
		[CompilerGenerated]
		private SmtpMessageDirectSendDoneEventHandler ac;

		// Token: 0x04000826 RID: 2086
		private global::a.d.n ad;

		// Token: 0x04000827 RID: 2087
		private bool ae;
	}
}

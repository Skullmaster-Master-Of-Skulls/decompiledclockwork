using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using a;
using a.a;
using MailBee.Mime;
using MailBee.Proxy;
using MailBee.Security;

namespace MailBee.Pop3Mail
{
	// Token: 0x0200057A RID: 1402
	public class Pop3 : IComponent
	{
		// Token: 0x06002E6C RID: 11884 RVA: 0x000DEBD0 File Offset: 0x000DDBD0
		public Pop3() : this(null)
		{
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000DEBDC File Offset: 0x000DDBDC
		public Pop3(string licenseKey)
		{
			this.r = new global::a.a.h(this);
			Pop3.a(licenseKey);
			this.b = null;
			this.s = false;
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
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000DEC7F File Offset: 0x000DDC7F
		public Task<bool> ConnectAsync(string serverName, int port, bool pipelining)
		{
			return this.r.a(serverName, port, pipelining);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000DEC8F File Offset: 0x000DDC8F
		public Task<bool> ConnectAsync(string serverName, int port)
		{
			return this.r.a(serverName, port);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000DEC9E File Offset: 0x000DDC9E
		public Task<bool> ConnectAsync(string serverName)
		{
			return this.r.t(serverName);
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000DECAC File Offset: 0x000DDCAC
		public Task<bool> DisconnectAsync()
		{
			return this.r.my();
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000DECB9 File Offset: 0x000DDCB9
		public Task<bool> LoginAsync(string targetName, string domain, string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.r.a(targetName, domain, accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000DECD1 File Offset: 0x000DDCD1
		public Task<bool> LoginAsync(string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.r.a(accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000DECE5 File Offset: 0x000DDCE5
		public Task<bool> LoginAsync(string accountName, string password, AuthenticationMethods authMethods)
		{
			return this.r.a(accountName, password, authMethods);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000DECF5 File Offset: 0x000DDCF5
		public Task<bool> LoginAsync(string accountName, string password)
		{
			return this.r.g(accountName, password);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000DED04 File Offset: 0x000DDD04
		public Task<bool> StartTlsAsync()
		{
			return this.r.m0();
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000DED11 File Offset: 0x000DDD11
		public Task<bool> NoopAsync()
		{
			return this.r.mz();
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000DED1E File Offset: 0x000DDD1E
		public Task<bool> Noop2Async()
		{
			return this.r.ac();
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000DED2B File Offset: 0x000DDD2B
		public Task<bool> ResetDeletesAsync()
		{
			return this.r.x();
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000DED38 File Offset: 0x000DDD38
		public Task<int> LastAsync()
		{
			return this.r.aa();
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000DED45 File Offset: 0x000DDD45
		public Task<StringDictionary> GetExtensionsAsync()
		{
			return this.r.y();
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000DED52 File Offset: 0x000DDD52
		public Task<AuthenticationMethods> GetSupportedAuthMethodsAsync()
		{
			return this.r.z();
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000DED5F File Offset: 0x000DDD5F
		public Task<bool> DeleteMessageAsync(int index)
		{
			return this.r.c(index);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000DED6D File Offset: 0x000DDD6D
		public Task<bool> DeleteMessagesAsync(int startIndex, int count)
		{
			return this.r.f(startIndex, count);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000DED7C File Offset: 0x000DDD7C
		public Task<bool> DeleteMessagesAsync()
		{
			return this.r.f(-1, -1);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000DED8B File Offset: 0x000DDD8B
		public Task<MailMessage> DownloadEntireMessageAsync(int index)
		{
			return this.r.e(index, -1);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000DED9A File Offset: 0x000DDD9A
		public Task<MailMessageCollection> DownloadEntireMessagesAsync()
		{
			return this.r.c(-1, -1, -1);
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000DEDAA File Offset: 0x000DDDAA
		public Task<MailMessageCollection> DownloadEntireMessagesAsync(int startIndex, int count)
		{
			return this.r.c(startIndex, count, -1);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000DEDBA File Offset: 0x000DDDBA
		public Task<MailMessage> DownloadMessageHeaderAsync(int index)
		{
			return this.r.e(index, 0);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000DEDC9 File Offset: 0x000DDDC9
		public Task<MailMessage> DownloadMessageHeaderAsync(int index, int bodyLineCount)
		{
			return this.r.e(index, bodyLineCount);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000DEDD8 File Offset: 0x000DDDD8
		public Task<MailMessageCollection> DownloadMessageHeadersAsync()
		{
			return this.r.c(-1, -1, 0);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000DEDE8 File Offset: 0x000DDDE8
		public Task<MailMessageCollection> DownloadMessageHeadersAsync(int startIndex, int count)
		{
			return this.r.c(startIndex, count, 0);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000DEDF8 File Offset: 0x000DDDF8
		public Task<MailMessageCollection> DownloadMessageHeadersAsync(int startIndex, int count, int bodyLineCount)
		{
			return this.r.c(startIndex, count, bodyLineCount);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000DEE08 File Offset: 0x000DDE08
		public Task<bool> ExecuteCustomCommandAsync(string commandString, bool multiLineResponse)
		{
			return this.r.c(commandString, multiLineResponse);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000DEE17 File Offset: 0x000DDE17
		public Task<int[]> GetMessageSizesAsync()
		{
			return this.r.af();
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000DEE24 File Offset: 0x000DDE24
		public Task<string[]> GetMessageUidsAsync()
		{
			return this.r.ae();
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000DEE31 File Offset: 0x000DDE31
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000DEE3D File Offset: 0x000DDE3D
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(Pop3));
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000DEE54 File Offset: 0x000DDE54
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000DEE60 File Offset: 0x000DDE60
		// (set) Token: 0x06002E8F RID: 11919 RVA: 0x000DEE72 File Offset: 0x000DDE72
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.r.bp().d();
			}
			set
			{
				this.r.bp().a(value);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000DEE85 File Offset: 0x000DDE85
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000DEE8C File Offset: 0x000DDE8C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000DEE95 File Offset: 0x000DDE95
		protected virtual void Dispose(bool disposing)
		{
			if (!this.s)
			{
				if (disposing)
				{
					this.r.bo();
					if (this.a != null)
					{
						this.a(this, EventArgs.Empty);
					}
				}
				this.s = true;
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06002E93 RID: 11923 RVA: 0x000DEED0 File Offset: 0x000DDED0
		// (remove) Token: 0x06002E94 RID: 11924 RVA: 0x000DEF08 File Offset: 0x000DDF08
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

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000DEF3D File Offset: 0x000DDF3D
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x000DEF45 File Offset: 0x000DDF45
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

		// Token: 0x06002E97 RID: 11927 RVA: 0x000DEF4E File Offset: 0x000DDF4E
		internal bool m()
		{
			return this.c != null;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000DEF59 File Offset: 0x000DDF59
		protected internal void OnErrorOccurred(ErrorEventArgs args)
		{
			this.r.bp().a(this.c, this, args);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000DEF73 File Offset: 0x000DDF73
		internal bool o()
		{
			return this.d != null;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000DEF7E File Offset: 0x000DDF7E
		protected internal void OnLogNewEntry(LogNewEntryEventArgs args)
		{
			this.r.bp().a(this.d, this, args);
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000DEF98 File Offset: 0x000DDF98
		public bool IsBusy
		{
			get
			{
				return this.r.bc();
			}
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000DEFA5 File Offset: 0x000DDFA5
		public void Abort()
		{
			this.r.bd();
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000DEFB2 File Offset: 0x000DDFB2
		[Obsolete("This method is obsolete in .NET 4.5+.")]
		public void Wait()
		{
			this.r.bg();
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000DEFBF File Offset: 0x000DDFBF
		[Obsolete("This method is obsolete in .NET 4.5+.")]
		public bool Wait(int timeoutInterval)
		{
			return this.r.g(timeoutInterval);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000DEFCD File Offset: 0x000DDFCD
		public bool IsAborted
		{
			get
			{
				return this.r.bf();
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000DEFDA File Offset: 0x000DDFDA
		public string GetErrorDescription()
		{
			return this.r.l1();
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000DEFE7 File Offset: 0x000DDFE7
		public int LastResult
		{
			get
			{
				return this.r.l2();
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000DEFF4 File Offset: 0x000DDFF4
		public Logger Log
		{
			get
			{
				return this.r.bi();
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000DF001 File Offset: 0x000DE001
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000DF00E File Offset: 0x000DE00E
		public bool RaiseEvents
		{
			get
			{
				return this.r.bq();
			}
			set
			{
				this.r.k(value);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000DF01C File Offset: 0x000DE01C
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000DF029 File Offset: 0x000DE029
		public bool RaiseEventsViaMessageLoop
		{
			get
			{
				return this.r.bb();
			}
			set
			{
				this.r.j(value);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000DF037 File Offset: 0x000DE037
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x000DF044 File Offset: 0x000DE044
		public Encoding RequestEncoding
		{
			get
			{
				return this.r.bk();
			}
			set
			{
				this.r.lt(value);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000DF052 File Offset: 0x000DE052
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x000DF05F File Offset: 0x000DE05F
		public Encoding ResponseEncoding
		{
			get
			{
				return this.r.bm();
			}
			set
			{
				this.r.lu(value);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000DF06D File Offset: 0x000DE06D
		// (set) Token: 0x06002EAC RID: 11948 RVA: 0x000DF07A File Offset: 0x000DE07A
		public bool ThrowExceptions
		{
			get
			{
				return this.r.be();
			}
			set
			{
				this.r.ls(value);
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000DF088 File Offset: 0x000DE088
		internal bool a()
		{
			return this.e != null;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000DF093 File Offset: 0x000DE093
		protected internal void OnDataReceived(DataTransferEventArgs args)
		{
			this.r.bp().a(this.e, this, args);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000DF0AD File Offset: 0x000DE0AD
		internal bool e()
		{
			return this.f != null;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000DF0B8 File Offset: 0x000DE0B8
		protected internal void OnDataSent(DataTransferEventArgs args)
		{
			this.r.bp().a(this.f, this, args);
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000DF0D2 File Offset: 0x000DE0D2
		public Socket GetSocket()
		{
			return this.r.lv();
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000DF0DF File Offset: 0x000DE0DF
		public Stream GetStream()
		{
			return this.r.ba();
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000DF0EC File Offset: 0x000DE0EC
		public int GetSocketError()
		{
			return this.r.lw();
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000DF0F9 File Offset: 0x000DE0F9
		internal bool g()
		{
			return this.g != null;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000DF104 File Offset: 0x000DE104
		protected internal void OnLowLevelDataReceived(DataTransferEventArgs args)
		{
			this.r.bp().a(this.g, this, args);
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000DF11E File Offset: 0x000DE11E
		internal bool b()
		{
			return this.h != null;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000DF129 File Offset: 0x000DE129
		protected internal void OnLowLevelDataSent(DataTransferEventArgs args)
		{
			this.r.bp().a(this.h, this, args);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000DF143 File Offset: 0x000DE143
		internal bool d()
		{
			return this.i != null;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000DF14E File Offset: 0x000DE14E
		protected internal void OnHostResolved(HostResolvedEventArgs args)
		{
			this.r.bp().a(this.i, this, args);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000DF168 File Offset: 0x000DE168
		internal bool k()
		{
			return this.j != null;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000DF173 File Offset: 0x000DE173
		protected internal void OnSocketCreating(SocketCreatingEventArgs args)
		{
			this.r.bp().a(this.j, this, args);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000DF18D File Offset: 0x000DE18D
		internal bool h()
		{
			return this.k != null;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000DF198 File Offset: 0x000DE198
		protected internal void OnSocketConnected(SocketConnectedEventArgs args)
		{
			this.r.bp().a(this.k, this, args);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000DF1B2 File Offset: 0x000DE1B2
		internal bool c()
		{
			return this.l != null;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000DF1BD File Offset: 0x000DE1BD
		protected internal void OnConnected(ConnectedEventArgs args)
		{
			this.r.bp().a(this.l, this, args);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000DF1D7 File Offset: 0x000DE1D7
		internal bool j()
		{
			return this.m != null;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000DF1E2 File Offset: 0x000DE1E2
		protected internal void OnDisconnected(DisconnectedEventArgs args)
		{
			this.r.bp().a(this.m, this, args);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000DF1FC File Offset: 0x000DE1FC
		internal bool l()
		{
			return this.n != null;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000DF207 File Offset: 0x000DE207
		protected internal void OnTlsStarted(TlsStartedEventArgs args)
		{
			this.r.bp().a(this.n, this, args);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000DF221 File Offset: 0x000DE221
		internal bool f()
		{
			return this.o != null;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000DF22C File Offset: 0x000DE22C
		protected internal void OnLoggedIn(LoggedInEventArgs args)
		{
			this.r.bp().a(this.o, this, args);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000DF246 File Offset: 0x000DE246
		public bool Disconnect()
		{
			return this.r.lo(true);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000DF254 File Offset: 0x000DE254
		[Obsolete("This method is obsolete in .NET 4.5+. Use DisconnectAsync instead.")]
		public IAsyncResult BeginDisconnect(AsyncCallback callback, object state)
		{
			return this.r.lp(callback, state);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000DF263 File Offset: 0x000DE263
		public bool EndDisconnect()
		{
			return this.r.az();
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000DF270 File Offset: 0x000DE270
		public bool Noop()
		{
			return this.r.lq(true);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000DF27E File Offset: 0x000DE27E
		public void ResetState()
		{
			this.r.cb();
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000DF28B File Offset: 0x000DE28B
		public StringDictionary GetExtensions()
		{
			return this.r.ke();
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000DF298 File Offset: 0x000DE298
		public string GetExtension(string name)
		{
			return this.r.kf(name);
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000DF2A6 File Offset: 0x000DE2A6
		public string GetExtensionValue(string name)
		{
			return this.r.kg(name);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000DF2B4 File Offset: 0x000DE2B4
		public string GetServerResponse()
		{
			return this.r.l0();
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000DF2C1 File Offset: 0x000DE2C1
		public AuthenticationMethods GetSupportedAuthMethods()
		{
			return this.r.kh();
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000DF2CE File Offset: 0x000DE2CE
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x000DF2DB File Offset: 0x000DE2DB
		public SslStartupMode SslMode
		{
			get
			{
				return this.r.aq();
			}
			set
			{
				this.r.a(value);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000DF2E9 File Offset: 0x000DE2E9
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x000DF2F6 File Offset: 0x000DE2F6
		public SecurityProtocol SslProtocol
		{
			get
			{
				return this.r.@as();
			}
			set
			{
				this.r.a(value);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x000DF304 File Offset: 0x000DE304
		public ClientServerCertificates SslCertificates
		{
			get
			{
				return this.r.at();
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000DF311 File Offset: 0x000DE311
		public ProxyServer Proxy
		{
			get
			{
				return this.r.ap();
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x000DF31E File Offset: 0x000DE31E
		public bool IsConnected
		{
			get
			{
				return this.r.lx();
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000DF32B File Offset: 0x000DE32B
		public bool IsSslConnection
		{
			get
			{
				return this.r.ly();
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000DF338 File Offset: 0x000DE338
		public bool IsLoggedIn
		{
			get
			{
				return this.r.lz();
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000DF345 File Offset: 0x000DE345
		public bool Connect(string serverName, int port, bool pipelining)
		{
			return this.r.a(true, serverName, port, pipelining);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000DF356 File Offset: 0x000DE356
		public bool Connect(string serverName, int port)
		{
			return this.r.a(true, serverName, port);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000DF366 File Offset: 0x000DE366
		public bool Connect(string serverName)
		{
			return this.r.g(true, serverName);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000DF375 File Offset: 0x000DE375
		[Obsolete("This method is obsolete in .NET 4.5+. Use ConnectAsync instead.")]
		public IAsyncResult BeginConnect(string serverName, int port, bool pipelining, AsyncCallback callback, object state)
		{
			return this.r.a(serverName, port, pipelining, null, null, callback, state);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000DF38B File Offset: 0x000DE38B
		public bool EndConnect()
		{
			return this.r.ar();
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000DF398 File Offset: 0x000DE398
		public bool StartTls()
		{
			return this.r.lr(true);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000DF3A6 File Offset: 0x000DE3A6
		[Obsolete("This method is obsolete in .NET 4.5+. Use StartTlsAsync instead.")]
		public IAsyncResult BeginStartTls(AsyncCallback callback, object state)
		{
			return this.r.e(callback, state);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000DF3B5 File Offset: 0x000DE3B5
		public bool EndStartTls()
		{
			return this.r.a8();
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000DF3C4 File Offset: 0x000DE3C4
		public bool Login(string targetName, string domain, string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.r.a(true, targetName, domain, accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000DF3E8 File Offset: 0x000DE3E8
		public bool Login(string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.r.a(true, accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000DF3FD File Offset: 0x000DE3FD
		public bool Login(string accountName, string password, AuthenticationMethods authMethods)
		{
			return this.r.a(true, accountName, password, authMethods);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000DF40E File Offset: 0x000DE40E
		public bool Login(string accountName, string password)
		{
			return this.r.f(true, accountName, password);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000DF420 File Offset: 0x000DE420
		[Obsolete("This method is obsolete in .NET 4.5+. Use LoginAsync instead.")]
		public IAsyncResult BeginLogin(string targetName, string domain, string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined, AsyncCallback callback, object state)
		{
			return this.r.a(targetName, domain, accountName, password, authMethods, authOptions, authUserDefined, callback, state);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000DF447 File Offset: 0x000DE447
		public bool EndLogin()
		{
			return this.r.au();
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000DF454 File Offset: 0x000DE454
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x000DF461 File Offset: 0x000DE461
		public int Timeout
		{
			get
			{
				return this.r.ao();
			}
			set
			{
				this.r.f(value);
			}
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000DF46F File Offset: 0x000DE46F
		internal bool i()
		{
			return this.p != null;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000DF47A File Offset: 0x000DE47A
		protected internal void OnMessageDownloaded(Pop3MessageDownloadedEventArgs args)
		{
			this.r.bp().a(this.p, this, args);
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000DF494 File Offset: 0x000DE494
		internal bool n()
		{
			return this.q != null;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000DF49F File Offset: 0x000DE49F
		protected internal void OnMessageDataChunkReceived(Pop3MessageDataChunkReceivedEventArgs args)
		{
			this.r.bp().a(this.q, this, args);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000DF4B9 File Offset: 0x000DE4B9
		public bool DeleteMessage(int index)
		{
			return this.r.a(true, index);
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000DF4C8 File Offset: 0x000DE4C8
		[Obsolete("This method is obsolete in .NET 4.5+. Use DeleteMessageAsync instead.")]
		public IAsyncResult BeginDeleteMessage(int index, AsyncCallback callback, object state)
		{
			return this.r.a(index, callback, state);
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000DF4D8 File Offset: 0x000DE4D8
		public bool EndDeleteMessage()
		{
			return this.r.p();
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000DF4E5 File Offset: 0x000DE4E5
		public bool DeleteMessages(int startIndex, int count)
		{
			return this.r.b(true, startIndex, count);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000DF4F5 File Offset: 0x000DE4F5
		public bool DeleteMessages()
		{
			return this.r.b(true, -1, -1);
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000DF505 File Offset: 0x000DE505
		[Obsolete("This method is obsolete in .NET 4.5+. Use DeleteMessagesAsync instead.")]
		public IAsyncResult BeginDeleteMessages(int startIndex, int count, AsyncCallback callback, object state)
		{
			return this.r.b(startIndex, count, callback, state);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000DF517 File Offset: 0x000DE517
		public bool EndDeleteMessages()
		{
			return this.r.q();
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000DF524 File Offset: 0x000DE524
		public MailMessage DownloadEntireMessage(int index)
		{
			return this.r.a(true, index, -1);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000DF534 File Offset: 0x000DE534
		public MailMessage DownloadMessageHeader(int index)
		{
			return this.r.a(true, index, 0);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000DF544 File Offset: 0x000DE544
		public MailMessage DownloadMessageHeader(int index, int bodyLineCount)
		{
			return this.r.a(true, index, bodyLineCount);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000DF554 File Offset: 0x000DE554
		[Obsolete("This method is obsolete in .NET 4.5+. Use DownloadEntireMessageAsync or DownloadMessageHeaderAsync instead.")]
		public IAsyncResult BeginDownloadMessage(int index, int bodyLineCount, AsyncCallback callback, object state)
		{
			return this.r.a(index, bodyLineCount, callback, state);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000DF566 File Offset: 0x000DE566
		public MailMessage EndDownloadMessage()
		{
			return this.r.ab();
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000DF573 File Offset: 0x000DE573
		public MailMessageCollection DownloadEntireMessages(int startIndex, int count)
		{
			return this.r.a(true, startIndex, count, -1);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000DF584 File Offset: 0x000DE584
		public MailMessageCollection DownloadEntireMessages()
		{
			return this.r.a(true, -1, -1, -1);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000DF595 File Offset: 0x000DE595
		public MailMessageCollection DownloadMessageHeaders(int startIndex, int count, int bodyLineCount)
		{
			return this.r.a(true, startIndex, count, bodyLineCount);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000DF5A6 File Offset: 0x000DE5A6
		public MailMessageCollection DownloadMessageHeaders(int startIndex, int count)
		{
			return this.r.a(true, startIndex, count, 0);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000DF5B7 File Offset: 0x000DE5B7
		public MailMessageCollection DownloadMessageHeaders()
		{
			return this.r.a(true, -1, -1, 0);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000DF5C8 File Offset: 0x000DE5C8
		[Obsolete("This method is obsolete in .NET 4.5+. Use DownloadEntireMessagesAsync or DownloadMessageHeadersAsync instead.")]
		public IAsyncResult BeginDownloadMessages(int startIndex, int count, int bodyLineCount, AsyncCallback callback, object state)
		{
			return this.r.a(startIndex, count, bodyLineCount, callback, state);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000DF5DC File Offset: 0x000DE5DC
		public MailMessageCollection EndDownloadMessages()
		{
			return this.r.w();
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000DF5E9 File Offset: 0x000DE5E9
		public bool ExecuteCustomCommand(string commandString, bool multiLineResponse)
		{
			return this.r.a(true, commandString, multiLineResponse);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000DF5F9 File Offset: 0x000DE5F9
		[Obsolete("This method is obsolete in .NET 4.5+. Use ExecuteCustomCommandAsync instead.")]
		public IAsyncResult BeginExecuteCustomCommand(string commandString, bool multiLineResponse, AsyncCallback callback, object state)
		{
			return this.r.a(commandString, multiLineResponse, callback, state);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000DF60B File Offset: 0x000DE60B
		public bool EndExecuteCustomCommand()
		{
			return this.r.s();
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000DF618 File Offset: 0x000DE618
		public bool Noop2()
		{
			return this.r.c(true);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000DF626 File Offset: 0x000DE626
		public bool ResetDeletes()
		{
			return this.r.f(true);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000DF634 File Offset: 0x000DE634
		public int Last()
		{
			return this.r.d(true);
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000DF642 File Offset: 0x000DE642
		public long InboxSize
		{
			get
			{
				return this.r.o();
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000DF64F File Offset: 0x000DE64F
		public int InboxMessageCount
		{
			get
			{
				return this.r.r();
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000DF65C File Offset: 0x000DE65C
		public int GetMessageSize(int index)
		{
			return this.r.e(index);
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000DF66A File Offset: 0x000DE66A
		public int[] GetMessageSizes()
		{
			return this.r.ad();
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000DF677 File Offset: 0x000DE677
		public int GetMessageIndexFromUid(string uid)
		{
			return this.r.a(uid);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000DF685 File Offset: 0x000DE685
		public string GetMessageUidFromIndex(int index)
		{
			return this.r.d(index);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000DF693 File Offset: 0x000DE693
		public string[] GetMessageUids()
		{
			return this.r.t();
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000DF6A0 File Offset: 0x000DE6A0
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000DF6AD File Offset: 0x000DE6AD
		public Pop3InboxPreloadOptions InboxPreloadOptions
		{
			get
			{
				return this.r.ag();
			}
			set
			{
				this.r.a(value);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000DF6BB File Offset: 0x000DE6BB
		// (set) Token: 0x06002F10 RID: 12048 RVA: 0x000DF6C8 File Offset: 0x000DE6C8
		public bool EnableLastDownloaded
		{
			get
			{
				return this.r.u();
			}
			set
			{
				this.r.e(value);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x000DF6D6 File Offset: 0x000DE6D6
		public MailMessageCollection LastDownloadedMessages
		{
			get
			{
				return this.r.v();
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000DF6E3 File Offset: 0x000DE6E3
		private static void a(string A_0)
		{
			Global.a(typeof(Pop3), A_0);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000DF6F8 File Offset: 0x000DE6F8
		public static MailMessage QuickDownloadMessage(string serverName, string accountName, string password, int index, int bodyLineCount)
		{
			Pop3.a(null);
			global::a.a.c c = new global::a.a.c(null, null, new Logger(null), 0);
			c.av().e(serverName);
			c.av().c(accountName);
			c.av().d(password);
			c.fy();
			c.fo();
			MailMessage result = c.c(index, bodyLineCount);
			c.fz(true);
			return result;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000DF75A File Offset: 0x000DE75A
		public static MailMessage QuickDownloadMessage(string serverName, string accountName, string password, int index)
		{
			return Pop3.QuickDownloadMessage(serverName, accountName, password, index, -1);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000DF768 File Offset: 0x000DE768
		public static MailMessageCollection QuickDownloadMessages(string serverName, string accountName, string password, int bodyLineCount)
		{
			Pop3.a(null);
			global::a.a.c c = new global::a.a.c(null, null, new Logger(null), 0);
			c.av().e(serverName);
			c.av().c(accountName);
			c.av().d(password);
			c.fy();
			c.fo();
			MailMessageCollection result = c.b(-1, -1, bodyLineCount);
			c.fz(true);
			return result;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000DF7CA File Offset: 0x000DE7CA
		public static MailMessageCollection QuickDownloadMessages(string serverName, string accountName, string password)
		{
			return Pop3.QuickDownloadMessages(serverName, accountName, password, -1);
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06002F17 RID: 12055 RVA: 0x000DF7D8 File Offset: 0x000DE7D8
		// (remove) Token: 0x06002F18 RID: 12056 RVA: 0x000DF810 File Offset: 0x000DE810
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

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06002F19 RID: 12057 RVA: 0x000DF848 File Offset: 0x000DE848
		// (remove) Token: 0x06002F1A RID: 12058 RVA: 0x000DF880 File Offset: 0x000DE880
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

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002F1B RID: 12059 RVA: 0x000DF8B8 File Offset: 0x000DE8B8
		// (remove) Token: 0x06002F1C RID: 12060 RVA: 0x000DF8F0 File Offset: 0x000DE8F0
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

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002F1D RID: 12061 RVA: 0x000DF928 File Offset: 0x000DE928
		// (remove) Token: 0x06002F1E RID: 12062 RVA: 0x000DF960 File Offset: 0x000DE960
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

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002F1F RID: 12063 RVA: 0x000DF998 File Offset: 0x000DE998
		// (remove) Token: 0x06002F20 RID: 12064 RVA: 0x000DF9D0 File Offset: 0x000DE9D0
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

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06002F21 RID: 12065 RVA: 0x000DFA08 File Offset: 0x000DEA08
		// (remove) Token: 0x06002F22 RID: 12066 RVA: 0x000DFA40 File Offset: 0x000DEA40
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

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06002F23 RID: 12067 RVA: 0x000DFA78 File Offset: 0x000DEA78
		// (remove) Token: 0x06002F24 RID: 12068 RVA: 0x000DFAB0 File Offset: 0x000DEAB0
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

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06002F25 RID: 12069 RVA: 0x000DFAE8 File Offset: 0x000DEAE8
		// (remove) Token: 0x06002F26 RID: 12070 RVA: 0x000DFB20 File Offset: 0x000DEB20
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

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06002F27 RID: 12071 RVA: 0x000DFB58 File Offset: 0x000DEB58
		// (remove) Token: 0x06002F28 RID: 12072 RVA: 0x000DFB90 File Offset: 0x000DEB90
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

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002F29 RID: 12073 RVA: 0x000DFBC8 File Offset: 0x000DEBC8
		// (remove) Token: 0x06002F2A RID: 12074 RVA: 0x000DFC00 File Offset: 0x000DEC00
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

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002F2B RID: 12075 RVA: 0x000DFC38 File Offset: 0x000DEC38
		// (remove) Token: 0x06002F2C RID: 12076 RVA: 0x000DFC70 File Offset: 0x000DEC70
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

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06002F2D RID: 12077 RVA: 0x000DFCA8 File Offset: 0x000DECA8
		// (remove) Token: 0x06002F2E RID: 12078 RVA: 0x000DFCE0 File Offset: 0x000DECE0
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

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002F2F RID: 12079 RVA: 0x000DFD18 File Offset: 0x000DED18
		// (remove) Token: 0x06002F30 RID: 12080 RVA: 0x000DFD50 File Offset: 0x000DED50
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

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06002F31 RID: 12081 RVA: 0x000DFD88 File Offset: 0x000DED88
		// (remove) Token: 0x06002F32 RID: 12082 RVA: 0x000DFDC0 File Offset: 0x000DEDC0
		public event Pop3MessageDownloadedEventHandler MessageDownloaded
		{
			[CompilerGenerated]
			add
			{
				Pop3MessageDownloadedEventHandler pop3MessageDownloadedEventHandler = this.p;
				Pop3MessageDownloadedEventHandler pop3MessageDownloadedEventHandler2;
				do
				{
					pop3MessageDownloadedEventHandler2 = pop3MessageDownloadedEventHandler;
					Pop3MessageDownloadedEventHandler value2 = (Pop3MessageDownloadedEventHandler)Delegate.Combine(pop3MessageDownloadedEventHandler2, value);
					pop3MessageDownloadedEventHandler = Interlocked.CompareExchange<Pop3MessageDownloadedEventHandler>(ref this.p, value2, pop3MessageDownloadedEventHandler2);
				}
				while (pop3MessageDownloadedEventHandler != pop3MessageDownloadedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				Pop3MessageDownloadedEventHandler pop3MessageDownloadedEventHandler = this.p;
				Pop3MessageDownloadedEventHandler pop3MessageDownloadedEventHandler2;
				do
				{
					pop3MessageDownloadedEventHandler2 = pop3MessageDownloadedEventHandler;
					Pop3MessageDownloadedEventHandler value2 = (Pop3MessageDownloadedEventHandler)Delegate.Remove(pop3MessageDownloadedEventHandler2, value);
					pop3MessageDownloadedEventHandler = Interlocked.CompareExchange<Pop3MessageDownloadedEventHandler>(ref this.p, value2, pop3MessageDownloadedEventHandler2);
				}
				while (pop3MessageDownloadedEventHandler != pop3MessageDownloadedEventHandler2);
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002F33 RID: 12083 RVA: 0x000DFDF8 File Offset: 0x000DEDF8
		// (remove) Token: 0x06002F34 RID: 12084 RVA: 0x000DFE30 File Offset: 0x000DEE30
		public event Pop3MessageDataChunkReceivedEventHandler MessageDataChunkReceived
		{
			[CompilerGenerated]
			add
			{
				Pop3MessageDataChunkReceivedEventHandler pop3MessageDataChunkReceivedEventHandler = this.q;
				Pop3MessageDataChunkReceivedEventHandler pop3MessageDataChunkReceivedEventHandler2;
				do
				{
					pop3MessageDataChunkReceivedEventHandler2 = pop3MessageDataChunkReceivedEventHandler;
					Pop3MessageDataChunkReceivedEventHandler value2 = (Pop3MessageDataChunkReceivedEventHandler)Delegate.Combine(pop3MessageDataChunkReceivedEventHandler2, value);
					pop3MessageDataChunkReceivedEventHandler = Interlocked.CompareExchange<Pop3MessageDataChunkReceivedEventHandler>(ref this.q, value2, pop3MessageDataChunkReceivedEventHandler2);
				}
				while (pop3MessageDataChunkReceivedEventHandler != pop3MessageDataChunkReceivedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				Pop3MessageDataChunkReceivedEventHandler pop3MessageDataChunkReceivedEventHandler = this.q;
				Pop3MessageDataChunkReceivedEventHandler pop3MessageDataChunkReceivedEventHandler2;
				do
				{
					pop3MessageDataChunkReceivedEventHandler2 = pop3MessageDataChunkReceivedEventHandler;
					Pop3MessageDataChunkReceivedEventHandler value2 = (Pop3MessageDataChunkReceivedEventHandler)Delegate.Remove(pop3MessageDataChunkReceivedEventHandler2, value);
					pop3MessageDataChunkReceivedEventHandler = Interlocked.CompareExchange<Pop3MessageDataChunkReceivedEventHandler>(ref this.q, value2, pop3MessageDataChunkReceivedEventHandler2);
				}
				while (pop3MessageDataChunkReceivedEventHandler != pop3MessageDataChunkReceivedEventHandler2);
			}
		}

		// Token: 0x04001FE3 RID: 8163
		[CompilerGenerated]
		private EventHandler a;

		// Token: 0x04001FE4 RID: 8164
		private ISite b;

		// Token: 0x04001FE5 RID: 8165
		[CompilerGenerated]
		private ErrorEventHandler c;

		// Token: 0x04001FE6 RID: 8166
		[CompilerGenerated]
		private LogNewEntryEventHandler d;

		// Token: 0x04001FE7 RID: 8167
		[CompilerGenerated]
		private DataTransferEventHandler e;

		// Token: 0x04001FE8 RID: 8168
		[CompilerGenerated]
		private DataTransferEventHandler f;

		// Token: 0x04001FE9 RID: 8169
		[CompilerGenerated]
		private DataTransferEventHandler g;

		// Token: 0x04001FEA RID: 8170
		[CompilerGenerated]
		private DataTransferEventHandler h;

		// Token: 0x04001FEB RID: 8171
		[CompilerGenerated]
		private HostResolvedEventHandler i;

		// Token: 0x04001FEC RID: 8172
		[CompilerGenerated]
		private SocketCreatingEventHandler j;

		// Token: 0x04001FED RID: 8173
		[CompilerGenerated]
		private SocketConnectedEventHandler k;

		// Token: 0x04001FEE RID: 8174
		[CompilerGenerated]
		private ConnectedEventHandler l;

		// Token: 0x04001FEF RID: 8175
		[CompilerGenerated]
		private DisconnectedEventHandler m;

		// Token: 0x04001FF0 RID: 8176
		[CompilerGenerated]
		private TlsStartedEventHandler n;

		// Token: 0x04001FF1 RID: 8177
		[CompilerGenerated]
		private LoggedInEventHandler o;

		// Token: 0x04001FF2 RID: 8178
		[CompilerGenerated]
		private Pop3MessageDownloadedEventHandler p;

		// Token: 0x04001FF3 RID: 8179
		[CompilerGenerated]
		private Pop3MessageDataChunkReceivedEventHandler q;

		// Token: 0x04001FF4 RID: 8180
		private global::a.a.h r;

		// Token: 0x04001FF5 RID: 8181
		private bool s;
	}
}

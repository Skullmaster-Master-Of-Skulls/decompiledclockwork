using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Timers;
using TechnoPro.ClockWorkServer.Client.CallbackContracts;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core
{
	// Token: 0x02000002 RID: 2
	public class MessagingManager : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		public event OnUserLoginEventHandler OnUserLogin;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event OnUserLogoutEventHandler OnUserLogout;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event OnMessageDeliveryEventHandler OnMessageDelivery;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000021A0 File Offset: 0x000003A0
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000021D8 File Offset: 0x000003D8
		public event OnAttachmentReceivedEventHandler OnAttachmentReceived;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000220D File Offset: 0x0000040D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002214 File Offset: 0x00000414
		public static string UserAppData_Path { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000221C File Offset: 0x0000041C
		public static string UserAppDownloadsPath
		{
			get
			{
				return Path.Combine(MessagingManager.UserAppData_Path, "Downloads");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000222D File Offset: 0x0000042D
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002238 File Offset: 0x00000438
		public MessagingReusableClientProxy MessagingClient
		{
			get
			{
				return this._messagingClient;
			}
			private set
			{
				this._messagingClient = value;
				if (this._messagingClient != null)
				{
					this._messagingClient.MessagingCallback.OnUserLogin += this._onUserLogin;
					this._messagingClient.MessagingCallback.OnUserLogout += this._onUserLogout;
					this._messagingClient.MessagingCallback.OnMessageDelivery += this._onMessageDelivery;
					this._messagingClient.MessagingCallback.OnAttachmentReceived += this._onAttachmentReceived;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022CB File Offset: 0x000004CB
		protected static MessagingManager _instance { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022D3 File Offset: 0x000004D3
		public static MessagingManager CurrentInstance
		{
			get
			{
				if (MessagingManager._instance == null)
				{
					MessagingManager._instance = new MessagingManager();
				}
				return MessagingManager._instance;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022EB File Offset: 0x000004EB
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022F3 File Offset: 0x000004F3
		private Timer checkConnectivityTimer { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000022FC File Offset: 0x000004FC
		static MessagingManager()
		{
			string text = string.Empty;
			try
			{
				text = Assembly.GetEntryAssembly().GetName().Name;
				if (string.IsNullOrEmpty(MessagingManager.UserAppData_Path))
				{
					string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TechnoPro");
					if (!Directory.Exists(text2))
					{
						Directory.CreateDirectory(text2);
					}
					MessagingManager.UserAppData_Path = Path.Combine(text2, text);
				}
				if (!Directory.Exists(MessagingManager.UserAppData_Path))
				{
					Directory.CreateDirectory(MessagingManager.UserAppData_Path);
				}
				if (!Directory.Exists(MessagingManager.UserAppDownloadsPath))
				{
					Directory.CreateDirectory(MessagingManager.UserAppDownloadsPath);
				}
			}
			catch (Exception)
			{
				MessagingManager.UserAppData_Path = Path.GetTempPath() + Path.DirectorySeparatorChar.ToString() + text;
				Directory.CreateDirectory(MessagingManager.UserAppData_Path);
				if (!Directory.Exists(MessagingManager.UserAppDownloadsPath))
				{
					Directory.CreateDirectory(MessagingManager.UserAppDownloadsPath);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023DC File Offset: 0x000005DC
		protected MessagingManager()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			this.MessagingClient = (clientCache.IsClockWorkServerEnable ? (WCFClientProxy<IMessaging>.GetReusableInstance() as MessagingReusableClientProxy) : null);
			if (this.MessagingClient != null)
			{
				this.checkConnectivityTimer = new Timer();
				this.checkConnectivityTimer.Elapsed += MessagingManager.CheckConnectivityTimerElapsed;
				this.checkConnectivityTimer.Interval = this._defaultCheckConnectivityInterval.TotalMilliseconds;
				this.checkConnectivityTimer.Enabled = true;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000246C File Offset: 0x0000066C
		public void Dispose()
		{
			if (this.checkConnectivityTimer != null)
			{
				this.checkConnectivityTimer.Elapsed -= MessagingManager.CheckConnectivityTimerElapsed;
				this.checkConnectivityTimer.Dispose();
			}
			this.removeEventSubscribers();
			if (this.MessagingClient != null)
			{
				((IDisposable)this.MessagingClient).Dispose();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024BC File Offset: 0x000006BC
		private static void CheckConnectivityTimerElapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				if (MessagingManager.CurrentInstance.MessagingClient != null)
				{
					if (MessagingManager.CurrentInstance.MessagingClient.InnerChannel.State != CommunicationState.Opening)
					{
						MessagingManager.CurrentInstance.MessagingClient.CheckConnectivity();
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002514 File Offset: 0x00000714
		private void removeEventSubscribers()
		{
			if (this.MessagingClient != null)
			{
				this.MessagingClient.MessagingCallback.OnUserLogin -= this._onUserLogin;
				this.MessagingClient.MessagingCallback.OnUserLogout -= this._onUserLogout;
				this.MessagingClient.MessagingCallback.OnMessageDelivery -= this._onMessageDelivery;
				this.MessagingClient.MessagingCallback.OnAttachmentReceived -= this._onAttachmentReceived;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000259C File Offset: 0x0000079C
		private void _onAttachmentReceived(AttachmentInfo att)
		{
			OnAttachmentReceivedEventHandler onAttachmentReceived = this.OnAttachmentReceived;
			if (onAttachmentReceived != null)
			{
				onAttachmentReceived(att);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025BC File Offset: 0x000007BC
		private void _onMessageDelivery(InstantMessage msg)
		{
			OnMessageDeliveryEventHandler onMessageDelivery = this.OnMessageDelivery;
			if (onMessageDelivery != null)
			{
				onMessageDelivery(msg);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025DC File Offset: 0x000007DC
		private void _onUserLogout(string username)
		{
			OnUserLogoutEventHandler onUserLogout = this.OnUserLogout;
			if (onUserLogout != null)
			{
				onUserLogout(username);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025FC File Offset: 0x000007FC
		private void _onUserLogin(IM_User user)
		{
			OnUserLoginEventHandler onUserLogin = this.OnUserLogin;
			if (onUserLogin != null)
			{
				onUserLogin(user);
			}
		}

		// Token: 0x04000006 RID: 6
		private MessagingReusableClientProxy _messagingClient;

		// Token: 0x04000008 RID: 8
		private readonly TimeSpan _defaultCheckConnectivityInterval = new TimeSpan(0, 1, 0);
	}
}

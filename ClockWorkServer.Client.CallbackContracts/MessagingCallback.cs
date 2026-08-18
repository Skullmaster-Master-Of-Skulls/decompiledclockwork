using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.CallbackContracts
{
	// Token: 0x02000006 RID: 6
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
	public class MessagingCallback : IMessagingCallback
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000011 RID: 17 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000012 RID: 18 RVA: 0x00002088 File Offset: 0x00000288
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnUserLoginEventHandler OnUserLogin;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000013 RID: 19 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x000020F8 File Offset: 0x000002F8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnUserLogoutEventHandler OnUserLogout;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000015 RID: 21 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x00002168 File Offset: 0x00000368
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnMessageDeliveryEventHandler OnMessageDelivery;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000017 RID: 23 RVA: 0x000021A0 File Offset: 0x000003A0
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x000021D8 File Offset: 0x000003D8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnAttachmentReceivedEventHandler OnAttachmentReceived;

		// Token: 0x06000019 RID: 25 RVA: 0x0000220D File Offset: 0x0000040D
		public MessagingCallback()
		{
			this._syncContext = (SynchronizationContext.Current ?? new SynchronizationContext());
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000222C File Offset: 0x0000042C
		public void NotifyLogin(IM_User user)
		{
			SendOrPostCallback d = delegate(object s)
			{
				OnUserLoginEventHandler onUserLogin = this.OnUserLogin;
				bool flag = onUserLogin != null;
				if (flag)
				{
					onUserLogin(s as IM_User);
				}
			};
			this._syncContext.Post(d, user);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002258 File Offset: 0x00000458
		public void MessageDelivery(InstantMessage msg)
		{
			SendOrPostCallback d = delegate(object obj)
			{
				OnMessageDeliveryEventHandler onMessageDelivery = this.OnMessageDelivery;
				bool flag = onMessageDelivery != null;
				if (flag)
				{
					onMessageDelivery((InstantMessage)obj);
				}
			};
			this._syncContext.Post(d, msg);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002284 File Offset: 0x00000484
		public void NotifyLogout(string username)
		{
			SendOrPostCallback d = delegate(object s)
			{
				OnUserLogoutEventHandler onUserLogout = this.OnUserLogout;
				bool flag = onUserLogout != null;
				if (flag)
				{
					onUserLogout(s.ToString());
				}
			};
			this._syncContext.Post(d, username);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B0 File Offset: 0x000004B0
		public void NotifyAttachment(AttachmentInfo attInfo)
		{
			SendOrPostCallback d = delegate(object obj)
			{
				OnAttachmentReceivedEventHandler onAttachmentReceived = this.OnAttachmentReceived;
				bool flag = onAttachmentReceived != null;
				if (flag)
				{
					onAttachmentReceived(obj as AttachmentInfo);
				}
			};
			this._syncContext.Post(d, attInfo);
		}

		// Token: 0x04000001 RID: 1
		private SynchronizationContext _syncContext;
	}
}

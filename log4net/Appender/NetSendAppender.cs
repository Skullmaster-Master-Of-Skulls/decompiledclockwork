using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000032 RID: 50
	public class NetSendAppender : AppenderSkeleton
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006174 File Offset: 0x00004374
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000617C File Offset: 0x0000437C
		public string Sender
		{
			get
			{
				return this.m_sender;
			}
			set
			{
				this.m_sender = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006185 File Offset: 0x00004385
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000618D File Offset: 0x0000438D
		public string Recipient
		{
			get
			{
				return this.m_recipient;
			}
			set
			{
				this.m_recipient = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006196 File Offset: 0x00004396
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000619E File Offset: 0x0000439E
		public string Server
		{
			get
			{
				return this.m_server;
			}
			set
			{
				this.m_server = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000061A7 File Offset: 0x000043A7
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000061AF File Offset: 0x000043AF
		public log4net.Core.SecurityContext SecurityContext
		{
			get
			{
				return this.m_securityContext;
			}
			set
			{
				this.m_securityContext = value;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000061B8 File Offset: 0x000043B8
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.Recipient == null)
			{
				throw new ArgumentNullException("Recipient", "The required property 'Recipient' was not specified.");
			}
			if (this.m_securityContext == null)
			{
				this.m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000061F4 File Offset: 0x000043F4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		protected override void Append(LoggingEvent loggingEvent)
		{
			NativeError nativeError = null;
			string text = base.RenderLoggingEvent(loggingEvent);
			using (this.m_securityContext.Impersonate(this))
			{
				int num = NetSendAppender.NetMessageBufferSend(this.Server, this.Recipient, this.Sender, text, text.Length * Marshal.SystemDefaultCharSize);
				if (num != 0)
				{
					nativeError = NativeError.GetError(num);
				}
			}
			if (nativeError != null)
			{
				this.ErrorHandler.Error(string.Concat(new string[]
				{
					nativeError.ToString(),
					" (Params: Server=",
					this.Server,
					", Recipient=",
					this.Recipient,
					", Sender=",
					this.Sender,
					")"
				}));
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000062CC File Offset: 0x000044CC
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001D1 RID: 465
		[DllImport("netapi32.dll", SetLastError = true)]
		protected static extern int NetMessageBufferSend([MarshalAs(UnmanagedType.LPWStr)] string serverName, [MarshalAs(UnmanagedType.LPWStr)] string msgName, [MarshalAs(UnmanagedType.LPWStr)] string fromName, [MarshalAs(UnmanagedType.LPWStr)] string buffer, int bufferSize);

		// Token: 0x040000C9 RID: 201
		private string m_server;

		// Token: 0x040000CA RID: 202
		private string m_sender;

		// Token: 0x040000CB RID: 203
		private string m_recipient;

		// Token: 0x040000CC RID: 204
		private log4net.Core.SecurityContext m_securityContext;
	}
}

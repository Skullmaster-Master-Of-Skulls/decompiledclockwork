using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x0200003D RID: 61
	public class SocketConnectedEventArgs : CommonEventArgs
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00007F51 File Offset: 0x00006F51
		internal SocketConnectedEventArgs(ai A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007F61 File Offset: 0x00006F61
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.a.d();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007F6E File Offset: 0x00006F6E
		public string RemoteHostName
		{
			get
			{
				return this.a.b();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007F7B File Offset: 0x00006F7B
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.a.e();
			}
		}

		// Token: 0x0400015C RID: 348
		private ai a;
	}
}

using System;
using System.Net;
using System.Net.Sockets;
using a;

namespace MailBee
{
	// Token: 0x0200003B RID: 59
	public class SocketCreatingEventArgs : CommonEventArgs
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00007EF8 File Offset: 0x00006EF8
		internal SocketCreatingEventArgs(ac A_0, ai A_1, bc A_2) : base(A_2)
		{
			this.a = A_1;
			this.b = A_0;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007F0F File Offset: 0x00006F0F
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.a.d();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00007F1C File Offset: 0x00006F1C
		public string RemoteHostName
		{
			get
			{
				return this.a.b();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00007F29 File Offset: 0x00006F29
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.a.e();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007F36 File Offset: 0x00006F36
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007F43 File Offset: 0x00006F43
		public Socket NewSocket
		{
			get
			{
				return this.b.a();
			}
			set
			{
				this.b.a(value);
			}
		}

		// Token: 0x0400015A RID: 346
		private ai a;

		// Token: 0x0400015B RID: 347
		private ac b;
	}
}

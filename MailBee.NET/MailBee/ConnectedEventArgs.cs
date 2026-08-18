using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x0200005A RID: 90
	public class ConnectedEventArgs : CommonEventArgs
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00008E0B File Offset: 0x00007E0B
		internal ConnectedEventArgs(ai A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00008E1B File Offset: 0x00007E1B
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.a.d();
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00008E28 File Offset: 0x00007E28
		public string RemoteHostName
		{
			get
			{
				return this.a.b();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00008E35 File Offset: 0x00007E35
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.a.e();
			}
		}

		// Token: 0x04000165 RID: 357
		private ai a;
	}
}

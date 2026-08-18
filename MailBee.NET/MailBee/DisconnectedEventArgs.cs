using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x0200005C RID: 92
	public class DisconnectedEventArgs : CommonEventArgs
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00008E42 File Offset: 0x00007E42
		internal DisconnectedEventArgs(bool A_0, ai A_1, bc A_2) : base(A_2)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00008E59 File Offset: 0x00007E59
		public bool IsNormalShutdown
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00008E61 File Offset: 0x00007E61
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.b.d();
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00008E6E File Offset: 0x00007E6E
		public string RemoteHostName
		{
			get
			{
				return this.b.b();
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00008E7B File Offset: 0x00007E7B
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.b.e();
			}
		}

		// Token: 0x04000166 RID: 358
		private bool a;

		// Token: 0x04000167 RID: 359
		private ai b;
	}
}

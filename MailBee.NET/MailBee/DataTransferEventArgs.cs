using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x0200003F RID: 63
	public class DataTransferEventArgs : CommonEventArgs
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00007F88 File Offset: 0x00006F88
		internal DataTransferEventArgs(byte[] A_0, ai A_1, bc A_2) : base(A_2)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007F9F File Offset: 0x00006F9F
		public byte[] Data
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007FA7 File Offset: 0x00006FA7
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.b.d();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007FB4 File Offset: 0x00006FB4
		public string RemoteHostName
		{
			get
			{
				return this.b.b();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007FC1 File Offset: 0x00006FC1
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.b.e();
			}
		}

		// Token: 0x0400015D RID: 349
		private byte[] a;

		// Token: 0x0400015E RID: 350
		private ai b;
	}
}

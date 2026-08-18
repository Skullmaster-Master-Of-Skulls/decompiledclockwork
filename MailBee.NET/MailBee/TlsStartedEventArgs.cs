using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x0200005E RID: 94
	public class TlsStartedEventArgs : CommonEventArgs
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00008E88 File Offset: 0x00007E88
		internal TlsStartedEventArgs(ai A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00008E98 File Offset: 0x00007E98
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.a.d();
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00008EA5 File Offset: 0x00007EA5
		public string RemoteHostName
		{
			get
			{
				return this.a.b();
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00008EB2 File Offset: 0x00007EB2
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.a.e();
			}
		}

		// Token: 0x04000168 RID: 360
		private ai a;
	}
}

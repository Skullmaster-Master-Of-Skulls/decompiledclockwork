using System;
using System.Net;
using a;

namespace MailBee
{
	// Token: 0x02000060 RID: 96
	public class LoggedInEventArgs : CommonEventArgs
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00008EBF File Offset: 0x00007EBF
		internal LoggedInEventArgs(ai A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00008ECF File Offset: 0x00007ECF
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.a.d();
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00008EDC File Offset: 0x00007EDC
		public string RemoteHostName
		{
			get
			{
				return this.a.b();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00008EE9 File Offset: 0x00007EE9
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.a.e();
			}
		}

		// Token: 0x04000169 RID: 361
		private ai a;
	}
}

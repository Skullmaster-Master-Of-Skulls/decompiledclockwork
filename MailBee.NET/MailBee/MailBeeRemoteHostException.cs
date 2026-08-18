using System;
using System.Net;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000047 RID: 71
	public abstract class MailBeeRemoteHostException : MailBeeNetworkException
	{
		// Token: 0x060001AB RID: 427 RVA: 0x000080C9 File Offset: 0x000070C9
		internal MailBeeRemoteHostException(string A_0, int A_1, ai A_2) : base(A_0, A_1)
		{
			this.a = A_2;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000080DA File Offset: 0x000070DA
		internal MailBeeRemoteHostException(int A_0, ai A_1) : base(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000080EA File Offset: 0x000070EA
		internal MailBeeRemoteHostException(string A_0, int A_1, Exception A_2, ai A_3) : base(A_0, A_1, A_2)
		{
			this.a = A_3;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000080FD File Offset: 0x000070FD
		internal MailBeeRemoteHostException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1)
		{
			this.a = A_2;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000810E File Offset: 0x0000710E
		protected MailBeeRemoteHostException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008118 File Offset: 0x00007118
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.a.d();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008125 File Offset: 0x00007125
		public string RemoteHostName
		{
			get
			{
				return this.a.b();
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008132 File Offset: 0x00007132
		public TopLevelProtocolType Protocol
		{
			get
			{
				return this.a.e();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000813F File Offset: 0x0000713F
		public bool WasConnected
		{
			get
			{
				return this.a.c();
			}
		}

		// Token: 0x04000163 RID: 355
		private ai a;
	}
}

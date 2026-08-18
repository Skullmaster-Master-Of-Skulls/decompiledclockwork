using System;
using System.Net;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class MailBeeNoIP4HostFoundException : MailBeeNetworkException
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00008000 File Offset: 0x00007000
		internal MailBeeNoIP4HostFoundException(int A_0, IPHostEntry A_1, int A_2) : base(A_0)
		{
			this.m_hostEntry = A_1;
			this.m_port = A_2;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008017 File Offset: 0x00007017
		protected MailBeeNoIP4HostFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00008021 File Offset: 0x00007021
		public IPHostEntry HostEntry
		{
			get
			{
				return this.m_hostEntry;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008029 File Offset: 0x00007029
		public int Port
		{
			get
			{
				return this.m_port;
			}
		}

		// Token: 0x0400015F RID: 351
		private IPHostEntry m_hostEntry;

		// Token: 0x04000160 RID: 352
		private int m_port;
	}
}

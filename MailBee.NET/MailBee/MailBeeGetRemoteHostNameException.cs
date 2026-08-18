using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public class MailBeeGetRemoteHostNameException : MailBeeGetHostNameException
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00008059 File Offset: 0x00007059
		internal MailBeeGetRemoteHostNameException(int A_0, Exception A_1, string A_2, TopLevelProtocolType A_3) : base(A_0, A_1)
		{
			this.m_hostName = A_2;
			this.m_hostProtocol = A_3;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008072 File Offset: 0x00007072
		protected MailBeeGetRemoteHostNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000807C File Offset: 0x0000707C
		public string HostName
		{
			get
			{
				return this.m_hostName;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008084 File Offset: 0x00007084
		public TopLevelProtocolType HostProtocol
		{
			get
			{
				return this.m_hostProtocol;
			}
		}

		// Token: 0x04000161 RID: 353
		private string m_hostName;

		// Token: 0x04000162 RID: 354
		private TopLevelProtocolType m_hostProtocol;
	}
}

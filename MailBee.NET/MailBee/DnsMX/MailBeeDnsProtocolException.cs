using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000572 RID: 1394
	[Serializable]
	public class MailBeeDnsProtocolException : MailBeeProtocolException
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x000DE6E6 File Offset: 0x000DD6E6
		internal MailBeeDnsProtocolException(string A_0, int A_1, ai A_2, string A_3) : base(A_0, A_1, A_2)
		{
			this.m_hostName = A_3;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000DE6F9 File Offset: 0x000DD6F9
		internal MailBeeDnsProtocolException(int A_0, ai A_1, string A_2) : base(A_0, A_1)
		{
			this.m_hostName = A_2;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000DE70A File Offset: 0x000DD70A
		protected MailBeeDnsProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x000DE714 File Offset: 0x000DD714
		public string HostName
		{
			get
			{
				return this.m_hostName;
			}
		}

		// Token: 0x04001FCD RID: 8141
		private string m_hostName;
	}
}

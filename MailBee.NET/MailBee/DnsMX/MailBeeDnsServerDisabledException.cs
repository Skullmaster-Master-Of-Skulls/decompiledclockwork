using System;
using System.Runtime.Serialization;

namespace MailBee.DnsMX
{
	// Token: 0x02000570 RID: 1392
	[Serializable]
	public class MailBeeDnsServerDisabledException : MailBeeLocalException
	{
		// Token: 0x06002E34 RID: 11828 RVA: 0x000DE67B File Offset: 0x000DD67B
		internal MailBeeDnsServerDisabledException(int A_0, DnsServer A_1) : base(A_0)
		{
			this.m_server = A_1;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000DE68B File Offset: 0x000DD68B
		protected MailBeeDnsServerDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000DE695 File Offset: 0x000DD695
		public DnsServer Server
		{
			get
			{
				return this.m_server;
			}
		}

		// Token: 0x04001FCB RID: 8139
		private DnsServer m_server;
	}
}

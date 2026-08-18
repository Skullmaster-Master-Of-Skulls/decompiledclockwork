using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000576 RID: 1398
	[Serializable]
	public class MailBeeDnsNameErrorException : MailBeeDnsProtocolNegativeResponseException
	{
		// Token: 0x06002E4C RID: 11852 RVA: 0x000DE7F9 File Offset: 0x000DD7F9
		internal MailBeeDnsNameErrorException(int A_0, ai A_1, string A_2, short A_3, byte[] A_4, DnsReplyCode A_5) : base(A_0, A_1, A_2, A_3, A_4, A_5)
		{
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000DE80A File Offset: 0x000DD80A
		protected MailBeeDnsNameErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

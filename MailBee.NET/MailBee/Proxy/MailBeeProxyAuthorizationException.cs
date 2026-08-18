using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Proxy
{
	// Token: 0x02000509 RID: 1289
	[Serializable]
	public class MailBeeProxyAuthorizationException : MailBeeProxyException
	{
		// Token: 0x06002ACB RID: 10955 RVA: 0x000CBBDD File Offset: 0x000CABDD
		internal MailBeeProxyAuthorizationException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000CBBE7 File Offset: 0x000CABE7
		protected MailBeeProxyAuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

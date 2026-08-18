using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Proxy
{
	// Token: 0x0200050A RID: 1290
	[Serializable]
	public class MailBeeProxyAuthenticationException : MailBeeProxyException
	{
		// Token: 0x06002ACD RID: 10957 RVA: 0x000CBBF1 File Offset: 0x000CABF1
		internal MailBeeProxyAuthenticationException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000CBBFB File Offset: 0x000CABFB
		protected MailBeeProxyAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Proxy
{
	// Token: 0x02000508 RID: 1288
	public abstract class MailBeeProxyException : MailBeeConnectionException
	{
		// Token: 0x06002AC9 RID: 10953 RVA: 0x000CBBC9 File Offset: 0x000CABC9
		internal MailBeeProxyException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000CBBD3 File Offset: 0x000CABD3
		protected MailBeeProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

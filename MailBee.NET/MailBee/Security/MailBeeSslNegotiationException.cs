using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class MailBeeSslNegotiationException : MailBeeSslException, IMailBeeSocketMustCloseException
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x00029F5F File Offset: 0x00028F5F
		internal MailBeeSslNegotiationException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00029F69 File Offset: 0x00028F69
		protected MailBeeSslNegotiationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

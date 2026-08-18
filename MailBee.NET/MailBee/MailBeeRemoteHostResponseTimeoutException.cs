using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class MailBeeRemoteHostResponseTimeoutException : MailBeeConnectionException
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00008197 File Offset: 0x00007197
		internal MailBeeRemoteHostResponseTimeoutException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000081A1 File Offset: 0x000071A1
		protected MailBeeRemoteHostResponseTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

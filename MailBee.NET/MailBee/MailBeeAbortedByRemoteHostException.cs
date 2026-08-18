using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class MailBeeAbortedByRemoteHostException : MailBeeConnectionException
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00008183 File Offset: 0x00007183
		internal MailBeeAbortedByRemoteHostException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000818D File Offset: 0x0000718D
		protected MailBeeAbortedByRemoteHostException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

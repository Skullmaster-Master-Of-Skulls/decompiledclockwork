using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class MailBeeSocketTimeoutException : MailBeeSocketException
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000081D5 File Offset: 0x000071D5
		internal MailBeeSocketTimeoutException(Exception A_0, ai A_1) : base(52, A_0, A_1)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000081E1 File Offset: 0x000071E1
		protected MailBeeSocketTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

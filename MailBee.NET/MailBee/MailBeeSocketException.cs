using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class MailBeeSocketException : MailBeeConnectionException
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000081C0 File Offset: 0x000071C0
		internal MailBeeSocketException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000081CB File Offset: 0x000071CB
		protected MailBeeSocketException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

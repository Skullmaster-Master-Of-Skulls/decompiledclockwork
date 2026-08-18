using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class MailBeeSocketResetException : MailBeeSocketException
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x000081EB File Offset: 0x000071EB
		internal MailBeeSocketResetException(Exception A_0, ai A_1) : base(59, A_0, A_1)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000081F7 File Offset: 0x000071F7
		protected MailBeeSocketResetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

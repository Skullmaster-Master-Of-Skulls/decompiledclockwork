using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class MailBeeSocketHostUnreachableException : MailBeeSocketException
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00008259 File Offset: 0x00007259
		internal MailBeeSocketHostUnreachableException(Exception A_0, ai A_1) : base(58, A_0, A_1)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008265 File Offset: 0x00007265
		protected MailBeeSocketHostUnreachableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

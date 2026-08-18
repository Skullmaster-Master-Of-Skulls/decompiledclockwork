using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class MailBeeSocketAbortedException : MailBeeSocketException
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00008201 File Offset: 0x00007201
		internal MailBeeSocketAbortedException(Exception A_0, ai A_1) : base(53, A_0, A_1)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000820D File Offset: 0x0000720D
		protected MailBeeSocketAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

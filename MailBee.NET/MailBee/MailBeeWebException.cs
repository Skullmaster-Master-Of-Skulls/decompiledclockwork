using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class MailBeeWebException : MailBeeException
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000079ED File Offset: 0x000069ED
		internal MailBeeWebException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000079F6 File Offset: 0x000069F6
		internal MailBeeWebException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007A00 File Offset: 0x00006A00
		protected MailBeeWebException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

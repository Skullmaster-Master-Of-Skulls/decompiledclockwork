using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class MailBeeBatchException : MailBeeException
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000798D File Offset: 0x0000698D
		internal MailBeeBatchException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007996 File Offset: 0x00006996
		protected MailBeeBatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

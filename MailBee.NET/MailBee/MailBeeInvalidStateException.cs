using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class MailBeeInvalidStateException : MailBeeException
	{
		// Token: 0x06000118 RID: 280 RVA: 0x0000795D File Offset: 0x0000695D
		internal MailBeeInvalidStateException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007966 File Offset: 0x00006966
		protected MailBeeInvalidStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

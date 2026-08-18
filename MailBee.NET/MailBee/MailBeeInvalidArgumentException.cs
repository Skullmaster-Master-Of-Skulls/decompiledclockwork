using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class MailBeeInvalidArgumentException : MailBeeException
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00007970 File Offset: 0x00006970
		internal MailBeeInvalidArgumentException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000797A File Offset: 0x0000697A
		internal MailBeeInvalidArgumentException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007983 File Offset: 0x00006983
		protected MailBeeInvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

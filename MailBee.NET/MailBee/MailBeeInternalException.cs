using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class MailBeeInternalException : MailBeeLocalException, IMailBeeFatalException
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000792C File Offset: 0x0000692C
		internal MailBeeInternalException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007935 File Offset: 0x00006935
		internal MailBeeInternalException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000793F File Offset: 0x0000693F
		protected MailBeeInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

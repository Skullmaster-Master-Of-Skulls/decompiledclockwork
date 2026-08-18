using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Pop3Mail
{
	// Token: 0x02000581 RID: 1409
	public class MailBeePop3OptionalCommandNotSupportedException : MailBeePop3NegativeResponseException
	{
		// Token: 0x06002F49 RID: 12105 RVA: 0x000DFF01 File Offset: 0x000DEF01
		internal MailBeePop3OptionalCommandNotSupportedException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000DFF0C File Offset: 0x000DEF0C
		protected MailBeePop3OptionalCommandNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

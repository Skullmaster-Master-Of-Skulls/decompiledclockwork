using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class MailBeeStreamException : MailBeeException
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000079B3 File Offset: 0x000069B3
		internal MailBeeStreamException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000079BC File Offset: 0x000069BC
		internal MailBeeStreamException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000079C6 File Offset: 0x000069C6
		protected MailBeeStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

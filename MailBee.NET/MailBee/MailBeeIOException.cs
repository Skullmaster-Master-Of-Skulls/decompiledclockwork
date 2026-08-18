using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class MailBeeIOException : MailBeeLocalException
	{
		// Token: 0x06000124 RID: 292 RVA: 0x000079D0 File Offset: 0x000069D0
		internal MailBeeIOException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000079D9 File Offset: 0x000069D9
		internal MailBeeIOException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000079E3 File Offset: 0x000069E3
		protected MailBeeIOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

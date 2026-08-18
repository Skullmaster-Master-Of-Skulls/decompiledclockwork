using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class MailBeeDataSyntaxException : MailBeeLocalException
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00007A44 File Offset: 0x00006A44
		internal MailBeeDataSyntaxException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007A4D File Offset: 0x00006A4D
		internal MailBeeDataSyntaxException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007A57 File Offset: 0x00006A57
		protected MailBeeDataSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

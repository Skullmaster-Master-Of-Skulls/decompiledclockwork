using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class MailBeeExternalException : MailBeeLocalException, IMailBeeFatalException
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00007949 File Offset: 0x00006949
		internal MailBeeExternalException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007953 File Offset: 0x00006953
		protected MailBeeExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x0200018C RID: 396
	public abstract class MailBeeImapLoginNegativeResponseException : MailBeeImapNegativeResponseException, IMailBeeLoginNegativeResponseException
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x0003597C File Offset: 0x0003497C
		internal MailBeeImapLoginNegativeResponseException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00035987 File Offset: 0x00034987
		protected MailBeeImapLoginNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x0200018F RID: 399
	[Serializable]
	public class MailBeeImapResponseNotFoundException : MailBeeEmailProtocolException
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x000359EB File Offset: 0x000349EB
		internal MailBeeImapResponseNotFoundException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x000359F5 File Offset: 0x000349F5
		protected MailBeeImapResponseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

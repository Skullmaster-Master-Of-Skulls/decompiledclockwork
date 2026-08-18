using System;
using System.Runtime.Serialization;

namespace MailBee.Tnef
{
	// Token: 0x02000418 RID: 1048
	[Serializable]
	public class MailBeeTnefParsingException : MailBeeTnefException
	{
		// Token: 0x06002499 RID: 9369 RVA: 0x0009BA90 File Offset: 0x0009AA90
		internal MailBeeTnefParsingException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x0009BA9A File Offset: 0x0009AA9A
		internal MailBeeTnefParsingException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0009BAA3 File Offset: 0x0009AAA3
		internal MailBeeTnefParsingException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0009BAAD File Offset: 0x0009AAAD
		protected MailBeeTnefParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

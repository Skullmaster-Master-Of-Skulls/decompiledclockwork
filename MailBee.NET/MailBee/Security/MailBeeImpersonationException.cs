using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x0200010E RID: 270
	public abstract class MailBeeImpersonationException : MailBeeLocalException
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x00029EE9 File Offset: 0x00028EE9
		internal MailBeeImpersonationException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00029EF2 File Offset: 0x00028EF2
		internal MailBeeImpersonationException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00029EFC File Offset: 0x00028EFC
		protected MailBeeImpersonationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

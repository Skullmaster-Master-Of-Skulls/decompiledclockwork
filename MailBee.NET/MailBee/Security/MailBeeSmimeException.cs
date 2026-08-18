using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x0200010D RID: 269
	public abstract class MailBeeSmimeException : MailBeeLocalException
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00029ECC File Offset: 0x00028ECC
		internal MailBeeSmimeException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00029ED5 File Offset: 0x00028ED5
		internal MailBeeSmimeException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00029EDF File Offset: 0x00028EDF
		protected MailBeeSmimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

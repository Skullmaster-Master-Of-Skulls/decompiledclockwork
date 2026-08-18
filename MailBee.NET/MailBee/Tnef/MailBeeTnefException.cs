using System;
using System.Runtime.Serialization;

namespace MailBee.Tnef
{
	// Token: 0x02000416 RID: 1046
	public abstract class MailBeeTnefException : MailBeeLocalException
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x0009BA42 File Offset: 0x0009AA42
		internal MailBeeTnefException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0009BA4C File Offset: 0x0009AA4C
		internal MailBeeTnefException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0009BA55 File Offset: 0x0009AA55
		internal MailBeeTnefException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0009BA5F File Offset: 0x0009AA5F
		protected MailBeeTnefException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

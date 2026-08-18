using System;
using System.Runtime.Serialization;

namespace MailBee.Tnef
{
	// Token: 0x02000417 RID: 1047
	[Serializable]
	public class MailBeeTnefNotFoundException : MailBeeTnefException
	{
		// Token: 0x06002495 RID: 9365 RVA: 0x0009BA69 File Offset: 0x0009AA69
		internal MailBeeTnefNotFoundException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x0009BA73 File Offset: 0x0009AA73
		internal MailBeeTnefNotFoundException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x0009BA7C File Offset: 0x0009AA7C
		internal MailBeeTnefNotFoundException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x0009BA86 File Offset: 0x0009AA86
		protected MailBeeTnefNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

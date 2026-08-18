using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000574 RID: 1396
	[Serializable]
	public class MailBeeDnsLackOfRecursionException : MailBeeDnsProtocolException
	{
		// Token: 0x06002E44 RID: 11844 RVA: 0x000DE751 File Offset: 0x000DD751
		internal MailBeeDnsLackOfRecursionException(int A_0, ai A_1, string A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000DE75C File Offset: 0x000DD75C
		protected MailBeeDnsLackOfRecursionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x0200010A RID: 266
	public abstract class MailBeeCryptoProviderException : MailBeeLocalException
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x00029E6B File Offset: 0x00028E6B
		internal MailBeeCryptoProviderException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00029E74 File Offset: 0x00028E74
		internal MailBeeCryptoProviderException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00029E7E File Offset: 0x00028E7E
		protected MailBeeCryptoProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

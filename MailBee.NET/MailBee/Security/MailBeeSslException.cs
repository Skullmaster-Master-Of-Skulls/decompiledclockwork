using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x02000109 RID: 265
	public abstract class MailBeeSslException : MailBeeException
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x00029E44 File Offset: 0x00028E44
		internal MailBeeSslException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00029E4D File Offset: 0x00028E4D
		internal MailBeeSslException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00029E57 File Offset: 0x00028E57
		internal MailBeeSslException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00029E61 File Offset: 0x00028E61
		protected MailBeeSslException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

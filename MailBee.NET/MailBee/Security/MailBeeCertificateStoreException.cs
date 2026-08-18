using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x0200010B RID: 267
	public abstract class MailBeeCertificateStoreException : MailBeeLocalException
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x00029E88 File Offset: 0x00028E88
		internal MailBeeCertificateStoreException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00029E91 File Offset: 0x00028E91
		internal MailBeeCertificateStoreException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00029E9B File Offset: 0x00028E9B
		protected MailBeeCertificateStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

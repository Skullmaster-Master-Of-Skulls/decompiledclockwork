using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x0200010C RID: 268
	public class MailBeeCertificateException : MailBeeLocalException
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x00029EA5 File Offset: 0x00028EA5
		internal MailBeeCertificateException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00029EAE File Offset: 0x00028EAE
		internal MailBeeCertificateException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00029EB8 File Offset: 0x00028EB8
		internal MailBeeCertificateException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00029EC2 File Offset: 0x00028EC2
		protected MailBeeCertificateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

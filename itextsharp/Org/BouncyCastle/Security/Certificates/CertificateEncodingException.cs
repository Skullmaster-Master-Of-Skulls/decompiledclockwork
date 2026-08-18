using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x0200033E RID: 830
	public class CertificateEncodingException : CertificateException
	{
		// Token: 0x06001E0C RID: 7692 RVA: 0x000B4B61 File Offset: 0x000B3B61
		public CertificateEncodingException()
		{
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000B4B69 File Offset: 0x000B3B69
		public CertificateEncodingException(string msg) : base(msg)
		{
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000B4B72 File Offset: 0x000B3B72
		public CertificateEncodingException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}

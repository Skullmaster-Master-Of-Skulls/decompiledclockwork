using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020001D8 RID: 472
	public class CertificateException : GeneralSecurityException
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x0006AA53 File Offset: 0x00069A53
		public CertificateException()
		{
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0006AA5B File Offset: 0x00069A5B
		public CertificateException(string message) : base(message)
		{
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0006AA64 File Offset: 0x00069A64
		public CertificateException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}

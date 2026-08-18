using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x02000542 RID: 1346
	public class CertificateParsingException : CertificateException
	{
		// Token: 0x06002E44 RID: 11844 RVA: 0x0011E588 File Offset: 0x0011D588
		public CertificateParsingException()
		{
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x0011E590 File Offset: 0x0011D590
		public CertificateParsingException(string message) : base(message)
		{
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x0011E599 File Offset: 0x0011D599
		public CertificateParsingException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}

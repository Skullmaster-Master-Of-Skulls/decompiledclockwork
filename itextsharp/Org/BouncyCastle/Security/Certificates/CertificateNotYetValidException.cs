using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020001D9 RID: 473
	public class CertificateNotYetValidException : CertificateException
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x0006AA6E File Offset: 0x00069A6E
		public CertificateNotYetValidException()
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0006AA76 File Offset: 0x00069A76
		public CertificateNotYetValidException(string message) : base(message)
		{
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0006AA7F File Offset: 0x00069A7F
		public CertificateNotYetValidException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}

using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020004FE RID: 1278
	public class CertificateExpiredException : CertificateException
	{
		// Token: 0x06002BB4 RID: 11188 RVA: 0x00108A46 File Offset: 0x00107A46
		public CertificateExpiredException()
		{
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x00108A4E File Offset: 0x00107A4E
		public CertificateExpiredException(string message) : base(message)
		{
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x00108A57 File Offset: 0x00107A57
		public CertificateExpiredException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}

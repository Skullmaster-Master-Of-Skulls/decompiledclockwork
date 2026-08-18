using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000189 RID: 393
	public class AlwaysValidVerifyer : ICertificateVerifyer
	{
		// Token: 0x06000F4D RID: 3917 RVA: 0x000584A9 File Offset: 0x000574A9
		public bool IsValid(X509CertificateStructure[] certs)
		{
			return true;
		}
	}
}

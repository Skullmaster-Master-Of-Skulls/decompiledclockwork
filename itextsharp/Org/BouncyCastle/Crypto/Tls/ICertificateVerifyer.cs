using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000188 RID: 392
	public interface ICertificateVerifyer
	{
		// Token: 0x06000F4C RID: 3916
		bool IsValid(X509CertificateStructure[] certs);
	}
}

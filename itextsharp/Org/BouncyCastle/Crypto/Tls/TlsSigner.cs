using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200023A RID: 570
	internal interface TlsSigner
	{
		// Token: 0x06001633 RID: 5683
		byte[] CalculateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5andsha1);

		// Token: 0x06001634 RID: 5684
		ISigner CreateSigner();
	}
}

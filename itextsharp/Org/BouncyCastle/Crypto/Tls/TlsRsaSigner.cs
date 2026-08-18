using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200023B RID: 571
	internal class TlsRsaSigner : TlsSigner
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x00081F78 File Offset: 0x00080F78
		public byte[] CalculateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5andsha1)
		{
			ISigner signer = new GenericSigner(new Pkcs1Encoding(new RsaBlindedEngine()), new NullDigest());
			signer.Init(true, privateKey);
			signer.BlockUpdate(md5andsha1, 0, md5andsha1.Length);
			return signer.GenerateSignature();
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00081FB3 File Offset: 0x00080FB3
		public ISigner CreateSigner()
		{
			return new GenericSigner(new Pkcs1Encoding(new RsaBlindedEngine()), new CombinedHash());
		}
	}
}

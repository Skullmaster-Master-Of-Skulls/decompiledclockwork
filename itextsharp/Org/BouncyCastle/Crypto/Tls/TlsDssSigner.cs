using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000474 RID: 1140
	internal class TlsDssSigner : TlsSigner
	{
		// Token: 0x060026D4 RID: 9940 RVA: 0x000EAD24 File Offset: 0x000E9D24
		public byte[] CalculateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5andsha1)
		{
			ISigner signer = new DsaDigestSigner(new DsaSigner(), new NullDigest());
			signer.Init(true, privateKey);
			signer.BlockUpdate(md5andsha1, 16, 20);
			return signer.GenerateSignature();
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000EAD5A File Offset: 0x000E9D5A
		public ISigner CreateSigner()
		{
			return new DsaDigestSigner(new DsaSigner(), new Sha1Digest());
		}
	}
}

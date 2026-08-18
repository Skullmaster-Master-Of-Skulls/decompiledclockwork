using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000136 RID: 310
	public abstract class X509SignatureGenerator
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x000246F0 File Offset: 0x000228F0
		public PublicKey PublicKey
		{
			get
			{
				if (this._publicKey == null)
				{
					this._publicKey = this.BuildPublicKey();
				}
				return this._publicKey;
			}
		}

		// Token: 0x06000A11 RID: 2577
		public abstract byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm);

		// Token: 0x06000A12 RID: 2578
		public abstract byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm);

		// Token: 0x06000A13 RID: 2579
		protected abstract PublicKey BuildPublicKey();

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002470C File Offset: 0x0002290C
		public static X509SignatureGenerator CreateForECDsa(ECDsa key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return new ECDsaX509SignatureGenerator(key);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00024724 File Offset: 0x00022924
		public static X509SignatureGenerator CreateForRSA(RSA key, RSASignaturePadding signaturePadding)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (signaturePadding == null)
			{
				throw new ArgumentNullException("signaturePadding");
			}
			if (signaturePadding == RSASignaturePadding.Pkcs1)
			{
				return new RSAPkcs1X509SignatureGenerator(key);
			}
			if (signaturePadding.Mode == RSASignaturePaddingMode.Pss)
			{
				return new RSAPssX509SignatureGenerator(key, signaturePadding);
			}
			throw new ArgumentException(SR.GetString("Cryptography_InvalidPaddingMode"));
		}

		// Token: 0x0400075C RID: 1884
		private PublicKey _publicKey;
	}
}

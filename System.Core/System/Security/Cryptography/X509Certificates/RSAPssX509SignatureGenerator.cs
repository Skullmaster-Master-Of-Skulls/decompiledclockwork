using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200012D RID: 301
	internal sealed class RSAPssX509SignatureGenerator : X509SignatureGenerator
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00023D97 File Offset: 0x00021F97
		internal RSAPssX509SignatureGenerator(RSA key, RSASignaturePadding padding)
		{
			this._key = key;
			this._padding = padding;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00023DB0 File Offset: 0x00021FB0
		public override byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm)
		{
			if (this._padding != RSASignaturePadding.Pss)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidPaddingMode"));
			}
			uint value;
			string oidValue;
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				value = 32U;
				oidValue = "2.16.840.1.101.3.4.2.1";
			}
			else if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				value = 48U;
				oidValue = "2.16.840.1.101.3.4.2.2";
			}
			else
			{
				if (!(hashAlgorithm == HashAlgorithmName.SHA512))
				{
					throw new ArgumentOutOfRangeException("hashAlgorithm", hashAlgorithm, SR.GetString("Cryptography_UnknownHashAlgorithm", new object[]
					{
						hashAlgorithm.Name
					}));
				}
				value = 64U;
				oidValue = "2.16.840.1.101.3.4.2.3";
			}
			byte[][] array = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid(oidValue)
			});
			byte[][] array2 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				array
			});
			array2[0][0] = 160;
			byte[][] array3 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.SegmentedEncodeOid("1.2.840.113549.1.1.8"),
					array
				})
			});
			array3[0][0] = 161;
			byte[][] array4 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeUnsignedInteger(value)
			});
			array4[0][0] = 162;
			return DerEncoder.ConstructSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid("1.2.840.113549.1.1.10"),
				DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					array2,
					array3,
					array4
				})
			});
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00023F0D File Offset: 0x0002210D
		public override byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			return this._key.SignData(data, hashAlgorithm, this._padding);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00023F22 File Offset: 0x00022122
		protected override PublicKey BuildPublicKey()
		{
			return RSAPkcs1X509SignatureGenerator.BuildPublicKey(this._key);
		}

		// Token: 0x04000740 RID: 1856
		private readonly RSA _key;

		// Token: 0x04000741 RID: 1857
		private readonly RSASignaturePadding _padding;
	}
}

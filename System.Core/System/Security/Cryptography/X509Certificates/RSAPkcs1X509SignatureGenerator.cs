using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200012C RID: 300
	internal sealed class RSAPkcs1X509SignatureGenerator : X509SignatureGenerator
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00023C6C File Offset: 0x00021E6C
		internal RSAPkcs1X509SignatureGenerator(RSA key)
		{
			this._key = key;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00023C7B File Offset: 0x00021E7B
		public override byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			return this._key.SignData(data, hashAlgorithm, RSASignaturePadding.Pkcs1);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00023C8F File Offset: 0x00021E8F
		protected override PublicKey BuildPublicKey()
		{
			return RSAPkcs1X509SignatureGenerator.BuildPublicKey(this._key);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00023C9C File Offset: 0x00021E9C
		internal static PublicKey BuildPublicKey(RSA rsa)
		{
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			byte[] rawData = DerEncoder.ConstructSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeUnsignedInteger(rsaparameters.Modulus),
				DerEncoder.SegmentedEncodeUnsignedInteger(rsaparameters.Exponent)
			});
			Oid oid = new Oid("1.2.840.113549.1.1.1");
			Oid oid2 = oid;
			Oid oid3 = oid;
			byte[] array = new byte[2];
			array[0] = 5;
			return new PublicKey(oid2, new AsnEncodedData(oid3, array), new AsnEncodedData(oid, rawData));
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00023D04 File Offset: 0x00021F04
		public override byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm)
		{
			string oidValue;
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				oidValue = "1.2.840.113549.1.1.11";
			}
			else if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				oidValue = "1.2.840.113549.1.1.12";
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
				oidValue = "1.2.840.113549.1.1.13";
			}
			return DerEncoder.ConstructSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid(oidValue),
				DerEncoder.SegmentedEncodeNull()
			});
		}

		// Token: 0x0400073F RID: 1855
		private readonly RSA _key;
	}
}

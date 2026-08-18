using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000125 RID: 293
	internal sealed class ECDsaX509SignatureGenerator : X509SignatureGenerator
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x000230A0 File Offset: 0x000212A0
		internal ECDsaX509SignatureGenerator(ECDsa key)
		{
			this._key = key;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000230B0 File Offset: 0x000212B0
		public override byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm)
		{
			string oidValue;
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				oidValue = "1.2.840.10045.4.3.2";
			}
			else if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				oidValue = "1.2.840.10045.4.3.3";
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
				oidValue = "1.2.840.10045.4.3.4";
			}
			return DerEncoder.ConstructSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid(oidValue)
			});
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002313C File Offset: 0x0002133C
		public override byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			byte[] array = this._key.SignData(data, hashAlgorithm);
			int num = array.Length / 2;
			return DerEncoder.ConstructSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeUnsignedInteger(array, 0, num),
				DerEncoder.SegmentedEncodeUnsignedInteger(array, num, num)
			});
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00023180 File Offset: 0x00021380
		protected override PublicKey BuildPublicKey()
		{
			ECParameters ecparameters = this._key.ExportParameters(false);
			if (!ecparameters.Curve.IsNamed)
			{
				throw new InvalidOperationException(SR.GetString("Cryptography_ECC_NamedCurvesOnly"));
			}
			string text = ecparameters.Curve.Oid.Value;
			if (string.IsNullOrEmpty(text))
			{
				string friendlyName = ecparameters.Curve.Oid.FriendlyName;
				if (!(friendlyName == "nistP256"))
				{
					if (!(friendlyName == "nistP384"))
					{
						if (!(friendlyName == "nistP521"))
						{
							text = new Oid(friendlyName).Value;
						}
						else
						{
							text = "1.3.132.0.35";
						}
					}
					else
					{
						text = "1.3.132.0.34";
					}
				}
				else
				{
					text = "1.2.840.10045.3.1.7";
				}
			}
			byte[] array = new byte[1 + ecparameters.Q.X.Length + ecparameters.Q.Y.Length];
			array[0] = 4;
			Buffer.BlockCopy(ecparameters.Q.X, 0, array, 1, ecparameters.Q.X.Length);
			Buffer.BlockCopy(ecparameters.Q.Y, 0, array, 1 + ecparameters.Q.X.Length, ecparameters.Q.Y.Length);
			Oid oid = new Oid("1.2.840.10045.2.1");
			return new PublicKey(oid, new AsnEncodedData(oid, DerEncoder.EncodeOid(text)), new AsnEncodedData(oid, array));
		}

		// Token: 0x0400070F RID: 1807
		private readonly ECDsa _key;
	}
}

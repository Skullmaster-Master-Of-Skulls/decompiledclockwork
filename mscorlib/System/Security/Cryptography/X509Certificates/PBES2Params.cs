using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E1 RID: 2273
	internal struct PBES2Params
	{
		// Token: 0x060052A9 RID: 21161 RVA: 0x00129860 File Offset: 0x00128860
		internal static PBES2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return PBES2Params.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x00129870 File Offset: 0x00128870
		internal static PBES2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			PBES2Params result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				PBES2Params pbes2Params;
				PBES2Params.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbes2Params);
				asnValueReader.ThrowIfNotEmpty();
				result = pbes2Params;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x001298C8 File Offset: 0x001288C8
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			PBES2Params.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x001298D8 File Offset: 0x001288D8
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			try
			{
				PBES2Params.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x00129910 File Offset: 0x00128910
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			decoded = default(PBES2Params);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.KeyDerivationFunc);
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptionScheme);
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A98 RID: 10904
		internal AlgorithmIdentifierAsn KeyDerivationFunc;

		// Token: 0x04002A99 RID: 10905
		internal AlgorithmIdentifierAsn EncryptionScheme;
	}
}

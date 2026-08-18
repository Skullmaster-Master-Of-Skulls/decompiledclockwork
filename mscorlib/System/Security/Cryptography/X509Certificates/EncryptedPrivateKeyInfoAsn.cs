using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008DE RID: 2270
	internal struct EncryptedPrivateKeyInfoAsn
	{
		// Token: 0x06005299 RID: 21145 RVA: 0x00129424 File Offset: 0x00128424
		internal static EncryptedPrivateKeyInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedPrivateKeyInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x00129434 File Offset: 0x00128434
		internal static EncryptedPrivateKeyInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedPrivateKeyInfoAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn;
				EncryptedPrivateKeyInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedPrivateKeyInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = encryptedPrivateKeyInfoAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0012948C File Offset: 0x0012848C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			EncryptedPrivateKeyInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0012949C File Offset: 0x0012849C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			try
			{
				EncryptedPrivateKeyInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x001294D4 File Offset: 0x001284D4
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			decoded = default(EncryptedPrivateKeyInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptionAlgorithm);
			ReadOnlySpan<byte> destination;
			if (asnValueReader.TryReadPrimitiveOctetString(out destination))
			{
				int start;
				decoded.EncryptedData = (span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			}
			else
			{
				decoded.EncryptedData = asnValueReader.ReadOctetString();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A90 RID: 10896
		internal AlgorithmIdentifierAsn EncryptionAlgorithm;

		// Token: 0x04002A91 RID: 10897
		internal ReadOnlyMemory<byte> EncryptedData;
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008DB RID: 2267
	internal struct DigestInfoAsn
	{
		// Token: 0x0600528A RID: 21130 RVA: 0x00128FE5 File Offset: 0x00127FE5
		internal static DigestInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return DigestInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00128FF4 File Offset: 0x00127FF4
		internal static DigestInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			DigestInfoAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				DigestInfoAsn digestInfoAsn;
				DigestInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out digestInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = digestInfoAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0012904C File Offset: 0x0012804C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			DigestInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x0012905C File Offset: 0x0012805C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			try
			{
				DigestInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x00129094 File Offset: 0x00128094
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			decoded = default(DigestInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.DigestAlgorithm);
			ReadOnlySpan<byte> destination;
			if (asnValueReader.TryReadPrimitiveOctetString(out destination))
			{
				int start;
				decoded.Digest = (span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			}
			else
			{
				decoded.Digest = asnValueReader.ReadOctetString();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A88 RID: 10888
		internal AlgorithmIdentifierAsn DigestAlgorithm;

		// Token: 0x04002A89 RID: 10889
		internal ReadOnlyMemory<byte> Digest;
	}
}

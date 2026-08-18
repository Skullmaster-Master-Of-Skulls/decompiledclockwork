using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008DA RID: 2266
	internal struct ContentInfoAsn
	{
		// Token: 0x06005285 RID: 21125 RVA: 0x00128EA2 File Offset: 0x00127EA2
		internal static ContentInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return ContentInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00128EB0 File Offset: 0x00127EB0
		internal static ContentInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			ContentInfoAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				ContentInfoAsn contentInfoAsn;
				ContentInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out contentInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = contentInfoAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00128F08 File Offset: 0x00127F08
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			ContentInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00128F18 File Offset: 0x00127F18
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			try
			{
				ContentInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00128F50 File Offset: 0x00127F50
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			decoded = default(ContentInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.ContentType = asnValueReader.ReadObjectIdentifier();
			AsnValueReader asnValueReader2 = asnValueReader.ReadSequence(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0)));
			ReadOnlySpan<byte> destination = asnValueReader2.ReadEncodedValue();
			int start;
			decoded.Content = (span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			asnValueReader2.ThrowIfNotEmpty();
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A86 RID: 10886
		internal byte[] ContentType;

		// Token: 0x04002A87 RID: 10887
		internal ReadOnlyMemory<byte> Content;
	}
}

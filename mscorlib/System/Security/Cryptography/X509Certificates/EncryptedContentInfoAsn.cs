using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008DC RID: 2268
	internal struct EncryptedContentInfoAsn
	{
		// Token: 0x0600528F RID: 21135 RVA: 0x00129120 File Offset: 0x00128120
		internal static EncryptedContentInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedContentInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00129130 File Offset: 0x00128130
		internal static EncryptedContentInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedContentInfoAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedContentInfoAsn encryptedContentInfoAsn;
				EncryptedContentInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedContentInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = encryptedContentInfoAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00129188 File Offset: 0x00128188
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			EncryptedContentInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00129198 File Offset: 0x00128198
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			try
			{
				EncryptedContentInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x001291D0 File Offset: 0x001281D0
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			decoded = default(EncryptedContentInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.ContentType = asnValueReader.ReadObjectIdentifier();
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.ContentEncryptionAlgorithm);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
			{
				ReadOnlySpan<byte> destination;
				if (asnValueReader.TryReadPrimitiveOctetString(out destination, new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0))))
				{
					int start;
					decoded.EncryptedContent = new ReadOnlyMemory<byte>?(span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
				}
				else
				{
					decoded.EncryptedContent = new ReadOnlyMemory<byte>?(asnValueReader.ReadOctetString(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0))));
				}
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A8A RID: 10890
		internal byte[] ContentType;

		// Token: 0x04002A8B RID: 10891
		internal AlgorithmIdentifierAsn ContentEncryptionAlgorithm;

		// Token: 0x04002A8C RID: 10892
		internal ReadOnlyMemory<byte>? EncryptedContent;
	}
}

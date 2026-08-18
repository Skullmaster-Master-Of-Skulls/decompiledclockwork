using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D9 RID: 2265
	internal struct AttributeAsn
	{
		// Token: 0x06005280 RID: 21120 RVA: 0x00128D4F File Offset: 0x00127D4F
		internal static AttributeAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return AttributeAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00128D60 File Offset: 0x00127D60
		internal static AttributeAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			AttributeAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				AttributeAsn attributeAsn;
				AttributeAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out attributeAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = attributeAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00128DB8 File Offset: 0x00127DB8
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out AttributeAsn decoded)
		{
			AttributeAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00128DC8 File Offset: 0x00127DC8
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AttributeAsn decoded)
		{
			try
			{
				AttributeAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00128E00 File Offset: 0x00127E00
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AttributeAsn decoded)
		{
			decoded = default(AttributeAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.AttrType = asnValueReader.ReadObjectIdentifier();
			AsnValueReader asnValueReader2 = asnValueReader.ReadSetOf();
			List<ReadOnlyMemory<byte>> list = new List<ReadOnlyMemory<byte>>();
			while (asnValueReader2.HasData)
			{
				ReadOnlySpan<byte> destination = asnValueReader2.ReadEncodedValue();
				int start;
				ReadOnlyMemory<byte> item = span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray();
				list.Add(item);
			}
			decoded.AttrValues = list.ToArray();
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A84 RID: 10884
		internal byte[] AttrType;

		// Token: 0x04002A85 RID: 10885
		internal ReadOnlyMemory<byte>[] AttrValues;
	}
}

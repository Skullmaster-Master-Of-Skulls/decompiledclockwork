using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E6 RID: 2278
	internal struct SafeBagAsn
	{
		// Token: 0x060052C5 RID: 21189 RVA: 0x0012A544 File Offset: 0x00129544
		internal static SafeBagAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return SafeBagAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0012A554 File Offset: 0x00129554
		internal static SafeBagAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			SafeBagAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				SafeBagAsn safeBagAsn;
				SafeBagAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out safeBagAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = safeBagAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x0012A5AC File Offset: 0x001295AC
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out SafeBagAsn decoded)
		{
			SafeBagAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0012A5BC File Offset: 0x001295BC
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SafeBagAsn decoded)
		{
			try
			{
				SafeBagAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x0012A5F4 File Offset: 0x001295F4
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SafeBagAsn decoded)
		{
			decoded = default(SafeBagAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.BagId = asnValueReader.ReadObjectIdentifier();
			AsnValueReader asnValueReader2 = asnValueReader.ReadSequence(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0)));
			ReadOnlySpan<byte> destination = asnValueReader2.ReadEncodedValue();
			int start;
			decoded.BagValue = (span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			asnValueReader2.ThrowIfNotEmpty();
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.SetOf))
			{
				AsnValueReader asnValueReader3 = asnValueReader.ReadSetOf();
				List<AttributeAsn> list = new List<AttributeAsn>();
				while (asnValueReader3.HasData)
				{
					AttributeAsn item;
					AttributeAsn.Decode(ref asnValueReader3, rebind, out item);
					list.Add(item);
				}
				decoded.BagAttributes = list.ToArray();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002AA8 RID: 10920
		internal byte[] BagId;

		// Token: 0x04002AA9 RID: 10921
		internal ReadOnlyMemory<byte> BagValue;

		// Token: 0x04002AAA RID: 10922
		internal AttributeAsn[] BagAttributes;
	}
}

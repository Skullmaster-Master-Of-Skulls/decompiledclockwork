using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008DD RID: 2269
	internal struct EncryptedDataAsn
	{
		// Token: 0x06005294 RID: 21140 RVA: 0x001292BC File Offset: 0x001282BC
		internal static EncryptedDataAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedDataAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x001292CC File Offset: 0x001282CC
		internal static EncryptedDataAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedDataAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedDataAsn encryptedDataAsn;
				EncryptedDataAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedDataAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = encryptedDataAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x00129324 File Offset: 0x00128324
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedDataAsn decoded)
		{
			EncryptedDataAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x00129334 File Offset: 0x00128334
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedDataAsn decoded)
		{
			try
			{
				EncryptedDataAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0012936C File Offset: 0x0012836C
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedDataAsn decoded)
		{
			decoded = default(EncryptedDataAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			if (!asnValueReader.TryReadInt32(out decoded.Version))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			EncryptedContentInfoAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptedContentInfo);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
			{
				AsnValueReader asnValueReader2 = asnValueReader.ReadSetOf(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 1)));
				List<AttributeAsn> list = new List<AttributeAsn>();
				while (asnValueReader2.HasData)
				{
					AttributeAsn item;
					AttributeAsn.Decode(ref asnValueReader2, rebind, out item);
					list.Add(item);
				}
				decoded.UnprotectedAttributes = list.ToArray();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A8D RID: 10893
		internal int Version;

		// Token: 0x04002A8E RID: 10894
		internal EncryptedContentInfoAsn EncryptedContentInfo;

		// Token: 0x04002A8F RID: 10895
		internal AttributeAsn[] UnprotectedAttributes;
	}
}

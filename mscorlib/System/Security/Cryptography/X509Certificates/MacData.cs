using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008DF RID: 2271
	internal struct MacData
	{
		// Token: 0x0600529E RID: 21150 RVA: 0x00129560 File Offset: 0x00128560
		internal static MacData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return MacData.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00129570 File Offset: 0x00128570
		internal static MacData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			MacData result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				MacData macData;
				MacData.DecodeCore(ref asnValueReader, expectedTag, encoded, out macData);
				asnValueReader.ThrowIfNotEmpty();
				result = macData;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x001295C8 File Offset: 0x001285C8
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			MacData.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x001295D8 File Offset: 0x001285D8
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			try
			{
				MacData.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00129610 File Offset: 0x00128610
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			decoded = default(MacData);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			DigestInfoAsn.Decode(ref asnValueReader, rebind, out decoded.Mac);
			ReadOnlySpan<byte> destination;
			if (asnValueReader.TryReadPrimitiveOctetString(out destination))
			{
				int start;
				decoded.MacSalt = (span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			}
			else
			{
				decoded.MacSalt = asnValueReader.ReadOctetString();
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
			{
				if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
				{
					asnValueReader.ThrowIfNotEmpty();
				}
			}
			else
			{
				AsnValueReader asnValueReader2 = new AsnValueReader(MacData.s_DefaultIterationCount, AsnEncodingRules.DER);
				if (!asnValueReader2.TryReadInt32(out decoded.IterationCount))
				{
					asnValueReader2.ThrowIfNotEmpty();
				}
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A92 RID: 10898
		private static readonly byte[] s_DefaultIterationCount = new byte[]
		{
			2,
			1,
			1
		};

		// Token: 0x04002A93 RID: 10899
		internal DigestInfoAsn Mac;

		// Token: 0x04002A94 RID: 10900
		internal ReadOnlyMemory<byte> MacSalt;

		// Token: 0x04002A95 RID: 10901
		internal int IterationCount;
	}
}

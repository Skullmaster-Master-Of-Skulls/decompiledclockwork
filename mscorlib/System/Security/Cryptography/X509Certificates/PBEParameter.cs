using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E0 RID: 2272
	internal struct PBEParameter
	{
		// Token: 0x060052A4 RID: 21156 RVA: 0x0012971B File Offset: 0x0012871B
		internal static PBEParameter Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return PBEParameter.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x0012972C File Offset: 0x0012872C
		internal static PBEParameter Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			PBEParameter result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				PBEParameter pbeparameter;
				PBEParameter.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbeparameter);
				asnValueReader.ThrowIfNotEmpty();
				result = pbeparameter;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00129784 File Offset: 0x00128784
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			PBEParameter.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00129794 File Offset: 0x00128794
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			try
			{
				PBEParameter.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x001297CC File Offset: 0x001287CC
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			decoded = default(PBEParameter);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			ReadOnlySpan<byte> destination;
			if (asnValueReader.TryReadPrimitiveOctetString(out destination))
			{
				int start;
				decoded.Salt = (span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			}
			else
			{
				decoded.Salt = asnValueReader.ReadOctetString();
			}
			if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A96 RID: 10902
		internal ReadOnlyMemory<byte> Salt;

		// Token: 0x04002A97 RID: 10903
		internal int IterationCount;
	}
}

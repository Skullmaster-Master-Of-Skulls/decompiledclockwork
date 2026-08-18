using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D8 RID: 2264
	internal struct AlgorithmIdentifierAsn
	{
		// Token: 0x06005278 RID: 21112 RVA: 0x00128BA2 File Offset: 0x00127BA2
		internal static AlgorithmIdentifierAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return AlgorithmIdentifierAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00128BB0 File Offset: 0x00127BB0
		internal static AlgorithmIdentifierAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			AlgorithmIdentifierAsn result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				AlgorithmIdentifierAsn algorithmIdentifierAsn;
				AlgorithmIdentifierAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out algorithmIdentifierAsn);
				asnValueReader.ThrowIfNotEmpty();
				result = algorithmIdentifierAsn;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00128C08 File Offset: 0x00127C08
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			AlgorithmIdentifierAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00128C18 File Offset: 0x00127C18
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			try
			{
				AlgorithmIdentifierAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00128C50 File Offset: 0x00127C50
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			decoded = default(AlgorithmIdentifierAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.Algorithm = asnValueReader.ReadObjectIdentifier();
			if (asnValueReader.HasData)
			{
				ReadOnlySpan<byte> destination = asnValueReader.ReadEncodedValue();
				int start;
				decoded.Parameters = new ReadOnlyMemory<byte>?(span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00128CD2 File Offset: 0x00127CD2
		internal bool HasNullEquivalentParameters()
		{
			return AlgorithmIdentifierAsn.RepresentsNull(this.Parameters);
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00128CE0 File Offset: 0x00127CE0
		internal static bool RepresentsNull(ReadOnlyMemory<byte>? parameters)
		{
			if (parameters == null)
			{
				return true;
			}
			ReadOnlySpan<byte> span = parameters.Value.Span;
			return span.Length == 2 && span[0] == 5 && span[1] == 0;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00128D2C File Offset: 0x00127D2C
		// Note: this type is marked as 'beforefieldinit'.
		static AlgorithmIdentifierAsn()
		{
			byte[] array = new byte[2];
			array[0] = 5;
			AlgorithmIdentifierAsn.ExplicitDerNull = array;
		}

		// Token: 0x04002A81 RID: 10881
		internal byte[] Algorithm;

		// Token: 0x04002A82 RID: 10882
		internal ReadOnlyMemory<byte>? Parameters;

		// Token: 0x04002A83 RID: 10883
		internal static readonly ReadOnlyMemory<byte> ExplicitDerNull;
	}
}

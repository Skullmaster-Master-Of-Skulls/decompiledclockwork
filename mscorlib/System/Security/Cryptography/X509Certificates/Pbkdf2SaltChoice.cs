using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E3 RID: 2275
	internal struct Pbkdf2SaltChoice
	{
		// Token: 0x060052B4 RID: 21172 RVA: 0x00129B10 File Offset: 0x00128B10
		internal static Pbkdf2SaltChoice Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			Pbkdf2SaltChoice result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				Pbkdf2SaltChoice pbkdf2SaltChoice;
				Pbkdf2SaltChoice.DecodeCore(ref asnValueReader, encoded, out pbkdf2SaltChoice);
				asnValueReader.ThrowIfNotEmpty();
				result = pbkdf2SaltChoice;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x00129B64 File Offset: 0x00128B64
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2SaltChoice decoded)
		{
			try
			{
				Pbkdf2SaltChoice.DecodeCore(ref reader, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x00129B98 File Offset: 0x00128B98
		private static void DecodeCore(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2SaltChoice decoded)
		{
			decoded = default(Pbkdf2SaltChoice);
			Asn1Tag asn1Tag = reader.PeekTag();
			ReadOnlySpan<byte> span = rebind.Span;
			if (asn1Tag.HasSameClassAndValue(Asn1Tag.PrimitiveOctetString))
			{
				ReadOnlySpan<byte> destination;
				if (reader.TryReadPrimitiveOctetString(out destination))
				{
					int start;
					decoded.Specified = new ReadOnlyMemory<byte>?(span.Overlaps(destination, out start) ? rebind.Slice(start, destination.Length) : destination.ToArray());
					return;
				}
				decoded.Specified = new ReadOnlyMemory<byte>?(reader.ReadOctetString());
				return;
			}
			else
			{
				if (asn1Tag.HasSameClassAndValue(Asn1Tag.Sequence))
				{
					AlgorithmIdentifierAsn value;
					AlgorithmIdentifierAsn.Decode(ref reader, rebind, out value);
					decoded.OtherSource = new AlgorithmIdentifierAsn?(value);
					return;
				}
				throw new CryptographicException();
			}
		}

		// Token: 0x04002A9F RID: 10911
		internal ReadOnlyMemory<byte>? Specified;

		// Token: 0x04002AA0 RID: 10912
		internal AlgorithmIdentifierAsn? OtherSource;
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E2 RID: 2274
	internal struct Pbkdf2Params
	{
		// Token: 0x060052AE RID: 21166 RVA: 0x00129954 File Offset: 0x00128954
		internal static Pbkdf2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return Pbkdf2Params.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00129964 File Offset: 0x00128964
		internal static Pbkdf2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			Pbkdf2Params result;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				Pbkdf2Params pbkdf2Params;
				Pbkdf2Params.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbkdf2Params);
				asnValueReader.ThrowIfNotEmpty();
				result = pbkdf2Params;
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x001299BC File Offset: 0x001289BC
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			Pbkdf2Params.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x001299CC File Offset: 0x001289CC
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			try
			{
				Pbkdf2Params.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00129A04 File Offset: 0x00128A04
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			decoded = default(Pbkdf2Params);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			Pbkdf2SaltChoice.Decode(ref asnValueReader, rebind, out decoded.Salt);
			if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
			{
				int value;
				if (asnValueReader.TryReadInt32(out value))
				{
					decoded.KeyLength = new int?(value);
				}
				else
				{
					asnValueReader.ThrowIfNotEmpty();
				}
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
			{
				AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.Prf);
			}
			else
			{
				AsnValueReader asnValueReader2 = new AsnValueReader(Pbkdf2Params.s_DefaultPrf, AsnEncodingRules.DER);
				AlgorithmIdentifierAsn.Decode(ref asnValueReader2, rebind, out decoded.Prf);
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04002A9A RID: 10906
		private static readonly byte[] s_DefaultPrf = new byte[]
		{
			48,
			12,
			6,
			8,
			42,
			134,
			72,
			134,
			247,
			13,
			2,
			7,
			5,
			0
		};

		// Token: 0x04002A9B RID: 10907
		internal Pbkdf2SaltChoice Salt;

		// Token: 0x04002A9C RID: 10908
		internal int IterationCount;

		// Token: 0x04002A9D RID: 10909
		internal int? KeyLength;

		// Token: 0x04002A9E RID: 10910
		internal AlgorithmIdentifierAsn Prf;
	}
}

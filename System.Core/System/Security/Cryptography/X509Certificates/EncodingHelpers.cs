using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000126 RID: 294
	internal static class EncodingHelpers
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x000232CF File Offset: 0x000214CF
		internal static byte[][] WrapAsSegmentedForSequence(this byte[] derData)
		{
			return new byte[][]
			{
				EncodingHelpers.s_emptyArray,
				EncodingHelpers.s_emptyArray,
				derData
			};
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000232EC File Offset: 0x000214EC
		internal static void ValidateSignatureAlgorithm(byte[] signatureAlgorithm)
		{
			DerSequenceReader derSequenceReader = new DerSequenceReader(signatureAlgorithm);
			derSequenceReader.ReadOidAsString();
			if (derSequenceReader.HasData)
			{
				derSequenceReader.ValidateAndSkipDerValue();
			}
			if (derSequenceReader.HasData)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00023330 File Offset: 0x00021530
		internal static byte[][] SegmentedEncodeSubjectPublicKeyInfo(this PublicKey publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.Oid == null || string.IsNullOrEmpty(publicKey.Oid.Value) || publicKey.EncodedKeyValue == null)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidPublicKey_Object"));
			}
			byte[][] array;
			if (publicKey.EncodedParameters == null)
			{
				array = DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.SegmentedEncodeOid(publicKey.Oid)
				});
			}
			else
			{
				DerSequenceReader derSequenceReader = DerSequenceReader.CreateForPayload(publicKey.EncodedParameters.RawData);
				derSequenceReader.ValidateAndSkipDerValue();
				if (derSequenceReader.HasData)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				array = DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.SegmentedEncodeOid(publicKey.Oid),
					publicKey.EncodedParameters.RawData.WrapAsSegmentedForSequence()
				});
			}
			return DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				array,
				DerEncoder.SegmentedEncodeBitString(publicKey.EncodedKeyValue.RawData)
			});
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00023420 File Offset: 0x00021620
		internal static byte[][] SegmentedEncodedX509Extension(this X509Extension extension)
		{
			if (extension.Critical)
			{
				return DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.SegmentedEncodeOid(extension.Oid),
					DerEncoder.SegmentedEncodeBoolean(extension.Critical),
					DerEncoder.SegmentedEncodeOctetString(extension.RawData)
				});
			}
			return DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid(extension.Oid),
				DerEncoder.SegmentedEncodeOctetString(extension.RawData)
			});
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00023494 File Offset: 0x00021694
		internal static byte[][] SegmentedEncodeAttributeSet(this IEnumerable<X501Attribute> attributes)
		{
			List<byte[][]> list = new List<byte[][]>();
			foreach (X501Attribute x501Attribute in attributes)
			{
				list.Add(DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.SegmentedEncodeOid(x501Attribute.Oid),
					DerEncoder.ConstructSegmentedPresortedSet(new byte[][][]
					{
						x501Attribute.RawData.WrapAsSegmentedForSequence()
					})
				}));
			}
			return DerEncoder.ConstructSegmentedSet(list.ToArray());
		}

		// Token: 0x04000710 RID: 1808
		internal static readonly byte[] s_emptyArray = new byte[0];
	}
}

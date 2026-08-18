using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000129 RID: 297
	internal class Pkcs10CertificationRequestInfo
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002364E File Offset: 0x0002184E
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00023656 File Offset: 0x00021856
		internal X500DistinguishedName Subject { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002365F File Offset: 0x0002185F
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00023667 File Offset: 0x00021867
		internal PublicKey PublicKey { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00023670 File Offset: 0x00021870
		internal Collection<X501Attribute> Attributes { get; } = new Collection<X501Attribute>();

		// Token: 0x060009C7 RID: 2503 RVA: 0x00023678 File Offset: 0x00021878
		internal Pkcs10CertificationRequestInfo(X500DistinguishedName subject, PublicKey publicKey, IEnumerable<X501Attribute> attributes)
		{
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			this.Subject = subject;
			this.PublicKey = publicKey;
			if (attributes != null)
			{
				Collection<X501Attribute> attributes2 = this.Attributes;
				foreach (X501Attribute item in attributes)
				{
					attributes2.Add(item);
				}
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00023708 File Offset: 0x00021908
		private byte[] Encode()
		{
			byte[][] array = this.Attributes.SegmentedEncodeAttributeSet();
			array[0][0] = 160;
			return DerEncoder.ConstructSequence(new byte[][][]
			{
				Pkcs10CertificationRequestInfo.s_encodedVersion,
				this.Subject.RawData.WrapAsSegmentedForSequence(),
				this.PublicKey.SegmentedEncodeSubjectPublicKeyInfo(),
				array
			});
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00023764 File Offset: 0x00021964
		internal byte[] ToPkcs10Request(X509SignatureGenerator signatureGenerator, HashAlgorithmName hashAlgorithm)
		{
			byte[] array = this.Encode();
			byte[] data = signatureGenerator.SignData(array, hashAlgorithm);
			byte[] signatureAlgorithmIdentifier = signatureGenerator.GetSignatureAlgorithmIdentifier(hashAlgorithm);
			EncodingHelpers.ValidateSignatureAlgorithm(signatureAlgorithmIdentifier);
			return DerEncoder.ConstructSequence(new byte[][][]
			{
				array.WrapAsSegmentedForSequence(),
				signatureAlgorithmIdentifier.WrapAsSegmentedForSequence(),
				DerEncoder.SegmentedEncodeBitString(data)
			});
		}

		// Token: 0x0400073B RID: 1851
		private static readonly byte[][] s_encodedVersion = DerEncoder.SegmentedEncodeUnsignedInteger(new byte[1]);
	}
}

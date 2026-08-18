using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200012F RID: 303
	internal sealed class TbsCertificate
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002405B File Offset: 0x0002225B
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00024063 File Offset: 0x00022263
		public byte Version { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002406C File Offset: 0x0002226C
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00024074 File Offset: 0x00022274
		public byte[] SerialNumber { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002407D File Offset: 0x0002227D
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00024085 File Offset: 0x00022285
		public byte[] SignatureAlgorithm { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002408E File Offset: 0x0002228E
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00024096 File Offset: 0x00022296
		public X500DistinguishedName Issuer { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002409F File Offset: 0x0002229F
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x000240A7 File Offset: 0x000222A7
		public DateTimeOffset NotBefore { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000240B0 File Offset: 0x000222B0
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000240B8 File Offset: 0x000222B8
		public DateTimeOffset NotAfter { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000240C1 File Offset: 0x000222C1
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x000240C9 File Offset: 0x000222C9
		public X500DistinguishedName Subject { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000240D2 File Offset: 0x000222D2
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000240DA File Offset: 0x000222DA
		public PublicKey PublicKey { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000240E3 File Offset: 0x000222E3
		public Collection<X509Extension> Extensions { get; } = new Collection<X509Extension>();

		// Token: 0x060009F3 RID: 2547 RVA: 0x000240EC File Offset: 0x000222EC
		private byte[] Encode(X509SignatureGenerator signatureGenerator, HashAlgorithmName hashAlgorithm)
		{
			List<byte[][]> list = new List<byte[][]>();
			byte version = this.Version;
			if (version != 0)
			{
				byte[][] array = DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.SegmentedEncodeUnsignedInteger(new byte[]
					{
						version
					})
				});
				array[0][0] = 160;
				list.Add(array);
			}
			list.Add(DerEncoder.SegmentedEncodeUnsignedInteger(this.SerialNumber));
			byte[] array2 = this.SignatureAlgorithm ?? signatureGenerator.GetSignatureAlgorithmIdentifier(hashAlgorithm);
			EncodingHelpers.ValidateSignatureAlgorithm(array2);
			list.Add(array2.WrapAsSegmentedForSequence());
			list.Add(this.Issuer.RawData.WrapAsSegmentedForSequence());
			list.Add(DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				TbsCertificate.EncodeValidityField(this.NotBefore, "NotBefore"),
				TbsCertificate.EncodeValidityField(this.NotAfter, "NotAfter")
			}));
			list.Add(this.Subject.RawData.WrapAsSegmentedForSequence());
			list.Add(this.PublicKey.SegmentedEncodeSubjectPublicKeyInfo());
			if (this.Extensions.Count > 0)
			{
				List<byte[][]> list2 = new List<byte[][]>(this.Extensions.Count);
				HashSet<string> hashSet = new HashSet<string>(this.Extensions.Count);
				foreach (X509Extension x509Extension in this.Extensions)
				{
					if (x509Extension != null)
					{
						if (!hashSet.Add(x509Extension.Oid.Value))
						{
							throw new InvalidOperationException(SR.GetString("Cryptography_CertReq_DuplicateExtension", new object[]
							{
								x509Extension.Oid.Value
							}));
						}
						list2.Add(x509Extension.SegmentedEncodedX509Extension());
					}
				}
				byte[][] array3 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
				{
					DerEncoder.ConstructSegmentedSequence(list2)
				});
				array3[0][0] = 163;
				list.Add(array3);
			}
			return DerEncoder.ConstructSequence(list);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000242D4 File Offset: 0x000224D4
		private static byte[][] EncodeValidityField(DateTimeOffset validityField, string propertyName)
		{
			DateTime utcDateTime = validityField.UtcDateTime;
			if (utcDateTime.Year < 1950)
			{
				throw new ArgumentOutOfRangeException(propertyName, utcDateTime, SR.GetString("Cryptography_CertReq_DateTooOld"));
			}
			if (utcDateTime.Year < 2050)
			{
				return DerEncoder.SegmentedEncodeUtcTime(utcDateTime);
			}
			return DerEncoder.SegmentedEncodeGeneralizedTime(utcDateTime);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002432C File Offset: 0x0002252C
		internal byte[] Sign(X509SignatureGenerator signatureGenerator, HashAlgorithmName hashAlgorithm)
		{
			if (signatureGenerator == null)
			{
				throw new ArgumentNullException("signatureGenerator");
			}
			byte[] array = this.Encode(signatureGenerator, hashAlgorithm);
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
	}
}

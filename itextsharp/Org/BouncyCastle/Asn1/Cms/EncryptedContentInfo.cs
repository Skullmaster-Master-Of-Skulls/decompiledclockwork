using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000047 RID: 71
	public class EncryptedContentInfo : Asn1Encodable
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000A30E File Offset: 0x0000930E
		public EncryptedContentInfo(DerObjectIdentifier contentType, AlgorithmIdentifier contentEncryptionAlgorithm, Asn1OctetString encryptedContent)
		{
			this.contentType = contentType;
			this.contentEncryptionAlgorithm = contentEncryptionAlgorithm;
			this.encryptedContent = encryptedContent;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A32C File Offset: 0x0000932C
		public EncryptedContentInfo(Asn1Sequence seq)
		{
			this.contentType = (DerObjectIdentifier)seq[0];
			this.contentEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.encryptedContent = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[2], false);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A384 File Offset: 0x00009384
		public static EncryptedContentInfo GetInstance(object obj)
		{
			if (obj == null || obj is EncryptedContentInfo)
			{
				return (EncryptedContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EncryptedContentInfo: " + obj.GetType().Name);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A3D1 File Offset: 0x000093D1
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A3D9 File Offset: 0x000093D9
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this.contentEncryptionAlgorithm;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A3E1 File Offset: 0x000093E1
		public Asn1OctetString EncryptedContent
		{
			get
			{
				return this.encryptedContent;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A3EC File Offset: 0x000093EC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.contentType,
				this.contentEncryptionAlgorithm
			});
			if (this.encryptedContent != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new BerTaggedObject(false, 0, this.encryptedContent)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040000D6 RID: 214
		private DerObjectIdentifier contentType;

		// Token: 0x040000D7 RID: 215
		private AlgorithmIdentifier contentEncryptionAlgorithm;

		// Token: 0x040000D8 RID: 216
		private Asn1OctetString encryptedContent;
	}
}

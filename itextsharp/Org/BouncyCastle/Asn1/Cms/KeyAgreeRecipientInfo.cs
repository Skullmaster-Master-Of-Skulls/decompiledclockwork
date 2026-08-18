using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020005C2 RID: 1474
	public class KeyAgreeRecipientInfo : Asn1Encodable
	{
		// Token: 0x0600329E RID: 12958 RVA: 0x00139880 File Offset: 0x00138880
		public KeyAgreeRecipientInfo(OriginatorIdentifierOrKey originator, Asn1OctetString ukm, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1Sequence recipientEncryptedKeys)
		{
			this.version = new DerInteger(3);
			this.originator = originator;
			this.ukm = ukm;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.recipientEncryptedKeys = recipientEncryptedKeys;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x001398B4 File Offset: 0x001388B4
		public KeyAgreeRecipientInfo(Asn1Sequence seq)
		{
			int index = 0;
			this.version = (DerInteger)seq[index++];
			this.originator = OriginatorIdentifierOrKey.GetInstance((Asn1TaggedObject)seq[index++], true);
			if (seq[index] is Asn1TaggedObject)
			{
				this.ukm = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[index++], true);
			}
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[index++]);
			this.recipientEncryptedKeys = (Asn1Sequence)seq[index++];
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x00139951 File Offset: 0x00138951
		public static KeyAgreeRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KeyAgreeRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x00139960 File Offset: 0x00138960
		public static KeyAgreeRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KeyAgreeRecipientInfo)
			{
				return (KeyAgreeRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyAgreeRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Illegal object in KeyAgreeRecipientInfo: " + obj.GetType().Name);
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x001399AD File Offset: 0x001389AD
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x001399B5 File Offset: 0x001389B5
		public OriginatorIdentifierOrKey Originator
		{
			get
			{
				return this.originator;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x001399BD File Offset: 0x001389BD
		public Asn1OctetString UserKeyingMaterial
		{
			get
			{
				return this.ukm;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x001399C5 File Offset: 0x001389C5
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x001399CD File Offset: 0x001389CD
		public Asn1Sequence RecipientEncryptedKeys
		{
			get
			{
				return this.recipientEncryptedKeys;
			}
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x001399D8 File Offset: 0x001389D8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				new DerTaggedObject(true, 0, this.originator)
			});
			if (this.ukm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.ukm)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.keyEncryptionAlgorithm,
				this.recipientEncryptedKeys
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002292 RID: 8850
		private DerInteger version;

		// Token: 0x04002293 RID: 8851
		private OriginatorIdentifierOrKey originator;

		// Token: 0x04002294 RID: 8852
		private Asn1OctetString ukm;

		// Token: 0x04002295 RID: 8853
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04002296 RID: 8854
		private Asn1Sequence recipientEncryptedKeys;
	}
}

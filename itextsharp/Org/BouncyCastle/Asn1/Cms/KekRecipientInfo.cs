using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000318 RID: 792
	public class KekRecipientInfo : Asn1Encodable
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x000ABCAC File Offset: 0x000AACAC
		public KekRecipientInfo(KekIdentifier kekID, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(4);
			this.kekID = kekID;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000ABCD8 File Offset: 0x000AACD8
		public KekRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.kekID = KekIdentifier.GetInstance(seq[1]);
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
			this.encryptedKey = (Asn1OctetString)seq[3];
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000ABD33 File Offset: 0x000AAD33
		public static KekRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KekRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000ABD44 File Offset: 0x000AAD44
		public static KekRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KekRecipientInfo)
			{
				return (KekRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KekRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid KekRecipientInfo: " + obj.GetType().Name);
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x000ABD91 File Offset: 0x000AAD91
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x000ABD99 File Offset: 0x000AAD99
		public KekIdentifier KekID
		{
			get
			{
				return this.kekID;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x000ABDA1 File Offset: 0x000AADA1
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x000ABDA9 File Offset: 0x000AADA9
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000ABDB4 File Offset: 0x000AADB4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.kekID,
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
		}

		// Token: 0x040013DA RID: 5082
		private DerInteger version;

		// Token: 0x040013DB RID: 5083
		private KekIdentifier kekID;

		// Token: 0x040013DC RID: 5084
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x040013DD RID: 5085
		private Asn1OctetString encryptedKey;
	}
}

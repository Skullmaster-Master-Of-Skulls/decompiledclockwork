using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000046 RID: 70
	public class KeyTransRecipientInfo : Asn1Encodable
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000A1B4 File Offset: 0x000091B4
		public KeyTransRecipientInfo(RecipientIdentifier rid, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			if (rid.ToAsn1Object() is Asn1TaggedObject)
			{
				this.version = new DerInteger(2);
			}
			else
			{
				this.version = new DerInteger(0);
			}
			this.rid = rid;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A204 File Offset: 0x00009204
		public KeyTransRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.rid = RecipientIdentifier.GetInstance(seq[1]);
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
			this.encryptedKey = (Asn1OctetString)seq[3];
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A260 File Offset: 0x00009260
		public static KeyTransRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is KeyTransRecipientInfo)
			{
				return (KeyTransRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyTransRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Illegal object in KeyTransRecipientInfo: " + obj.GetType().Name);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A2AD File Offset: 0x000092AD
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A2B5 File Offset: 0x000092B5
		public RecipientIdentifier RecipientIdentifier
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A2BD File Offset: 0x000092BD
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A2C5 File Offset: 0x000092C5
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A2D0 File Offset: 0x000092D0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.rid,
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
		}

		// Token: 0x040000D2 RID: 210
		private DerInteger version;

		// Token: 0x040000D3 RID: 211
		private RecipientIdentifier rid;

		// Token: 0x040000D4 RID: 212
		private AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x040000D5 RID: 213
		private Asn1OctetString encryptedKey;
	}
}

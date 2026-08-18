using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000521 RID: 1313
	public class PasswordRecipientInfo : Asn1Encodable
	{
		// Token: 0x06002CC3 RID: 11459 RVA: 0x0010FE97 File Offset: 0x0010EE97
		public PasswordRecipientInfo(AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(0);
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0010FEB9 File Offset: 0x0010EEB9
		public PasswordRecipientInfo(AlgorithmIdentifier keyDerivationAlgorithm, AlgorithmIdentifier keyEncryptionAlgorithm, Asn1OctetString encryptedKey)
		{
			this.version = new DerInteger(0);
			this.keyDerivationAlgorithm = keyDerivationAlgorithm;
			this.keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0010FEE4 File Offset: 0x0010EEE4
		public PasswordRecipientInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			if (seq[1] is Asn1TaggedObject)
			{
				this.keyDerivationAlgorithm = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)seq[1], false);
				this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[2]);
				this.encryptedKey = (Asn1OctetString)seq[3];
				return;
			}
			this.keyEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.encryptedKey = (Asn1OctetString)seq[2];
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0010FF78 File Offset: 0x0010EF78
		public static PasswordRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return PasswordRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0010FF88 File Offset: 0x0010EF88
		public static PasswordRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is PasswordRecipientInfo)
			{
				return (PasswordRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PasswordRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid PasswordRecipientInfo: " + obj.GetType().Name);
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x0010FFD5 File Offset: 0x0010EFD5
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x0010FFDD File Offset: 0x0010EFDD
		public AlgorithmIdentifier KeyDerivationAlgorithm
		{
			get
			{
				return this.keyDerivationAlgorithm;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x0010FFE5 File Offset: 0x0010EFE5
		public AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this.keyEncryptionAlgorithm;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x0010FFED File Offset: 0x0010EFED
		public Asn1OctetString EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x0010FFF8 File Offset: 0x0010EFF8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			if (this.keyDerivationAlgorithm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.keyDerivationAlgorithm)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.keyEncryptionAlgorithm,
				this.encryptedKey
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001ECF RID: 7887
		private readonly DerInteger version;

		// Token: 0x04001ED0 RID: 7888
		private readonly AlgorithmIdentifier keyDerivationAlgorithm;

		// Token: 0x04001ED1 RID: 7889
		private readonly AlgorithmIdentifier keyEncryptionAlgorithm;

		// Token: 0x04001ED2 RID: 7890
		private readonly Asn1OctetString encryptedKey;
	}
}

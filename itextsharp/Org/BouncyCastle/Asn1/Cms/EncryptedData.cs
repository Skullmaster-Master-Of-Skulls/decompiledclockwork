using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000452 RID: 1106
	public class EncryptedData : Asn1Encodable
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x000E21F8 File Offset: 0x000E11F8
		public static EncryptedData GetInstance(object obj)
		{
			if (obj is EncryptedData)
			{
				return (EncryptedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EncryptedData: " + obj.GetType().Name);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000E2237 File Offset: 0x000E1237
		public EncryptedData(EncryptedContentInfo encInfo) : this(encInfo, null)
		{
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000E2241 File Offset: 0x000E1241
		public EncryptedData(EncryptedContentInfo encInfo, Asn1Set unprotectedAttrs)
		{
			if (encInfo == null)
			{
				throw new ArgumentNullException("encInfo");
			}
			this.version = new DerInteger((unprotectedAttrs == null) ? 0 : 2);
			this.encryptedContentInfo = encInfo;
			this.unprotectedAttrs = unprotectedAttrs;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000E2278 File Offset: 0x000E1278
		private EncryptedData(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.version = DerInteger.GetInstance(seq[0]);
			this.encryptedContentInfo = EncryptedContentInfo.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.unprotectedAttrs = Asn1Set.GetInstance(seq[2]);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000E230A File Offset: 0x000E130A
		public virtual DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000E2312 File Offset: 0x000E1312
		public virtual EncryptedContentInfo EncryptedContentInfo
		{
			get
			{
				return this.encryptedContentInfo;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000E231A File Offset: 0x000E131A
		public virtual Asn1Set UnprotectedAttrs
		{
			get
			{
				return this.unprotectedAttrs;
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000E2324 File Offset: 0x000E1324
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.encryptedContentInfo
			});
			if (this.unprotectedAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new BerTaggedObject(false, 1, this.unprotectedAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04001A24 RID: 6692
		private readonly DerInteger version;

		// Token: 0x04001A25 RID: 6693
		private readonly EncryptedContentInfo encryptedContentInfo;

		// Token: 0x04001A26 RID: 6694
		private readonly Asn1Set unprotectedAttrs;
	}
}

using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000411 RID: 1041
	public class EnvelopedData : Asn1Encodable
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x000D96E4 File Offset: 0x000D86E4
		public EnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo encryptedContentInfo, Asn1Set unprotectedAttrs)
		{
			if (originatorInfo != null || unprotectedAttrs != null)
			{
				this.version = new DerInteger(2);
			}
			else
			{
				this.version = new DerInteger(0);
				foreach (object o in recipientInfos)
				{
					RecipientInfo instance = RecipientInfo.GetInstance(o);
					if (!instance.Version.Equals(this.version))
					{
						this.version = new DerInteger(2);
						break;
					}
				}
			}
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.encryptedContentInfo = encryptedContentInfo;
			this.unprotectedAttrs = unprotectedAttrs;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000D9798 File Offset: 0x000D8798
		public EnvelopedData(Asn1Sequence seq)
		{
			int num = 0;
			this.version = (DerInteger)seq[num++];
			object obj = seq[num++];
			if (obj is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)obj, false);
				obj = seq[num++];
			}
			this.recipientInfos = Asn1Set.GetInstance(obj);
			this.encryptedContentInfo = EncryptedContentInfo.GetInstance(seq[num++]);
			if (seq.Count > num)
			{
				this.unprotectedAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[num], false);
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000D9838 File Offset: 0x000D8838
		public static EnvelopedData GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return EnvelopedData.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000D9848 File Offset: 0x000D8848
		public static EnvelopedData GetInstance(object obj)
		{
			if (obj == null || obj is EnvelopedData)
			{
				return (EnvelopedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EnvelopedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EnvelopedData: " + obj.GetType().Name);
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000D9895 File Offset: 0x000D8895
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000D989D File Offset: 0x000D889D
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000D98A5 File Offset: 0x000D88A5
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000D98AD File Offset: 0x000D88AD
		public EncryptedContentInfo EncryptedContentInfo
		{
			get
			{
				return this.encryptedContentInfo;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000D98B5 File Offset: 0x000D88B5
		public Asn1Set UnprotectedAttrs
		{
			get
			{
				return this.unprotectedAttrs;
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000D98C0 File Offset: 0x000D88C0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			if (this.originatorInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.originatorInfo)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.recipientInfos,
				this.encryptedContentInfo
			});
			if (this.unprotectedAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.unprotectedAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x0400187C RID: 6268
		private DerInteger version;

		// Token: 0x0400187D RID: 6269
		private OriginatorInfo originatorInfo;

		// Token: 0x0400187E RID: 6270
		private Asn1Set recipientInfos;

		// Token: 0x0400187F RID: 6271
		private EncryptedContentInfo encryptedContentInfo;

		// Token: 0x04001880 RID: 6272
		private Asn1Set unprotectedAttrs;
	}
}

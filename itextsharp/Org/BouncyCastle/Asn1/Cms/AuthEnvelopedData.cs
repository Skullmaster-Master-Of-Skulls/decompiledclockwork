using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000AF RID: 175
	public class AuthEnvelopedData : Asn1Encodable
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0001C668 File Offset: 0x0001B668
		public AuthEnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo authEncryptedContentInfo, Asn1Set authAttrs, Asn1OctetString mac, Asn1Set unauthAttrs)
		{
			this.version = new DerInteger(0);
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.authEncryptedContentInfo = authEncryptedContentInfo;
			this.authAttrs = authAttrs;
			this.mac = mac;
			this.unauthAttrs = unauthAttrs;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001C6B4 File Offset: 0x0001B6B4
		private AuthEnvelopedData(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Object asn1Object = seq[num++].ToAsn1Object();
			this.version = (DerInteger)asn1Object;
			asn1Object = seq[num++].ToAsn1Object();
			if (asn1Object is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)asn1Object, false);
				asn1Object = seq[num++].ToAsn1Object();
			}
			this.recipientInfos = Asn1Set.GetInstance(asn1Object);
			asn1Object = seq[num++].ToAsn1Object();
			this.authEncryptedContentInfo = EncryptedContentInfo.GetInstance(asn1Object);
			asn1Object = seq[num++].ToAsn1Object();
			if (asn1Object is Asn1TaggedObject)
			{
				this.authAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Object, false);
				asn1Object = seq[num++].ToAsn1Object();
			}
			this.mac = Asn1OctetString.GetInstance(asn1Object);
			if (seq.Count > num)
			{
				asn1Object = seq[num++].ToAsn1Object();
				this.unauthAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Object, false);
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001C7BF File Offset: 0x0001B7BF
		public static AuthEnvelopedData GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AuthEnvelopedData.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001C7D0 File Offset: 0x0001B7D0
		public static AuthEnvelopedData GetInstance(object obj)
		{
			if (obj == null || obj is AuthEnvelopedData)
			{
				return (AuthEnvelopedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthEnvelopedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid AuthEnvelopedData: " + obj.GetType().Name);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001C81D File Offset: 0x0001B81D
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001C825 File Offset: 0x0001B825
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001C82D File Offset: 0x0001B82D
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001C835 File Offset: 0x0001B835
		public EncryptedContentInfo AuthEncryptedContentInfo
		{
			get
			{
				return this.authEncryptedContentInfo;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001C83D File Offset: 0x0001B83D
		public Asn1Set AuthAttrs
		{
			get
			{
				return this.authAttrs;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001C845 File Offset: 0x0001B845
		public Asn1OctetString Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001C84D File Offset: 0x0001B84D
		public Asn1Set UnauthAttrs
		{
			get
			{
				return this.unauthAttrs;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001C858 File Offset: 0x0001B858
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
				this.recipientInfos
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.authEncryptedContentInfo
			});
			if (this.authAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.authAttrs)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.mac
			});
			if (this.unauthAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.unauthAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040002AE RID: 686
		private DerInteger version;

		// Token: 0x040002AF RID: 687
		private OriginatorInfo originatorInfo;

		// Token: 0x040002B0 RID: 688
		private Asn1Set recipientInfos;

		// Token: 0x040002B1 RID: 689
		private EncryptedContentInfo authEncryptedContentInfo;

		// Token: 0x040002B2 RID: 690
		private Asn1Set authAttrs;

		// Token: 0x040002B3 RID: 691
		private Asn1OctetString mac;

		// Token: 0x040002B4 RID: 692
		private Asn1Set unauthAttrs;
	}
}

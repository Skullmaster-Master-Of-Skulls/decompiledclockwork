using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000391 RID: 913
	public class AuthenticatedData : Asn1Encodable
	{
		// Token: 0x06001FBC RID: 8124 RVA: 0x000BCD0C File Offset: 0x000BBD0C
		public AuthenticatedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, AlgorithmIdentifier macAlgorithm, AlgorithmIdentifier digestAlgorithm, ContentInfo encapsulatedContent, Asn1Set authAttrs, Asn1OctetString mac, Asn1Set unauthAttrs)
		{
			if ((digestAlgorithm != null || authAttrs != null) && (digestAlgorithm == null || authAttrs == null))
			{
				throw new ArgumentException("digestAlgorithm and authAttrs must be set together");
			}
			this.version = new DerInteger(AuthenticatedData.CalculateVersion(originatorInfo));
			this.originatorInfo = originatorInfo;
			this.macAlgorithm = macAlgorithm;
			this.digestAlgorithm = digestAlgorithm;
			this.recipientInfos = recipientInfos;
			this.encapsulatedContentInfo = encapsulatedContent;
			this.authAttrs = authAttrs;
			this.mac = mac;
			this.unauthAttrs = unauthAttrs;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000BCD88 File Offset: 0x000BBD88
		private AuthenticatedData(Asn1Sequence seq)
		{
			int num = 0;
			this.version = (DerInteger)seq[num++];
			Asn1Encodable asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.recipientInfos = Asn1Set.GetInstance(asn1Encodable);
			this.macAlgorithm = AlgorithmIdentifier.GetInstance(seq[num++]);
			asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.digestAlgorithm = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.encapsulatedContentInfo = ContentInfo.GetInstance(asn1Encodable);
			asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.authAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.mac = Asn1OctetString.GetInstance(asn1Encodable);
			if (seq.Count > num)
			{
				this.unauthAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[num], false);
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000BCEA4 File Offset: 0x000BBEA4
		public static AuthenticatedData GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AuthenticatedData.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000BCEB4 File Offset: 0x000BBEB4
		public static AuthenticatedData GetInstance(object obj)
		{
			if (obj == null || obj is AuthenticatedData)
			{
				return (AuthenticatedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthenticatedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid AuthenticatedData: " + obj.GetType().Name);
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x000BCF01 File Offset: 0x000BBF01
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x000BCF09 File Offset: 0x000BBF09
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x000BCF11 File Offset: 0x000BBF11
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x000BCF19 File Offset: 0x000BBF19
		public AlgorithmIdentifier MacAlgorithm
		{
			get
			{
				return this.macAlgorithm;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000BCF21 File Offset: 0x000BBF21
		public ContentInfo EncapsulatedContentInfo
		{
			get
			{
				return this.encapsulatedContentInfo;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x000BCF29 File Offset: 0x000BBF29
		public Asn1Set AuthAttrs
		{
			get
			{
				return this.authAttrs;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000BCF31 File Offset: 0x000BBF31
		public Asn1OctetString Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x000BCF39 File Offset: 0x000BBF39
		public Asn1Set UnauthAttrs
		{
			get
			{
				return this.unauthAttrs;
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000BCF44 File Offset: 0x000BBF44
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
				this.macAlgorithm
			});
			if (this.digestAlgorithm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.digestAlgorithm)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.encapsulatedContentInfo
			});
			if (this.authAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.authAttrs)
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
					new DerTaggedObject(false, 3, this.unauthAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000BD074 File Offset: 0x000BC074
		public static int CalculateVersion(OriginatorInfo origInfo)
		{
			if (origInfo == null)
			{
				return 0;
			}
			int result = 0;
			foreach (object obj in origInfo.Certificates)
			{
				if (obj is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
					if (asn1TaggedObject.TagNo == 2)
					{
						result = 1;
					}
					else if (asn1TaggedObject.TagNo == 3)
					{
						result = 3;
						break;
					}
				}
			}
			foreach (object obj2 in origInfo.Crls)
			{
				if (obj2 is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject2 = (Asn1TaggedObject)obj2;
					if (asn1TaggedObject2.TagNo == 1)
					{
						result = 3;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x040015E6 RID: 5606
		private DerInteger version;

		// Token: 0x040015E7 RID: 5607
		private OriginatorInfo originatorInfo;

		// Token: 0x040015E8 RID: 5608
		private Asn1Set recipientInfos;

		// Token: 0x040015E9 RID: 5609
		private AlgorithmIdentifier macAlgorithm;

		// Token: 0x040015EA RID: 5610
		private AlgorithmIdentifier digestAlgorithm;

		// Token: 0x040015EB RID: 5611
		private ContentInfo encapsulatedContentInfo;

		// Token: 0x040015EC RID: 5612
		private Asn1Set authAttrs;

		// Token: 0x040015ED RID: 5613
		private Asn1OctetString mac;

		// Token: 0x040015EE RID: 5614
		private Asn1Set unauthAttrs;
	}
}

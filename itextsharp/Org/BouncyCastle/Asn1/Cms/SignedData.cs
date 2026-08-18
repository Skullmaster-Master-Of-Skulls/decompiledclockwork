using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000156 RID: 342
	public class SignedData : Asn1Encodable
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00043520 File Offset: 0x00042520
		public static SignedData GetInstance(object obj)
		{
			if (obj is SignedData)
			{
				return (SignedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00043570 File Offset: 0x00042570
		public SignedData(Asn1Set digestAlgorithms, ContentInfo contentInfo, Asn1Set certificates, Asn1Set crls, Asn1Set signerInfos)
		{
			this.version = this.CalculateVersion(contentInfo.ContentType, certificates, crls, signerInfos);
			this.digestAlgorithms = digestAlgorithms;
			this.contentInfo = contentInfo;
			this.certificates = certificates;
			this.crls = crls;
			this.signerInfos = signerInfos;
			this.crlsBer = (crls is BerSet);
			this.certsBer = (certificates is BerSet);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000435E0 File Offset: 0x000425E0
		private DerInteger CalculateVersion(DerObjectIdentifier contentOid, Asn1Set certs, Asn1Set crls, Asn1Set signerInfs)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					if (obj is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
						if (asn1TaggedObject.TagNo == 1)
						{
							flag3 = true;
						}
						else if (asn1TaggedObject.TagNo == 2)
						{
							flag4 = true;
						}
						else if (asn1TaggedObject.TagNo == 3)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				return new DerInteger(5);
			}
			if (crls != null)
			{
				foreach (object obj2 in crls)
				{
					if (obj2 is Asn1TaggedObject)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				return new DerInteger(5);
			}
			if (flag4)
			{
				return new DerInteger(4);
			}
			if (flag3)
			{
				return new DerInteger(3);
			}
			if (contentOid.Equals(CmsObjectIdentifiers.Data) && !this.CheckForVersion3(signerInfs))
			{
				return new DerInteger(1);
			}
			return new DerInteger(3);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00043714 File Offset: 0x00042714
		private bool CheckForVersion3(Asn1Set signerInfs)
		{
			foreach (object obj in signerInfs)
			{
				SignerInfo instance = SignerInfo.GetInstance(obj);
				if (instance.Version.Value.IntValue == 3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00043780 File Offset: 0x00042780
		private SignedData(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.digestAlgorithms = (Asn1Set)enumerator.Current;
			enumerator.MoveNext();
			this.contentInfo = ContentInfo.GetInstance(enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1Object asn1Object = (Asn1Object)obj;
				if (asn1Object is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Object;
					switch (asn1TaggedObject.TagNo)
					{
					case 0:
						this.certsBer = (asn1TaggedObject is BerTaggedObject);
						this.certificates = Asn1Set.GetInstance(asn1TaggedObject, false);
						break;
					case 1:
						this.crlsBer = (asn1TaggedObject is BerTaggedObject);
						this.crls = Asn1Set.GetInstance(asn1TaggedObject, false);
						break;
					default:
						throw new ArgumentException("unknown tag value " + asn1TaggedObject.TagNo);
					}
				}
				else
				{
					this.signerInfos = (Asn1Set)asn1Object;
				}
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00043887 File Offset: 0x00042887
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0004388F File Offset: 0x0004288F
		public Asn1Set DigestAlgorithms
		{
			get
			{
				return this.digestAlgorithms;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00043897 File Offset: 0x00042897
		public ContentInfo EncapContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0004389F File Offset: 0x0004289F
		public Asn1Set Certificates
		{
			get
			{
				return this.certificates;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000438A7 File Offset: 0x000428A7
		public Asn1Set CRLs
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000438AF File Offset: 0x000428AF
		public Asn1Set SignerInfos
		{
			get
			{
				return this.signerInfos;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000438B8 File Offset: 0x000428B8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithms,
				this.contentInfo
			});
			if (this.certificates != null)
			{
				if (this.certsBer)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new BerTaggedObject(false, 0, this.certificates)
					});
				}
				else
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 0, this.certificates)
					});
				}
			}
			if (this.crls != null)
			{
				if (this.crlsBer)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new BerTaggedObject(false, 1, this.crls)
					});
				}
				else
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 1, this.crls)
					});
				}
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.signerInfos
			});
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x0400098C RID: 2444
		private readonly DerInteger version;

		// Token: 0x0400098D RID: 2445
		private readonly Asn1Set digestAlgorithms;

		// Token: 0x0400098E RID: 2446
		private readonly ContentInfo contentInfo;

		// Token: 0x0400098F RID: 2447
		private readonly Asn1Set certificates;

		// Token: 0x04000990 RID: 2448
		private readonly Asn1Set crls;

		// Token: 0x04000991 RID: 2449
		private readonly Asn1Set signerInfos;

		// Token: 0x04000992 RID: 2450
		private readonly bool certsBer;

		// Token: 0x04000993 RID: 2451
		private readonly bool crlsBer;
	}
}

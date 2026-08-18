using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000575 RID: 1397
	public class SignedData : Asn1Encodable
	{
		// Token: 0x06002FA9 RID: 12201 RVA: 0x00126F7C File Offset: 0x00125F7C
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

		// Token: 0x06002FAA RID: 12202 RVA: 0x00126FCB File Offset: 0x00125FCB
		public SignedData(DerInteger _version, Asn1Set _digestAlgorithms, ContentInfo _contentInfo, Asn1Set _certificates, Asn1Set _crls, Asn1Set _signerInfos)
		{
			this.version = _version;
			this.digestAlgorithms = _digestAlgorithms;
			this.contentInfo = _contentInfo;
			this.certificates = _certificates;
			this.crls = _crls;
			this.signerInfos = _signerInfos;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00127000 File Offset: 0x00126000
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
				if (asn1Object is DerTaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Object;
					switch (derTaggedObject.TagNo)
					{
					case 0:
						this.certificates = Asn1Set.GetInstance(derTaggedObject, false);
						break;
					case 1:
						this.crls = Asn1Set.GetInstance(derTaggedObject, false);
						break;
					default:
						throw new ArgumentException("unknown tag value " + derTaggedObject.TagNo);
					}
				}
				else
				{
					this.signerInfos = (Asn1Set)asn1Object;
				}
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x001270E3 File Offset: 0x001260E3
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x001270EB File Offset: 0x001260EB
		public Asn1Set DigestAlgorithms
		{
			get
			{
				return this.digestAlgorithms;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x001270F3 File Offset: 0x001260F3
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x001270FB File Offset: 0x001260FB
		public Asn1Set Certificates
		{
			get
			{
				return this.certificates;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x00127103 File Offset: 0x00126103
		public Asn1Set Crls
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x0012710B File Offset: 0x0012610B
		public Asn1Set SignerInfos
		{
			get
			{
				return this.signerInfos;
			}
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x00127114 File Offset: 0x00126114
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
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.certificates)
				});
			}
			if (this.crls != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.crls)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.signerInfos
			});
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040020C5 RID: 8389
		private readonly DerInteger version;

		// Token: 0x040020C6 RID: 8390
		private readonly Asn1Set digestAlgorithms;

		// Token: 0x040020C7 RID: 8391
		private readonly ContentInfo contentInfo;

		// Token: 0x040020C8 RID: 8392
		private readonly Asn1Set certificates;

		// Token: 0x040020C9 RID: 8393
		private readonly Asn1Set crls;

		// Token: 0x040020CA RID: 8394
		private readonly Asn1Set signerInfos;
	}
}

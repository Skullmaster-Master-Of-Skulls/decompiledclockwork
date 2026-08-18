using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000258 RID: 600
	public class AttributeCertificateInfo : Asn1Encodable
	{
		// Token: 0x060016C9 RID: 5833 RVA: 0x00083909 File Offset: 0x00082909
		public static AttributeCertificateInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AttributeCertificateInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00083918 File Offset: 0x00082918
		public static AttributeCertificateInfo GetInstance(object obj)
		{
			if (obj is AttributeCertificateInfo)
			{
				return (AttributeCertificateInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeCertificateInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00083968 File Offset: 0x00082968
		private AttributeCertificateInfo(Asn1Sequence seq)
		{
			if (seq.Count < 7 || seq.Count > 9)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.version = DerInteger.GetInstance(seq[0]);
			this.holder = Holder.GetInstance(seq[1]);
			this.issuer = AttCertIssuer.GetInstance(seq[2]);
			this.signature = AlgorithmIdentifier.GetInstance(seq[3]);
			this.serialNumber = DerInteger.GetInstance(seq[4]);
			this.attrCertValidityPeriod = AttCertValidityPeriod.GetInstance(seq[5]);
			this.attributes = Asn1Sequence.GetInstance(seq[6]);
			for (int i = 7; i < seq.Count; i++)
			{
				Asn1Encodable asn1Encodable = seq[i];
				if (asn1Encodable is DerBitString)
				{
					this.issuerUniqueID = DerBitString.GetInstance(seq[i]);
				}
				else if (asn1Encodable is Asn1Sequence || asn1Encodable is X509Extensions)
				{
					this.extensions = X509Extensions.GetInstance(seq[i]);
				}
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00083A7E File Offset: 0x00082A7E
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00083A86 File Offset: 0x00082A86
		public Holder Holder
		{
			get
			{
				return this.holder;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00083A8E File Offset: 0x00082A8E
		public AttCertIssuer Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00083A96 File Offset: 0x00082A96
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00083A9E File Offset: 0x00082A9E
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00083AA6 File Offset: 0x00082AA6
		public AttCertValidityPeriod AttrCertValidityPeriod
		{
			get
			{
				return this.attrCertValidityPeriod;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00083AAE File Offset: 0x00082AAE
		public Asn1Sequence Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00083AB6 File Offset: 0x00082AB6
		public DerBitString IssuerUniqueID
		{
			get
			{
				return this.issuerUniqueID;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00083ABE File Offset: 0x00082ABE
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00083AC8 File Offset: 0x00082AC8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.holder,
				this.issuer,
				this.signature,
				this.serialNumber,
				this.attrCertValidityPeriod,
				this.attributes
			});
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerUniqueID
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.extensions
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FAB RID: 4011
		internal readonly DerInteger version;

		// Token: 0x04000FAC RID: 4012
		internal readonly Holder holder;

		// Token: 0x04000FAD RID: 4013
		internal readonly AttCertIssuer issuer;

		// Token: 0x04000FAE RID: 4014
		internal readonly AlgorithmIdentifier signature;

		// Token: 0x04000FAF RID: 4015
		internal readonly DerInteger serialNumber;

		// Token: 0x04000FB0 RID: 4016
		internal readonly AttCertValidityPeriod attrCertValidityPeriod;

		// Token: 0x04000FB1 RID: 4017
		internal readonly Asn1Sequence attributes;

		// Token: 0x04000FB2 RID: 4018
		internal readonly DerBitString issuerUniqueID;

		// Token: 0x04000FB3 RID: 4019
		internal readonly X509Extensions extensions;
	}
}

using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000140 RID: 320
	public class TbsCertificateStructure : Asn1Encodable
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x00040C92 File Offset: 0x0003FC92
		public static TbsCertificateStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsCertificateStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00040CA0 File Offset: 0x0003FCA0
		public static TbsCertificateStructure GetInstance(object obj)
		{
			if (obj is TbsCertificateStructure)
			{
				return (TbsCertificateStructure)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsCertificateStructure((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00040CF0 File Offset: 0x0003FCF0
		internal TbsCertificateStructure(Asn1Sequence seq)
		{
			int num = 0;
			this.seq = seq;
			if (seq[0] is DerTaggedObject)
			{
				this.version = DerInteger.GetInstance(seq[0]);
			}
			else
			{
				num = -1;
				this.version = new DerInteger(0);
			}
			this.serialNumber = DerInteger.GetInstance(seq[num + 1]);
			this.signature = AlgorithmIdentifier.GetInstance(seq[num + 2]);
			this.issuer = X509Name.GetInstance(seq[num + 3]);
			Asn1Sequence asn1Sequence = (Asn1Sequence)seq[num + 4];
			this.startDate = Time.GetInstance(asn1Sequence[0]);
			this.endDate = Time.GetInstance(asn1Sequence[1]);
			this.subject = X509Name.GetInstance(seq[num + 5]);
			this.subjectPublicKeyInfo = SubjectPublicKeyInfo.GetInstance(seq[num + 6]);
			for (int i = seq.Count - (num + 6) - 1; i > 0; i--)
			{
				DerTaggedObject derTaggedObject = (DerTaggedObject)seq[num + 6 + i];
				switch (derTaggedObject.TagNo)
				{
				case 1:
					this.issuerUniqueID = DerBitString.GetInstance(derTaggedObject, false);
					break;
				case 2:
					this.subjectUniqueID = DerBitString.GetInstance(derTaggedObject, false);
					break;
				case 3:
					this.extensions = X509Extensions.GetInstance(derTaggedObject);
					break;
				}
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00040E44 File Offset: 0x0003FE44
		public int Version
		{
			get
			{
				return this.version.Value.IntValue + 1;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00040E58 File Offset: 0x0003FE58
		public DerInteger VersionNumber
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00040E60 File Offset: 0x0003FE60
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00040E68 File Offset: 0x0003FE68
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00040E70 File Offset: 0x0003FE70
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00040E78 File Offset: 0x0003FE78
		public Time StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00040E80 File Offset: 0x0003FE80
		public Time EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00040E88 File Offset: 0x0003FE88
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00040E90 File Offset: 0x0003FE90
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.subjectPublicKeyInfo;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00040E98 File Offset: 0x0003FE98
		public DerBitString IssuerUniqueID
		{
			get
			{
				return this.issuerUniqueID;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00040EA0 File Offset: 0x0003FEA0
		public DerBitString SubjectUniqueID
		{
			get
			{
				return this.subjectUniqueID;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00040EA8 File Offset: 0x0003FEA8
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00040EB0 File Offset: 0x0003FEB0
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000918 RID: 2328
		internal Asn1Sequence seq;

		// Token: 0x04000919 RID: 2329
		internal DerInteger version;

		// Token: 0x0400091A RID: 2330
		internal DerInteger serialNumber;

		// Token: 0x0400091B RID: 2331
		internal AlgorithmIdentifier signature;

		// Token: 0x0400091C RID: 2332
		internal X509Name issuer;

		// Token: 0x0400091D RID: 2333
		internal Time startDate;

		// Token: 0x0400091E RID: 2334
		internal Time endDate;

		// Token: 0x0400091F RID: 2335
		internal X509Name subject;

		// Token: 0x04000920 RID: 2336
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;

		// Token: 0x04000921 RID: 2337
		internal DerBitString issuerUniqueID;

		// Token: 0x04000922 RID: 2338
		internal DerBitString subjectUniqueID;

		// Token: 0x04000923 RID: 2339
		internal X509Extensions extensions;
	}
}

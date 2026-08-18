using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200044C RID: 1100
	public class CertificationRequestInfo : Asn1Encodable
	{
		// Token: 0x0600252F RID: 9519 RVA: 0x000E19C8 File Offset: 0x000E09C8
		public static CertificationRequestInfo GetInstance(object obj)
		{
			if (obj is CertificationRequestInfo)
			{
				return (CertificationRequestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificationRequestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000E1A18 File Offset: 0x000E0A18
		public CertificationRequestInfo(X509Name subject, SubjectPublicKeyInfo pkInfo, Asn1Set attributes)
		{
			this.subject = subject;
			this.subjectPKInfo = pkInfo;
			this.attributes = attributes;
			if (subject == null || this.version == null || this.subjectPKInfo == null)
			{
				throw new ArgumentException("Not all mandatory fields set in CertificationRequestInfo generator.");
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000E1A6C File Offset: 0x000E0A6C
		private CertificationRequestInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.subject = X509Name.GetInstance(seq[1]);
			this.subjectPKInfo = SubjectPublicKeyInfo.GetInstance(seq[2]);
			if (seq.Count > 3)
			{
				DerTaggedObject obj = (DerTaggedObject)seq[3];
				this.attributes = Asn1Set.GetInstance(obj, false);
			}
			if (this.subject == null || this.version == null || this.subjectPKInfo == null)
			{
				throw new ArgumentException("Not all mandatory fields set in CertificationRequestInfo generator.");
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000E1B07 File Offset: 0x000E0B07
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000E1B0F File Offset: 0x000E0B0F
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000E1B17 File Offset: 0x000E0B17
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.subjectPKInfo;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000E1B1F File Offset: 0x000E0B1F
		public Asn1Set Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000E1B28 File Offset: 0x000E0B28
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.subject,
				this.subjectPKInfo
			});
			if (this.attributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.attributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001A14 RID: 6676
		internal DerInteger version = new DerInteger(0);

		// Token: 0x04001A15 RID: 6677
		internal X509Name subject;

		// Token: 0x04001A16 RID: 6678
		internal SubjectPublicKeyInfo subjectPKInfo;

		// Token: 0x04001A17 RID: 6679
		internal Asn1Set attributes;
	}
}

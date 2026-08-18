using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020004CE RID: 1230
	public class X509CertificateStructure : Asn1Encodable
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x000FFACD File Offset: 0x000FEACD
		public static X509CertificateStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509CertificateStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000FFADC File Offset: 0x000FEADC
		public static X509CertificateStructure GetInstance(object obj)
		{
			if (obj is X509CertificateStructure)
			{
				return (X509CertificateStructure)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new X509CertificateStructure((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000FFB2C File Offset: 0x000FEB2C
		public X509CertificateStructure(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlgID, DerBitString sig)
		{
			if (tbsCert == null)
			{
				throw new ArgumentNullException("tbsCert");
			}
			if (sigAlgID == null)
			{
				throw new ArgumentNullException("sigAlgID");
			}
			if (sig == null)
			{
				throw new ArgumentNullException("sig");
			}
			this.tbsCert = tbsCert;
			this.sigAlgID = sigAlgID;
			this.sig = sig;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000FFB80 File Offset: 0x000FEB80
		private X509CertificateStructure(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("sequence wrong size for a certificate", "seq");
			}
			this.tbsCert = TbsCertificateStructure.GetInstance(seq[0]);
			this.sigAlgID = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sig = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000FFBE2 File Offset: 0x000FEBE2
		public TbsCertificateStructure TbsCertificate
		{
			get
			{
				return this.tbsCert;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000FFBEA File Offset: 0x000FEBEA
		public int Version
		{
			get
			{
				return this.tbsCert.Version;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000FFBF7 File Offset: 0x000FEBF7
		public DerInteger SerialNumber
		{
			get
			{
				return this.tbsCert.SerialNumber;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x000FFC04 File Offset: 0x000FEC04
		public X509Name Issuer
		{
			get
			{
				return this.tbsCert.Issuer;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060029F4 RID: 10740 RVA: 0x000FFC11 File Offset: 0x000FEC11
		public Time StartDate
		{
			get
			{
				return this.tbsCert.StartDate;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x000FFC1E File Offset: 0x000FEC1E
		public Time EndDate
		{
			get
			{
				return this.tbsCert.EndDate;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x000FFC2B File Offset: 0x000FEC2B
		public X509Name Subject
		{
			get
			{
				return this.tbsCert.Subject;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000FFC38 File Offset: 0x000FEC38
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.tbsCert.SubjectPublicKeyInfo;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000FFC45 File Offset: 0x000FEC45
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgID;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000FFC4D File Offset: 0x000FEC4D
		public DerBitString Signature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000FFC58 File Offset: 0x000FEC58
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.tbsCert,
				this.sigAlgID,
				this.sig
			});
		}

		// Token: 0x04001D41 RID: 7489
		private readonly TbsCertificateStructure tbsCert;

		// Token: 0x04001D42 RID: 7490
		private readonly AlgorithmIdentifier sigAlgID;

		// Token: 0x04001D43 RID: 7491
		private readonly DerBitString sig;
	}
}

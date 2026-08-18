using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020004D0 RID: 1232
	public class CertificateList : Asn1Encodable
	{
		// Token: 0x06002A04 RID: 10756 RVA: 0x000FFE0F File Offset: 0x000FEE0F
		public static CertificateList GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CertificateList.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000FFE20 File Offset: 0x000FEE20
		public static CertificateList GetInstance(object obj)
		{
			if (obj is CertificateList)
			{
				return (CertificateList)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificateList((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000FFE70 File Offset: 0x000FEE70
		private CertificateList(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("sequence wrong size for CertificateList", "seq");
			}
			this.tbsCertList = TbsCertificateList.GetInstance(seq[0]);
			this.sigAlgID = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sig = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x000FFED2 File Offset: 0x000FEED2
		public TbsCertificateList TbsCertList
		{
			get
			{
				return this.tbsCertList;
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000FFEDA File Offset: 0x000FEEDA
		public CrlEntry[] GetRevokedCertificates()
		{
			return this.tbsCertList.GetRevokedCertificates();
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000FFEE7 File Offset: 0x000FEEE7
		public IEnumerable GetRevokedCertificateEnumeration()
		{
			return this.tbsCertList.GetRevokedCertificateEnumeration();
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000FFEF4 File Offset: 0x000FEEF4
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgID;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000FFEFC File Offset: 0x000FEEFC
		public DerBitString Signature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000FFF04 File Offset: 0x000FEF04
		public int Version
		{
			get
			{
				return this.tbsCertList.Version;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x000FFF11 File Offset: 0x000FEF11
		public X509Name Issuer
		{
			get
			{
				return this.tbsCertList.Issuer;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x000FFF1E File Offset: 0x000FEF1E
		public Time ThisUpdate
		{
			get
			{
				return this.tbsCertList.ThisUpdate;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000FFF2B File Offset: 0x000FEF2B
		public Time NextUpdate
		{
			get
			{
				return this.tbsCertList.NextUpdate;
			}
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000FFF38 File Offset: 0x000FEF38
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.tbsCertList,
				this.sigAlgID,
				this.sig
			});
		}

		// Token: 0x04001D48 RID: 7496
		private readonly TbsCertificateList tbsCertList;

		// Token: 0x04001D49 RID: 7497
		private readonly AlgorithmIdentifier sigAlgID;

		// Token: 0x04001D4A RID: 7498
		private readonly DerBitString sig;
	}
}

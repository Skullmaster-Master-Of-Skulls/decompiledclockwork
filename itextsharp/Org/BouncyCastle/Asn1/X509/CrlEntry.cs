using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000483 RID: 1155
	public class CrlEntry : Asn1Encodable
	{
		// Token: 0x0600272F RID: 10031 RVA: 0x000ED264 File Offset: 0x000EC264
		public CrlEntry(Asn1Sequence seq)
		{
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.seq = seq;
			this.userCertificate = DerInteger.GetInstance(seq[0]);
			this.revocationDate = Time.GetInstance(seq[1]);
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000ED2CF File Offset: 0x000EC2CF
		public DerInteger UserCertificate
		{
			get
			{
				return this.userCertificate;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x000ED2D7 File Offset: 0x000EC2D7
		public Time RevocationDate
		{
			get
			{
				return this.revocationDate;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x000ED2DF File Offset: 0x000EC2DF
		public X509Extensions Extensions
		{
			get
			{
				if (this.crlEntryExtensions == null && this.seq.Count == 3)
				{
					this.crlEntryExtensions = X509Extensions.GetInstance(this.seq[2]);
				}
				return this.crlEntryExtensions;
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000ED314 File Offset: 0x000EC314
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04001B03 RID: 6915
		internal Asn1Sequence seq;

		// Token: 0x04001B04 RID: 6916
		internal DerInteger userCertificate;

		// Token: 0x04001B05 RID: 6917
		internal Time revocationDate;

		// Token: 0x04001B06 RID: 6918
		internal X509Extensions crlEntryExtensions;
	}
}

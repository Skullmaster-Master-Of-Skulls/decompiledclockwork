using System;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000429 RID: 1065
	public class SingleResp : X509ExtensionBase
	{
		// Token: 0x06002438 RID: 9272 RVA: 0x000DC9BD File Offset: 0x000DB9BD
		public SingleResp(SingleResponse resp)
		{
			this.resp = resp;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000DC9CC File Offset: 0x000DB9CC
		public CertificateID GetCertID()
		{
			return new CertificateID(this.resp.CertId);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000DC9E0 File Offset: 0x000DB9E0
		public object GetCertStatus()
		{
			CertStatus certStatus = this.resp.CertStatus;
			if (certStatus.TagNo == 0)
			{
				return null;
			}
			if (certStatus.TagNo == 1)
			{
				return new RevokedStatus(RevokedInfo.GetInstance(certStatus.Status));
			}
			return new UnknownStatus();
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x000DCA22 File Offset: 0x000DBA22
		public DateTime ThisUpdate
		{
			get
			{
				return this.resp.ThisUpdate.ToDateTime();
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x000DCA34 File Offset: 0x000DBA34
		public DateTimeObject NextUpdate
		{
			get
			{
				if (this.resp.NextUpdate != null)
				{
					return new DateTimeObject(this.resp.NextUpdate.ToDateTime());
				}
				return null;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000DCA5A File Offset: 0x000DBA5A
		public X509Extensions SingleExtensions
		{
			get
			{
				return this.resp.SingleExtensions;
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000DCA67 File Offset: 0x000DBA67
		protected override X509Extensions GetX509Extensions()
		{
			return this.SingleExtensions;
		}

		// Token: 0x04001925 RID: 6437
		internal readonly SingleResponse resp;
	}
}

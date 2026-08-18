using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000470 RID: 1136
	public class RevokedStatus : CertificateStatus
	{
		// Token: 0x060026C2 RID: 9922 RVA: 0x000EAB03 File Offset: 0x000E9B03
		public RevokedStatus(RevokedInfo info)
		{
			this.info = info;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000EAB12 File Offset: 0x000E9B12
		public RevokedStatus(DateTime revocationDate, int reason)
		{
			this.info = new RevokedInfo(new DerGeneralizedTime(revocationDate), new CrlReason(reason));
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000EAB31 File Offset: 0x000E9B31
		public DateTime RevocationTime
		{
			get
			{
				return this.info.RevocationTime.ToDateTime();
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000EAB43 File Offset: 0x000E9B43
		public bool HasRevocationReason
		{
			get
			{
				return this.info.RevocationReason != null;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x000EAB56 File Offset: 0x000E9B56
		public int RevocationReason
		{
			get
			{
				if (this.info.RevocationReason == null)
				{
					throw new InvalidOperationException("attempt to get a reason where none is available");
				}
				return this.info.RevocationReason.Value.IntValue;
			}
		}

		// Token: 0x04001AB8 RID: 6840
		internal readonly RevokedInfo info;
	}
}

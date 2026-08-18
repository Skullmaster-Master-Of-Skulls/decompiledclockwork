using System;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000078 RID: 120
	public class CertStatus
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001525C File Offset: 0x0001425C
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00015264 File Offset: 0x00014264
		public DateTimeObject RevocationDate
		{
			get
			{
				return this.revocationDate;
			}
			set
			{
				this.revocationDate = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001526D File Offset: 0x0001426D
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00015275 File Offset: 0x00014275
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x04000204 RID: 516
		public const int Unrevoked = 11;

		// Token: 0x04000205 RID: 517
		public const int Undetermined = 12;

		// Token: 0x04000206 RID: 518
		private int status = 11;

		// Token: 0x04000207 RID: 519
		private DateTimeObject revocationDate;
	}
}

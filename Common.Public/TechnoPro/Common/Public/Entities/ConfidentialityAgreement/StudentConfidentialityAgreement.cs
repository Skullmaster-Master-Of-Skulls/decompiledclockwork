using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ConfidentialityAgreement
{
	// Token: 0x02000442 RID: 1090
	public class StudentConfidentialityAgreement : BusinessBase<int>
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00025374 File Offset: 0x00023574
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x0000E258 File Offset: 0x0000C458
		public int StudentConfidentialityAgreementId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0002538C File Offset: 0x0002358C
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x00025394 File Offset: 0x00023594
		public DateTime SignedOn { get; set; }

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0002539D File Offset: 0x0002359D
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x000253A5 File Offset: 0x000235A5
		public PersonBase Student { get; set; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000253AE File Offset: 0x000235AE
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x000253B6 File Offset: 0x000235B6
		public eClockWorkModules ModuleName { get; set; }
	}
}

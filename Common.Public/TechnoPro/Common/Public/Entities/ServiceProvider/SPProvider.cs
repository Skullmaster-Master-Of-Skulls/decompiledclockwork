using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E7 RID: 487
	public class SPProvider : BusinessBase<int>
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x000161CC File Offset: 0x000143CC
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPProviderId
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x000161E4 File Offset: 0x000143E4
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x000161EC File Offset: 0x000143EC
		public PersonBase Person { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x000161F5 File Offset: 0x000143F5
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x000161FD File Offset: 0x000143FD
		public string UserName { get; set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00016206 File Offset: 0x00014406
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0001620E File Offset: 0x0001440E
		public string ExternalId { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00016217 File Offset: 0x00014417
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x0001621F File Offset: 0x0001441F
		public string Specializations { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00016228 File Offset: 0x00014428
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00016230 File Offset: 0x00014430
		public string Note1 { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00016239 File Offset: 0x00014439
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00016241 File Offset: 0x00014441
		public string Note2 { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0001624A File Offset: 0x0001444A
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x00016252 File Offset: 0x00014452
		public string Email { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0001625B File Offset: 0x0001445B
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x00016263 File Offset: 0x00014463
		public string AlternateEmail { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0001626C File Offset: 0x0001446C
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x00016274 File Offset: 0x00014474
		public string Phone1 { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0001627D File Offset: 0x0001447D
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x00016285 File Offset: 0x00014485
		public string Phone2 { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0001628E File Offset: 0x0001448E
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x00016296 File Offset: 0x00014496
		public string PhoneNote { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0001629F File Offset: 0x0001449F
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x000162A7 File Offset: 0x000144A7
		public string Address1 { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000162B0 File Offset: 0x000144B0
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x000162B8 File Offset: 0x000144B8
		public string Address2 { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000162C1 File Offset: 0x000144C1
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x000162C9 File Offset: 0x000144C9
		public bool Address1IsPrimary { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000162D2 File Offset: 0x000144D2
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x000162DA File Offset: 0x000144DA
		public bool IsActive { get; set; }
	}
}

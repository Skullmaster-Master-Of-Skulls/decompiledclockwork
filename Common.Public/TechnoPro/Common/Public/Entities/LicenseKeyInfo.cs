using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public class LicenseKeyInfo : BusinessBase<string>
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000EA54 File Offset: 0x0000CC54
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public virtual string ProductName { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000EA68 File Offset: 0x0000CC68
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string LicenseKey
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000EA80 File Offset: 0x0000CC80
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public virtual DateTime IssuedDate { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000EA91 File Offset: 0x0000CC91
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000EA99 File Offset: 0x0000CC99
		public virtual DateTime? ExpiryDate { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000EAA2 File Offset: 0x0000CCA2
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000EAAA File Offset: 0x0000CCAA
		public virtual LicenseType LicenseType { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000EABB File Offset: 0x0000CCBB
		public virtual int NLicenses { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public virtual string LicensedTo { get; set; }
	}
}

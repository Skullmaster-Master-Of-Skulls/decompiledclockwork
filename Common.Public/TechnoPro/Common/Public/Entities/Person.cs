using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000F4 RID: 244
	public class Person : BusinessBase<string>
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000EB71 File Offset: 0x0000CD71
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000EB79 File Offset: 0x0000CD79
		public virtual int PersonID { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000EB82 File Offset: 0x0000CD82
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		public virtual string FirstName { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000EB93 File Offset: 0x0000CD93
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000EB9B File Offset: 0x0000CD9B
		public virtual string LastName { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public virtual string MiddleName { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E9 RID: 489
	public class SPProviderType : BusinessBase<int>
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00016340 File Offset: 0x00014540
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPProviderTypeId
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

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00016358 File Offset: 0x00014558
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00016360 File Offset: 0x00014560
		public string Title { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00016369 File Offset: 0x00014569
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x00016371 File Offset: 0x00014571
		public string Description { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0001637A File Offset: 0x0001457A
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00016382 File Offset: 0x00014582
		public eProviderTypeBehaviourCode BehaviourCode { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0001638B File Offset: 0x0001458B
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x00016393 File Offset: 0x00014593
		public bool IsActive { get; set; }
	}
}

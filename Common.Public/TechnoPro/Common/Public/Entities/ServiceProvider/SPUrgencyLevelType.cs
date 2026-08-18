using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001F3 RID: 499
	public class SPUrgencyLevelType : BusinessBase<int>
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x000167B8 File Offset: 0x000149B8
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPUrgencyLevelTypeId
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

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x000167D0 File Offset: 0x000149D0
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x000167D8 File Offset: 0x000149D8
		public string Title { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x000167E1 File Offset: 0x000149E1
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x000167E9 File Offset: 0x000149E9
		public string Description { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000167F2 File Offset: 0x000149F2
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x000167FA File Offset: 0x000149FA
		public int Urgency { get; set; }
	}
}

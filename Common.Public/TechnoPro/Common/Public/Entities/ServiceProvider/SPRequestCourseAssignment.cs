using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001EE RID: 494
	public class SPRequestCourseAssignment : BusinessBase<int>
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x000165BC File Offset: 0x000147BC
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestCourseAssignmentId
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

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000165D4 File Offset: 0x000147D4
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x000165DC File Offset: 0x000147DC
		public SPProvider Provider { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000165E5 File Offset: 0x000147E5
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x000165ED File Offset: 0x000147ED
		public LookupCourseBase Course { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x000165F6 File Offset: 0x000147F6
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x000165FE File Offset: 0x000147FE
		public string Notes { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00016607 File Offset: 0x00014807
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0001660F File Offset: 0x0001480F
		public bool IsActive { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00016618 File Offset: 0x00014818
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00016620 File Offset: 0x00014820
		public DateTime? DateCancelled { get; set; }
	}
}

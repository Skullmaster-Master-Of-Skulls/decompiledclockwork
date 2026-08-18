using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001F0 RID: 496
	public class SPRequestEventAssignment : BusinessBase<int>
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x000166CC File Offset: 0x000148CC
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestEventAssignmentId
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

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000166E4 File Offset: 0x000148E4
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x000166EC File Offset: 0x000148EC
		public SPProvider AssignedProvider { get; set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000166F5 File Offset: 0x000148F5
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x000166FD File Offset: 0x000148FD
		public string Notes { get; set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00016706 File Offset: 0x00014906
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x0001670E File Offset: 0x0001490E
		public bool IsActive { get; set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00016717 File Offset: 0x00014917
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x0001671F File Offset: 0x0001491F
		public DateTime? DateCancelled { get; set; }
	}
}

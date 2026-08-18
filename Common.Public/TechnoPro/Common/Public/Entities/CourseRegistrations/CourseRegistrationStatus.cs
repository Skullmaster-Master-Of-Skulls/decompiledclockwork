using System;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x02000436 RID: 1078
	public class CourseRegistrationStatus : BusinessBase<int>
	{
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00024C84 File Offset: 0x00022E84
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CourseRegistrationStatusId
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

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00024C9C File Offset: 0x00022E9C
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x00024CA4 File Offset: 0x00022EA4
		public string Title { get; set; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00024CAD File Offset: 0x00022EAD
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x00024CB5 File Offset: 0x00022EB5
		public string Description { get; set; }

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00024CBE File Offset: 0x00022EBE
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x00024CC6 File Offset: 0x00022EC6
		public bool IsRegistered { get; set; }
	}
}

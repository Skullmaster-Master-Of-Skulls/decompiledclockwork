using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E6 RID: 486
	public class SPApplicationCourse : BusinessBase<int>
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0001614C File Offset: 0x0001434C
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPApplicationCourseId
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

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00016164 File Offset: 0x00014364
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0001616C File Offset: 0x0001436C
		public SPApplication Application { get; set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00016175 File Offset: 0x00014375
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0001617D File Offset: 0x0001437D
		public SPProviderCourseRegistration ProviderCourseRegistration { get; set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00016186 File Offset: 0x00014386
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0001618E File Offset: 0x0001438E
		public string LookupSubject { get; set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00016197 File Offset: 0x00014397
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0001619F File Offset: 0x0001439F
		public string LookupCourseCode { get; set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000161A8 File Offset: 0x000143A8
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x000161B0 File Offset: 0x000143B0
		public string LookupCourseSection { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x000161B9 File Offset: 0x000143B9
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x000161C1 File Offset: 0x000143C1
		public string LookupTimeOfDay { get; set; }
	}
}

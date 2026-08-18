using System;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E8 RID: 488
	public class SPProviderCourseRegistration : BusinessBase<int>
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x000162E4 File Offset: 0x000144E4
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPProviderCourseRegistrationId
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

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x000162FC File Offset: 0x000144FC
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x00016304 File Offset: 0x00014504
		public SPProvider Provider { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0001630D File Offset: 0x0001450D
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00016315 File Offset: 0x00014515
		public LookupCourseBase Course { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0001631E File Offset: 0x0001451E
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00016326 File Offset: 0x00014526
		public CourseRegistrationStatus RegistrationStatus { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0001632F File Offset: 0x0001452F
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00016337 File Offset: 0x00014537
		public bool IsExemptFromDataSync { get; set; }
	}
}

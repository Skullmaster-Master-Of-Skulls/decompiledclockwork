using System;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001FC RID: 508
	public class ServiceProviderApplicationCourse : BusinessBase<int>
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00016A90 File Offset: 0x00014C90
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00016AA8 File Offset: 0x00014CA8
		public new virtual int Id
		{
			get
			{
				return this.ServiceProviderApplicationCourseId;
			}
			set
			{
				this.ServiceProviderApplicationCourseId = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00016AB3 File Offset: 0x00014CB3
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00016ABB File Offset: 0x00014CBB
		public int ServiceProviderApplicationCourseId { get; set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00016AC4 File Offset: 0x00014CC4
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00016ACC File Offset: 0x00014CCC
		public LookupCourseBase CourseBase { get; set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00016AD5 File Offset: 0x00014CD5
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00016ADD File Offset: 0x00014CDD
		public CourseRegistrationStatus RegistrationStatus { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00016AE6 File Offset: 0x00014CE6
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00016AEE File Offset: 0x00014CEE
		public DateTime? DateCancelled { get; set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00016AF7 File Offset: 0x00014CF7
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00016AFF File Offset: 0x00014CFF
		public string Note { get; set; }
	}
}

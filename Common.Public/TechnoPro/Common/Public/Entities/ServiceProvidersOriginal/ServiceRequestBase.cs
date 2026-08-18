using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x02000205 RID: 517
	public class ServiceRequestBase : BusinessBase<int>
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00016FA4 File Offset: 0x000151A4
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderRequestId
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

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00016FBC File Offset: 0x000151BC
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00016FC4 File Offset: 0x000151C4
		public PersonBase Student { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00016FCD File Offset: 0x000151CD
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00016FD5 File Offset: 0x000151D5
		public LookupCourseBase CourseBase { get; set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00016FDE File Offset: 0x000151DE
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00016FE6 File Offset: 0x000151E6
		public int AssignedServiceProviderId { get; set; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00016FEF File Offset: 0x000151EF
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00016FF7 File Offset: 0x000151F7
		public LookupCourseBase AssignedServiceProviderCourse { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00017000 File Offset: 0x00015200
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00017008 File Offset: 0x00015208
		public bool IsAssignedPrivate { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E8 RID: 744
	public class LookupInstructorCourseInfo : BusinessBase<int>
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0001BA24 File Offset: 0x00019C24
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int LuCourseId
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

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0001BA3C File Offset: 0x00019C3C
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0001BA44 File Offset: 0x00019C44
		public ePermissionForCourse PermissionForCourse { get; set; }
	}
}

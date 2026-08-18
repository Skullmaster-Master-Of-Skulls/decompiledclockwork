using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007DC RID: 2012
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupInstructorCourseInfoDTO : ICloneable<LookupInstructorCourseInfoDTO>, ICloneable
	{
		// Token: 0x0600290B RID: 10507 RVA: 0x000036BD File Offset: 0x000018BD
		public LookupInstructorCourseInfoDTO()
		{
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000136C0 File Offset: 0x000118C0
		public LookupInstructorCourseInfoDTO(LookupInstructorCourseInfoDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.LuCourseId = item.LuCourseId;
				this.PermissionForCourse = item.PermissionForCourse;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x000136F9 File Offset: 0x000118F9
		// (set) Token: 0x0600290E RID: 10510 RVA: 0x00013701 File Offset: 0x00011901
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x0001370A File Offset: 0x0001190A
		// (set) Token: 0x06002910 RID: 10512 RVA: 0x00013712 File Offset: 0x00011912
		[DataMember]
		public ePermissionForCourseDTO PermissionForCourse { get; set; }

		// Token: 0x06002911 RID: 10513 RVA: 0x0001371C File Offset: 0x0001191C
		public LookupInstructorCourseInfoDTO Clone()
		{
			return new LookupInstructorCourseInfoDTO(this);
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00013734 File Offset: 0x00011934
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}

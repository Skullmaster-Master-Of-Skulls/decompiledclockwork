using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000293 RID: 659
	public class DuplicateCourse
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00019B56 File Offset: 0x00017D56
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x00019B5E File Offset: 0x00017D5E
		public LookupCourseBase LookupCourse { get; set; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00019B67 File Offset: 0x00017D67
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x00019B6F File Offset: 0x00017D6F
		public LookupCourseRelatedInfo CourseRelatedInfo { get; set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00019B78 File Offset: 0x00017D78
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x00019B80 File Offset: 0x00017D80
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}

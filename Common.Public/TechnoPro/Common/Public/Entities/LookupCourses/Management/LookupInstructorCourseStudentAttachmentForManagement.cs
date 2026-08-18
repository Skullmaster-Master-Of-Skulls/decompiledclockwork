using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses.Management
{
	// Token: 0x020002F3 RID: 755
	public class LookupInstructorCourseStudentAttachmentForManagement
	{
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0001C15D File Offset: 0x0001A35D
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x0001C165 File Offset: 0x0001A365
		public int PersonId { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0001C16E File Offset: 0x0001A36E
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x0001C176 File Offset: 0x0001A376
		public string StudentNumber { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0001C17F File Offset: 0x0001A37F
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0001C187 File Offset: 0x0001A387
		public string Name { get; set; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0001C190 File Offset: 0x0001A390
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x0001C198 File Offset: 0x0001A398
		public bool IsCourseDropped { get; set; }
	}
}

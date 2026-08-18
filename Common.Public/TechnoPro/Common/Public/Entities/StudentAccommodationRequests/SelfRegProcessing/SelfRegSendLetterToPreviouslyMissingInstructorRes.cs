using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x020001AB RID: 427
	public class SelfRegSendLetterToPreviouslyMissingInstructorRes
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00013D4C File Offset: 0x00011F4C
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00013D54 File Offset: 0x00011F54
		public StudentCourseAccommodationRequest Request { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00013D5D File Offset: 0x00011F5D
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00013D65 File Offset: 0x00011F65
		public TryToSendInstructorEmailResult EmailResult { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x020001AA RID: 426
	public class SelfRegCourseInfo
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00013D19 File Offset: 0x00011F19
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00013D21 File Offset: 0x00011F21
		public int LuCourseId { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00013D2A File Offset: 0x00011F2A
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x00013D32 File Offset: 0x00011F32
		public string CourseDescription { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00013D3B File Offset: 0x00011F3B
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x00013D43 File Offset: 0x00011F43
		public string EncodedLucidForUrl { get; set; }
	}
}

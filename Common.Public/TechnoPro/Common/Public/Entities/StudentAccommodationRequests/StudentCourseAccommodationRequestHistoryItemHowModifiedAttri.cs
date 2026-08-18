using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x0200019C RID: 412
	public class StudentCourseAccommodationRequestHistoryItemHowModifiedAttribute : Attribute
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public StudentCourseAccommodationRequestHistoryItemHowModifiedAttribute()
		{
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00013913 File Offset: 0x00011B13
		public StudentCourseAccommodationRequestHistoryItemHowModifiedAttribute(string displayTitle)
		{
			this.DisplayTitle = displayTitle;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00013925 File Offset: 0x00011B25
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0001392D File Offset: 0x00011B2D
		public string DisplayTitle { get; set; }
	}
}

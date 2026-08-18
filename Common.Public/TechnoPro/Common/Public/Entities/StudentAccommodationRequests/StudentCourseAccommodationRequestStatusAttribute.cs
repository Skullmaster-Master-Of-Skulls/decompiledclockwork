using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x020001A2 RID: 418
	public class StudentCourseAccommodationRequestStatusAttribute : Attribute
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public StudentCourseAccommodationRequestStatusAttribute()
		{
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00013B12 File Offset: 0x00011D12
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x00013B1A File Offset: 0x00011D1A
		public string DisplayTitle { get; set; }

		// Token: 0x06000ACC RID: 2764 RVA: 0x00013B23 File Offset: 0x00011D23
		public StudentCourseAccommodationRequestStatusAttribute(string displayTitle)
		{
			this.DisplayTitle = displayTitle;
		}
	}
}

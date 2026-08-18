using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000063 RID: 99
	public class CourseRegistrationWrapper : WrapperBase<CourseRegistrationDTO>
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000EFE7 File Offset: 0x0000D1E7
		public CourseRegistrationWrapper()
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000EFF1 File Offset: 0x0000D1F1
		public CourseRegistrationWrapper(CourseRegistrationDTO course, bool profLetterIsReady, bool isCourseDateStillRelevant) : base(course)
		{
			this.ProfLetterReady = profLetterIsReady;
			this.IsCourseDateStillRelevant = isCourseDateStillRelevant;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000F00C File Offset: 0x0000D20C
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000F014 File Offset: 0x0000D214
		public bool ProfLetterReady { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000F01D File Offset: 0x0000D21D
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000F025 File Offset: 0x0000D225
		public bool IsCourseDateStillRelevant { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000F02E File Offset: 0x0000D22E
		public string CourseDescription
		{
			get
			{
				CourseRegistrationDTO item = base.Item;
				return (((item != null) ? item.Course : null) == null) ? "" : base.Item.Course.GetCourseDescription();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000F05C File Offset: 0x0000D25C
		public string DateStudentLastViewedDescription
		{
			get
			{
				CourseRegistrationDTO item = base.Item;
				DateTime? dateTime;
				return ((item != null) ? ((item.DateStudentLastViewed != null) ? dateTime.GetValueOrDefault().ToString("yyyy-MM-dd") : null) : null) ?? "-";
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public int LuCourseId
		{
			get
			{
				CourseRegistrationDTO item = base.Item;
				int? num;
				if (item == null)
				{
					num = null;
				}
				else
				{
					LookupCourseDTO course = item.Course;
					num = ((course != null) ? new int?(course.LuCourseId) : null);
				}
				return num ?? 0;
			}
		}
	}
}

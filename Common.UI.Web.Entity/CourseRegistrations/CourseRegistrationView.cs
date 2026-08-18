using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.Common.UI.Web.Entity.CourseRegistrations
{
	// Token: 0x0200003B RID: 59
	public class CourseRegistrationView : WrapperBase<CourseRegistrationDTO>
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000342C File Offset: 0x0000162C
		public CourseRegistrationView(CourseRegistrationDTO item) : base(item)
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003438 File Offset: 0x00001638
		public DateTime? DateAdded
		{
			get
			{
				return (base.Item != null) ? new DateTime?(base.Item.DateAdded) : null;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003470 File Offset: 0x00001670
		public int CoursesId
		{
			get
			{
				return (base.Item != null) ? base.Item.CoursesId : 0;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003498 File Offset: 0x00001698
		public string CourseDescription
		{
			get
			{
				return (base.Item != null && base.Item.Course != null) ? base.Item.Course.GetCourseDescription() : string.Empty;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000034D8 File Offset: 0x000016D8
		public string Status
		{
			get
			{
				return (base.Item != null) ? base.Item.RegistrationStatus.ToString() : string.Empty;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003514 File Offset: 0x00001714
		public DateTime? DateLetterIssued
		{
			get
			{
				return (base.Item != null) ? base.Item.DateLetterIssued : null;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003544 File Offset: 0x00001744
		public DateTime? DateInstructorViewedOnline
		{
			get
			{
				return (base.Item != null) ? base.Item.DateInstructorLastViewed : null;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003574 File Offset: 0x00001774
		public DateTime? DateStudentViewedOnline
		{
			get
			{
				return (base.Item != null) ? base.Item.DateStudentLastViewed : null;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000035A4 File Offset: 0x000017A4
		public DateTime? DateLetterReturned
		{
			get
			{
				return (base.Item != null) ? base.Item.DateLetterReturned : null;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000035D4 File Offset: 0x000017D4
		public LookupCourseView Course
		{
			get
			{
				return (base.Item != null) ? new LookupCourseView(base.Item.Course) : null;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003604 File Offset: 0x00001804
		public string TimetableInfo
		{
			get
			{
				return (base.Item != null && base.Item.Course != null && base.Item.Course.TimetableItems != null) ? base.Item.Course.TimetableItems.GetTimetableDescription() : string.Empty;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000365C File Offset: 0x0000185C
		public IList<int> ExemptedInstructorAssignments
		{
			get
			{
				return (base.Item != null) ? base.Item.ExemptedInstructorAssignments : null;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003684 File Offset: 0x00001884
		public int LuCourseId
		{
			get
			{
				return (base.Item != null && base.Item.Course != null) ? base.Item.Course.LuCourseId : 0;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000036C0 File Offset: 0x000018C0
		public CourseRequestBaseView CourseAccommodationRequestBase
		{
			get
			{
				return (base.Item != null) ? new CourseRequestBaseView(base.Item.CourseAccommodationRequestBase) : null;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000036F0 File Offset: 0x000018F0
		public override string ToString()
		{
			return this.CourseDescription;
		}
	}
}

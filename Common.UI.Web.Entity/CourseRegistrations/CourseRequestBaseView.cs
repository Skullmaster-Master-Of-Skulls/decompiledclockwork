using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.UI.Web.Entity.People;

namespace TechnoPro.Common.UI.Web.Entity.CourseRegistrations
{
	// Token: 0x0200003C RID: 60
	public class CourseRequestBaseView : WrapperBase<CourseRequestBaseDTO>
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00003708 File Offset: 0x00001908
		public CourseRequestBaseView(CourseRequestBaseDTO item) : base(item)
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003714 File Offset: 0x00001914
		public int CoursesId
		{
			get
			{
				return (base.Item != null) ? base.Item.CoursesId : 0;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000373C File Offset: 0x0000193C
		public int StudentCourseAccommodationRequestId
		{
			get
			{
				return (base.Item != null) ? base.Item.StudentCourseAccommodationRequestId : 0;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003764 File Offset: 0x00001964
		public eStudentCourseAccommodationRequestStatusDTO? Status
		{
			get
			{
				return (base.Item != null) ? new eStudentCourseAccommodationRequestStatusDTO?(base.Item.Status) : null;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000379C File Offset: 0x0000199C
		public DateTime? DateRequested
		{
			get
			{
				return (base.Item != null) ? base.Item.DateRequested : null;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000037CC File Offset: 0x000019CC
		public PersonBaseView WhoEntered
		{
			get
			{
				return (base.Item != null && base.Item.WhoEntered != null) ? new PersonBaseView(base.Item.WhoEntered) : null;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00003808 File Offset: 0x00001A08
		public DateTime? DateEntered
		{
			get
			{
				return (base.Item != null) ? new DateTime?(base.Item.DateEntered) : null;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003840 File Offset: 0x00001A40
		public string Note1
		{
			get
			{
				return (base.Item != null) ? (base.Item.Note1 ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003878 File Offset: 0x00001A78
		public string Note2
		{
			get
			{
				return (base.Item != null) ? (base.Item.Note2 ?? string.Empty) : string.Empty;
			}
		}
	}
}

using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.UI.Web.Entity.LookupCourses
{
	// Token: 0x02000031 RID: 49
	public class LookupCourseView : WrapperBase<LookupCourseDTO>
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00002FCC File Offset: 0x000011CC
		public LookupCourseView(LookupCourseDTO item) : base(item)
		{
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002FD8 File Offset: 0x000011D8
		public int LuCourseId
		{
			get
			{
				return (base.Item != null) ? this.LuCourseId : 0;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00002FFC File Offset: 0x000011FC
		public string CourseDescription
		{
			get
			{
				return (base.Item != null) ? base.Item.GetCourseDescription() : string.Empty;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003028 File Offset: 0x00001228
		public DateTime? StartDate
		{
			get
			{
				return (base.Item != null) ? new DateTime?(base.Item.StartDate) : null;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003060 File Offset: 0x00001260
		public DateTime? EndDate
		{
			get
			{
				return (base.Item != null) ? new DateTime?(base.Item.EndDate) : null;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003098 File Offset: 0x00001298
		public string PrimaryInstructorName
		{
			get
			{
				return (base.Item != null) ? base.Item.GetPrimaryInstructor().GetInstructorName() : string.Empty;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000030CC File Offset: 0x000012CC
		public string Term
		{
			get
			{
				return (base.Item != null) ? (base.Item.Term ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003104 File Offset: 0x00001304
		public string Course
		{
			get
			{
				return (base.Item != null) ? (base.Item.Course ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000313C File Offset: 0x0000133C
		public string Subject
		{
			get
			{
				return (base.Item != null && base.Item.Subject != null) ? (base.Item.Subject.SubjectDescription ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00003184 File Offset: 0x00001384
		public string Section
		{
			get
			{
				return (base.Item != null) ? (base.Item.Section ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000031BC File Offset: 0x000013BC
		public string TimeOfDay
		{
			get
			{
				return (base.Item != null) ? (base.Item.TimeOfDay ?? string.Empty) : string.Empty;
			}
		}
	}
}

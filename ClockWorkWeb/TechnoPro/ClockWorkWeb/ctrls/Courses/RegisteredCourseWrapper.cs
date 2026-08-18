using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.ctrls.Courses
{
	// Token: 0x0200014F RID: 335
	public class RegisteredCourseWrapper : WrapperBase<CourseRegistrationDTO>
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0000EFE7 File Offset: 0x0000D1E7
		public RegisteredCourseWrapper()
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0004763F File Offset: 0x0004583F
		public RegisteredCourseWrapper(CourseRegistrationDTO course) : base(course)
		{
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0004764C File Offset: 0x0004584C
		public int LuCourseId
		{
			get
			{
				return (base.Item == null || base.Item.Course == null) ? 0 : base.Item.Course.LuCourseId;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00047688 File Offset: 0x00045888
		public string DisplayString
		{
			get
			{
				return (base.Item == null || base.Item.Course == null) ? "" : base.Item.Course.GetCourseDescription();
			}
		}
	}
}

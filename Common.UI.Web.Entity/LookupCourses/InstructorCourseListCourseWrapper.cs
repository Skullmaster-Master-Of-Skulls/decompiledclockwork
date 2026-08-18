using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.UI.Web.Entity.LookupCourses
{
	// Token: 0x02000030 RID: 48
	public class InstructorCourseListCourseWrapper : WrapperBase<LookupCourseDTO>
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00002F28 File Offset: 0x00001128
		public InstructorCourseListCourseWrapper()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002F32 File Offset: 0x00001132
		public InstructorCourseListCourseWrapper(LookupCourseDTO course, bool canEditTestDef) : base(course)
		{
			this.CanEditTestDefinition = canEditTestDef;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002F45 File Offset: 0x00001145
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00002F4D File Offset: 0x0000114D
		public bool CanEditTestDefinition { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002F58 File Offset: 0x00001158
		public bool HasClassTestDefinition
		{
			get
			{
				return this.CanEditTestDefinition;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00002F70 File Offset: 0x00001170
		public string CourseDescription
		{
			get
			{
				return (base.Item == null) ? "" : base.Item.GetCourseDescription("<div style='padding-top:10px'><b>{0} {1}</b><br /><span style='font-size: .75em;'>SECTION: {2} {3} (TERM: {4}){5}</span></div>");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002FA4 File Offset: 0x000011A4
		public int LuCourseId
		{
			get
			{
				return (base.Item == null) ? 0 : base.Item.LuCourseId;
			}
		}
	}
}

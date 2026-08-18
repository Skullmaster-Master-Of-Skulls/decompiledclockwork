using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses
{
	// Token: 0x0200001C RID: 28
	public class StudentCourseLetterInfo
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00006DF7 File Offset: 0x00004FF7
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00006DFF File Offset: 0x00004FFF
		public LookupCourseBaseDTO CourseBase { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00006E08 File Offset: 0x00005008
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00006E10 File Offset: 0x00005010
		public BasicPersonDTO Student { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00006E19 File Offset: 0x00005019
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00006E21 File Offset: 0x00005021
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00006E2A File Offset: 0x0000502A
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00006E32 File Offset: 0x00005032
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006E3B File Offset: 0x0000503B
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00006E43 File Offset: 0x00005043
		public DateTime? DateApproved { get; set; }
	}
}

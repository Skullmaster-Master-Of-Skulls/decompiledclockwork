using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkWeb.Areas.Loa.Models
{
	// Token: 0x02000160 RID: 352
	public class StudentCourseForLogicEmailRulesViewModel
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00048CB2 File Offset: 0x00046EB2
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00048CBA File Offset: 0x00046EBA
		public IList<CourseRegistrationDTO> AllowedCourses { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00048CC3 File Offset: 0x00046EC3
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00048CCB File Offset: 0x00046ECB
		public PersonBaseDTO Student { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00048CD4 File Offset: 0x00046ED4
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00048CDC File Offset: 0x00046EDC
		public string PersonIdHash { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00048CE5 File Offset: 0x00046EE5
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00048CED File Offset: 0x00046EED
		public string PersonIdHashPlain { get; set; }
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Students
{
	// Token: 0x02000290 RID: 656
	public class DuplicateStudent
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00019A8A File Offset: 0x00017C8A
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00019A92 File Offset: 0x00017C92
		public PersonBase Student { get; set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00019A9B File Offset: 0x00017C9B
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00019AA3 File Offset: 0x00017CA3
		public IList<DynamicData> PerStudentDataItems { get; set; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00019AAC File Offset: 0x00017CAC
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public IList<BaseBasicAppointment> Appointments { get; set; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00019ABD File Offset: 0x00017CBD
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00019AC5 File Offset: 0x00017CC5
		public IList<CourseRegistration> Courses { get; set; }
	}
}

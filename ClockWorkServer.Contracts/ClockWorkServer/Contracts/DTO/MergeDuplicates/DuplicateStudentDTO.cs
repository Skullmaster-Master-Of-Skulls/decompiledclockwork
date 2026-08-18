using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x0200045F RID: 1119
	[DataContract(Namespace = "http://tpro.ca")]
	public class DuplicateStudentDTO
	{
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0000B06A File Offset: 0x0000926A
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x0000B072 File Offset: 0x00009272
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0000B07B File Offset: 0x0000927B
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x0000B083 File Offset: 0x00009283
		[DataMember]
		public IList<DynamicDataDTO> PerStudentDataItems { get; set; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0000B08C File Offset: 0x0000928C
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0000B094 File Offset: 0x00009294
		[DataMember]
		public IList<BaseBasicAppointmentDTO> Appointments { get; set; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0000B09D File Offset: 0x0000929D
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0000B0A5 File Offset: 0x000092A5
		[DataMember]
		public IList<CourseRegistrationDTO> Courses { get; set; }
	}
}

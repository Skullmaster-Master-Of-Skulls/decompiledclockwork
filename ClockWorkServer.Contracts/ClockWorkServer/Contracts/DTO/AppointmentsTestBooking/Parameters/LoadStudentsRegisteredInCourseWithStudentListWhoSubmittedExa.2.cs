using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A58 RID: 2648
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp
	{
		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x0001B095 File Offset: 0x00019295
		// (set) Token: 0x0600379C RID: 14236 RVA: 0x0001B09D File Offset: 0x0001929D
		[DataMember]
		public IList<PersonBaseDTO> StudentsRegisteredInCourse { get; set; }

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x0001B0A6 File Offset: 0x000192A6
		// (set) Token: 0x0600379E RID: 14238 RVA: 0x0001B0AE File Offset: 0x000192AE
		[DataMember]
		public IList<int> PersonIdsWhoHaveSubmittedExamRequest { get; set; }
	}
}

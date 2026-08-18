using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A7E RID: 2686
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedTestExamStudentsResp
	{
		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x0001B4E6 File Offset: 0x000196E6
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x0001B4EE File Offset: 0x000196EE
		[DataMember]
		public IList<UnbookedTestExamStudentDTO> UnbookedStudents { get; set; }
	}
}

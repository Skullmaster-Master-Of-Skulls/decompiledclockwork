using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009AE RID: 2478
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestForExamRequestDTO : ClassTestDTO
	{
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x0001876E File Offset: 0x0001696E
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x00018776 File Offset: 0x00016976
		[DataMember]
		public string ExamRequestInstructorChoices { get; set; }
	}
}

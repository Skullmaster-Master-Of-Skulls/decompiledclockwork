using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C7B RID: 3195
	[DataContract(Namespace = "http://tpro.ca")]
	public class AllowedToBookAppointmentForStudentResp
	{
		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x00020832 File Offset: 0x0001EA32
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x0002083A File Offset: 0x0001EA3A
		[DataMember]
		public bool IsAllowedToBookAppointments { get; set; }
	}
}

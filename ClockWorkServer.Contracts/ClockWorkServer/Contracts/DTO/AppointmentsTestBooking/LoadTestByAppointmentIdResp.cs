using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A05 RID: 2565
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestByAppointmentIdResp
	{
		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x00019DF2 File Offset: 0x00017FF2
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x00019DFA File Offset: 0x00017FFA
		[DataMember]
		public TestDTO Test { get; set; }
	}
}

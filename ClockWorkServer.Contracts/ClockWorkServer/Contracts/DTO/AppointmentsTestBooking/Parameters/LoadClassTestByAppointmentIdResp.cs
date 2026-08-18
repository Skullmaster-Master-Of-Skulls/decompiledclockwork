using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A5F RID: 2655
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestByAppointmentIdResp
	{
		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x0001B12E File Offset: 0x0001932E
		// (set) Token: 0x060037B5 RID: 14261 RVA: 0x0001B136 File Offset: 0x00019336
		[DataMember]
		public StudentClassTestDTO StudentTestInfo { get; set; }
	}
}

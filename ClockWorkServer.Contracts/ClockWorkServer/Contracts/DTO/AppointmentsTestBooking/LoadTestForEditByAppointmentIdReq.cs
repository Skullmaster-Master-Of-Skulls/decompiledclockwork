using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A33 RID: 2611
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestForEditByAppointmentIdReq : BaseMessageReq
	{
		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x0001A210 File Offset: 0x00018410
		// (set) Token: 0x060035E5 RID: 13797 RVA: 0x0001A218 File Offset: 0x00018418
		[DataMember]
		public int AppointmentId { get; set; }
	}
}

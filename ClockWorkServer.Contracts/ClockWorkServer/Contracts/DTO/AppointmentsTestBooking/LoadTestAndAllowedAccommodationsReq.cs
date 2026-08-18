using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A35 RID: 2613
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestAndAllowedAccommodationsReq : BaseMessageReq
	{
		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x0001A265 File Offset: 0x00018465
		// (set) Token: 0x060035F1 RID: 13809 RVA: 0x0001A26D File Offset: 0x0001846D
		[DataMember]
		public int AppointmentId { get; set; }
	}
}

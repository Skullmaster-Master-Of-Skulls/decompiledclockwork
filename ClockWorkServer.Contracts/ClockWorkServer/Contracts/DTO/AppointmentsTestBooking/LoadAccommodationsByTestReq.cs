using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A2D RID: 2605
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationsByTestReq : BaseMessageReq
	{
		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x0001A144 File Offset: 0x00018344
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x0001A14C File Offset: 0x0001834C
		[DataMember]
		public int AppointmentId { get; set; }
	}
}

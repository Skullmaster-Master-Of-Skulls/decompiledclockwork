using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A81 RID: 2689
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestExamRowReq : BaseMessageReq
	{
		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x0001B53B File Offset: 0x0001973B
		// (set) Token: 0x06003851 RID: 14417 RVA: 0x0001B543 File Offset: 0x00019743
		[DataMember]
		public int AppointmentId { get; set; }
	}
}

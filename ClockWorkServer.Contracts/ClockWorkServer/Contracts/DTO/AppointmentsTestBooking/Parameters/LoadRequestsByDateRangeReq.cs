using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A55 RID: 2645
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsByDateRangeReq : BaseMessageReq
	{
		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x0001B051 File Offset: 0x00019251
		// (set) Token: 0x06003791 RID: 14225 RVA: 0x0001B059 File Offset: 0x00019259
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x0001B062 File Offset: 0x00019262
		// (set) Token: 0x06003793 RID: 14227 RVA: 0x0001B06A File Offset: 0x0001926A
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}

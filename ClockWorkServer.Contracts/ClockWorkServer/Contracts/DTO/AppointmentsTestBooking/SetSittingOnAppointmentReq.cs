using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A3C RID: 2620
	[DataContract(Namespace = "http://tpro.ca")]
	public class SetSittingOnAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x0001A364 File Offset: 0x00018564
		// (set) Token: 0x06003616 RID: 13846 RVA: 0x0001A36C File Offset: 0x0001856C
		[DataMember]
		public IDictionary<int, int> AppointmentIdWithSittingIds { get; set; }
	}
}

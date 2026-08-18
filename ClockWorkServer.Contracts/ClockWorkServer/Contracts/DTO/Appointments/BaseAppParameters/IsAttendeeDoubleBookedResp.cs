using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200096A RID: 2410
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsAttendeeDoubleBookedResp
	{
		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x00017FD3 File Offset: 0x000161D3
		// (set) Token: 0x0600313A RID: 12602 RVA: 0x00017FDB File Offset: 0x000161DB
		[DataMember]
		public bool IsDoubleBooked { get; set; }
	}
}

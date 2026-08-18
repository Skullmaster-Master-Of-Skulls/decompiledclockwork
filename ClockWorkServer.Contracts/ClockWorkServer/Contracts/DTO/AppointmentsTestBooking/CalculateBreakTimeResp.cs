using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009CE RID: 2510
	[DataContract(Namespace = "http://tpro.ca")]
	public class CalculateBreakTimeResp
	{
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000194B8 File Offset: 0x000176B8
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x000194C0 File Offset: 0x000176C0
		[DataMember]
		public int NumExtraMinutes { get; set; }
	}
}

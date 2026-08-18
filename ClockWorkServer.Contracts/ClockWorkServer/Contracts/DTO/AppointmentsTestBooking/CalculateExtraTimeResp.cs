using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009CC RID: 2508
	[DataContract(Namespace = "http://tpro.ca")]
	public class CalculateExtraTimeResp
	{
		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x00019474 File Offset: 0x00017674
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x0001947C File Offset: 0x0001767C
		[DataMember]
		public int NumExtraMinutes { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009DF RID: 2527
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsStudentsAccommodationsExpiredResp
	{
		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x00019AF5 File Offset: 0x00017CF5
		// (set) Token: 0x060034BB RID: 13499 RVA: 0x00019AFD File Offset: 0x00017CFD
		[DataMember]
		public bool IsExpired { get; set; }
	}
}

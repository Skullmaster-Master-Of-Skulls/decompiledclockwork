using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009DE RID: 2526
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsStudentsAccommodationsExpiredReq : BaseMessageReq
	{
		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x00019AE4 File Offset: 0x00017CE4
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x00019AEC File Offset: 0x00017CEC
		[DataMember]
		public int PersonId { get; set; }
	}
}

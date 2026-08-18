using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009FF RID: 2559
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateScribeResp
	{
		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x00019D8C File Offset: 0x00017F8C
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x00019D94 File Offset: 0x00017F94
		[DataMember]
		public int PersonId { get; set; }
	}
}

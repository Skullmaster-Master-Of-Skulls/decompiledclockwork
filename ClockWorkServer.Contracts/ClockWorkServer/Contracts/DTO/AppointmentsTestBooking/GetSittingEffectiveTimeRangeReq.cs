using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009EE RID: 2542
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSittingEffectiveTimeRangeReq : BaseMessageReq
	{
		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x00019C6B File Offset: 0x00017E6B
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x00019C73 File Offset: 0x00017E73
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}

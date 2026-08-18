using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000947 RID: 2375
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCancelReasonReq : BaseMessageReq
	{
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x00017C1B File Offset: 0x00015E1B
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x00017C23 File Offset: 0x00015E23
		[DataMember]
		public AppCancelReasonDTO AppCancelReason { get; set; }
	}
}

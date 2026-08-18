using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000946 RID: 2374
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCancelReasonReq : BaseMessageReq
	{
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x00017C0A File Offset: 0x00015E0A
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x00017C12 File Offset: 0x00015E12
		[DataMember]
		public AppCancelReasonDTO CancelReason { get; set; }
	}
}

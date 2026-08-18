using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000945 RID: 2373
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCancelReasonReq : BaseMessageReq
	{
		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x00017BF9 File Offset: 0x00015DF9
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x00017C01 File Offset: 0x00015E01
		[DataMember]
		public int CancelReasonId { get; set; }
	}
}

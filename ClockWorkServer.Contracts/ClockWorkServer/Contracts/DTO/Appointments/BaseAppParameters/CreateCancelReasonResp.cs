using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000948 RID: 2376
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCancelReasonResp
	{
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x00017C2C File Offset: 0x00015E2C
		// (set) Token: 0x060030AA RID: 12458 RVA: 0x00017C34 File Offset: 0x00015E34
		[DataMember]
		public int CancelReasonId { get; set; }
	}
}

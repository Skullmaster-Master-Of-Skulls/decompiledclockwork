using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000943 RID: 2371
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCancelReasonByIdReq : BaseMessageReq
	{
		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x00017BD7 File Offset: 0x00015DD7
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x00017BDF File Offset: 0x00015DDF
		[DataMember]
		public int CancelReasonId { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000944 RID: 2372
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCancelReasonByIdResp
	{
		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x00017BE8 File Offset: 0x00015DE8
		// (set) Token: 0x0600309E RID: 12446 RVA: 0x00017BF0 File Offset: 0x00015DF0
		[DataMember]
		public AppCancelReasonDTO AppCancelReason { get; set; }
	}
}

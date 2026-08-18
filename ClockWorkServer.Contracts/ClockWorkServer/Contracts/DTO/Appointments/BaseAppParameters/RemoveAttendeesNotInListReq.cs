using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000964 RID: 2404
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveAttendeesNotInListReq : BaseMessageReq
	{
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x00017EC3 File Offset: 0x000160C3
		// (set) Token: 0x06003114 RID: 12564 RVA: 0x00017ECB File Offset: 0x000160CB
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x00017ED4 File Offset: 0x000160D4
		// (set) Token: 0x06003116 RID: 12566 RVA: 0x00017EDC File Offset: 0x000160DC
		[DataMember]
		public IList<int> PersonIds { get; set; }
	}
}

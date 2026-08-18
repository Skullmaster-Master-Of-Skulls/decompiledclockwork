using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000966 RID: 2406
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateNoShowValueByAttendeeIdReq : BaseMessageReq
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x00017F18 File Offset: 0x00016118
		// (set) Token: 0x06003120 RID: 12576 RVA: 0x00017F20 File Offset: 0x00016120
		[DataMember]
		public int AttendeeId { get; set; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x00017F29 File Offset: 0x00016129
		// (set) Token: 0x06003122 RID: 12578 RVA: 0x00017F31 File Offset: 0x00016131
		[DataMember]
		public bool NoShowValue { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200096D RID: 2413
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToRemoveAttendeesReq : BaseMessageReq
	{
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x00018039 File Offset: 0x00016239
		// (set) Token: 0x06003149 RID: 12617 RVA: 0x00018041 File Offset: 0x00016241
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x0001804A File Offset: 0x0001624A
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x00018052 File Offset: 0x00016252
		[DataMember]
		public IList<int> PersonIdList { get; set; }

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x0001805B File Offset: 0x0001625B
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x00018063 File Offset: 0x00016263
		[DataMember]
		public IList<int> AttendeeIdList { get; set; }
	}
}

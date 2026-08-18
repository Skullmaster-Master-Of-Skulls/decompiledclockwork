using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200096B RID: 2411
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetDoubleBookedAttendeesReq : BaseMessageReq
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x00017FE4 File Offset: 0x000161E4
		// (set) Token: 0x0600313D RID: 12605 RVA: 0x00017FEC File Offset: 0x000161EC
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x00017FF5 File Offset: 0x000161F5
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x00017FFD File Offset: 0x000161FD
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x00018006 File Offset: 0x00016206
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x0001800E File Offset: 0x0001620E
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x00018017 File Offset: 0x00016217
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x0001801F File Offset: 0x0001621F
		[DataMember]
		public int AppointmentIdToSkip { get; set; }
	}
}

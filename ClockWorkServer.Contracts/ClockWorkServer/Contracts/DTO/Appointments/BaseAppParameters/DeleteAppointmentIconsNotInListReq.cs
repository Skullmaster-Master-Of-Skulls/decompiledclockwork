using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000951 RID: 2385
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAppointmentIconsNotInListReq : BaseMessageReq
	{
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x00017CD6 File Offset: 0x00015ED6
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x00017CDE File Offset: 0x00015EDE
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x00017CE7 File Offset: 0x00015EE7
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x00017CEF File Offset: 0x00015EEF
		[DataMember]
		public IList<int> IconNums { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF1 RID: 2801
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithReq : BaseMessageReq
	{
		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06003B33 RID: 15155 RVA: 0x0001CCFF File Offset: 0x0001AEFF
		// (set) Token: 0x06003B34 RID: 15156 RVA: 0x0001CD07 File Offset: 0x0001AF07
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x0001CD10 File Offset: 0x0001AF10
		// (set) Token: 0x06003B36 RID: 15158 RVA: 0x0001CD18 File Offset: 0x0001AF18
		[DataMember]
		public IList<int> StaffGroupIds { get; set; }
	}
}

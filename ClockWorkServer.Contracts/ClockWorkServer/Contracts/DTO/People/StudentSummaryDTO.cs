using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003C0 RID: 960
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentSummaryDTO
	{
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0000A04F File Offset: 0x0000824F
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x0000A057 File Offset: 0x00008257
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0000A060 File Offset: 0x00008260
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x0000A068 File Offset: 0x00008268
		[DataMember]
		public IList<BaseExtendedAppointmentDTO> Appointments { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0000A071 File Offset: 0x00008271
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x0000A079 File Offset: 0x00008279
		[DataMember]
		public StudentCommonInfoDTO StudentCommonInfo { get; set; }
	}
}

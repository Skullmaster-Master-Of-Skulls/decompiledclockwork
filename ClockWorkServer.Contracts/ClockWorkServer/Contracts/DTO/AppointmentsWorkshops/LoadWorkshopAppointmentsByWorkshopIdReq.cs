using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F9 RID: 2297
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopAppointmentsByWorkshopIdReq : BaseMessageReq
	{
		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x0001646F File Offset: 0x0001466F
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x00016477 File Offset: 0x00014677
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x00016480 File Offset: 0x00014680
		// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x00016488 File Offset: 0x00014688
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x00016491 File Offset: 0x00014691
		// (set) Token: 0x06002ED8 RID: 11992 RVA: 0x00016499 File Offset: 0x00014699
		[DataMember]
		public int WorkshopId { get; set; }
	}
}

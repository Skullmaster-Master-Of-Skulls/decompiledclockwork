using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AED RID: 2797
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSingleDayAvailabilityStatusesByUserReq : BaseMessageReq
	{
		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x0001CC99 File Offset: 0x0001AE99
		// (set) Token: 0x06003B24 RID: 15140 RVA: 0x0001CCA1 File Offset: 0x0001AEA1
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x0001CCAA File Offset: 0x0001AEAA
		// (set) Token: 0x06003B26 RID: 15142 RVA: 0x0001CCB2 File Offset: 0x0001AEB2
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x0001CCBB File Offset: 0x0001AEBB
		// (set) Token: 0x06003B28 RID: 15144 RVA: 0x0001CCC3 File Offset: 0x0001AEC3
		[DataMember]
		public int NumDays { get; set; }
	}
}

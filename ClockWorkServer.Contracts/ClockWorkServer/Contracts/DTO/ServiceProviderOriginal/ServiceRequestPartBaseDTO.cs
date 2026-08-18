using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002E2 RID: 738
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceRequestPartBaseDTO
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00007E8B File Offset: 0x0000608B
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00007E93 File Offset: 0x00006093
		[DataMember]
		public int ServiceProviderRequestId { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00007E9C File Offset: 0x0000609C
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x00007EA4 File Offset: 0x000060A4
		[DataMember]
		public string PartsDescription { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E5 RID: 741
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAssignmentsByProviderAndAssignedDateReq : BaseMessageReq
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00007F02 File Offset: 0x00006102
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x00007F0A File Offset: 0x0000610A
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00007F13 File Offset: 0x00006113
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x00007F1B File Offset: 0x0000611B
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00007F24 File Offset: 0x00006124
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x00007F2C File Offset: 0x0000612C
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}

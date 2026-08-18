using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000907 RID: 2311
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateWorkshopDefinitionReq : BaseMessageReq
	{
		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x0001653B File Offset: 0x0001473B
		// (set) Token: 0x06002EFA RID: 12026 RVA: 0x00016543 File Offset: 0x00014743
		[DataMember]
		public WorkshopDefinitionDTO WorkshopDef { get; set; }
	}
}

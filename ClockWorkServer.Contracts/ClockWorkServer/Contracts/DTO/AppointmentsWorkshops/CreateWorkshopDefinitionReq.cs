using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000905 RID: 2309
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateWorkshopDefinitionReq : BaseMessageReq
	{
		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x00016519 File Offset: 0x00014719
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x00016521 File Offset: 0x00014721
		[DataMember]
		public WorkshopDefinitionDTO WorkshopDef { get; set; }
	}
}

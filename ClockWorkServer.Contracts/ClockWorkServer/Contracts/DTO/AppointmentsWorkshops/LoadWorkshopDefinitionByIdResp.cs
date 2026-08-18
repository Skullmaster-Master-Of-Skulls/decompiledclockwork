using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000910 RID: 2320
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopDefinitionByIdResp
	{
		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000165B2 File Offset: 0x000147B2
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000165BA File Offset: 0x000147BA
		[DataMember]
		public WorkshopDefinitionDTO WorkshopDefinition { get; set; }
	}
}

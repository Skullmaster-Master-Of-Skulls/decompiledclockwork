using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x0200090F RID: 2319
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopDefinitionByIdReq : BaseMessageReq
	{
		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000165A1 File Offset: 0x000147A1
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000165A9 File Offset: 0x000147A9
		[DataMember]
		public int WorkshopId { get; set; }
	}
}

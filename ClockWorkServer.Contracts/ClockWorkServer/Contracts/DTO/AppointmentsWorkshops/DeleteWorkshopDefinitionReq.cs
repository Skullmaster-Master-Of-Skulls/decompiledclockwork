using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000901 RID: 2305
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteWorkshopDefinitionReq : BaseMessageReq
	{
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x000164F7 File Offset: 0x000146F7
		// (set) Token: 0x06002EEC RID: 12012 RVA: 0x000164FF File Offset: 0x000146FF
		[DataMember]
		public int WorkshopId { get; set; }
	}
}

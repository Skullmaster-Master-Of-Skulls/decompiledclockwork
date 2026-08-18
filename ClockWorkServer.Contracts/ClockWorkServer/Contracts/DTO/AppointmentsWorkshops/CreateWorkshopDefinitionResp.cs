using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000906 RID: 2310
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateWorkshopDefinitionResp
	{
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x0001652A File Offset: 0x0001472A
		// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x00016532 File Offset: 0x00014732
		[DataMember]
		public int WorkshopId { get; set; }
	}
}

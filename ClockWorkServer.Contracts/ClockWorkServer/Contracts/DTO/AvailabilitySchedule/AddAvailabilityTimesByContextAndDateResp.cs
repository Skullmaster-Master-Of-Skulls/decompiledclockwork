using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C8 RID: 2248
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAvailabilityTimesByContextAndDateResp
	{
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x0001583F File Offset: 0x00013A3F
		// (set) Token: 0x06002D81 RID: 11649 RVA: 0x00015847 File Offset: 0x00013A47
		[DataMember]
		public AddAvailabilitiesActionResultDTO Result { get; set; }
	}
}

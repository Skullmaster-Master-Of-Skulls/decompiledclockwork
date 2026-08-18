using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008D4 RID: 2260
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAvailabilityGroupsResp
	{
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x00015982 File Offset: 0x00013B82
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x0001598A File Offset: 0x00013B8A
		[DataMember]
		public IList<AvailabilityGroupDTO> AvailabilityGroups { get; set; }
	}
}

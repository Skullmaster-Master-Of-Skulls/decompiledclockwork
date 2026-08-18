using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E7 RID: 1511
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetIntakeStatusesResp
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
		// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x0000DFBA File Offset: 0x0000C1BA
		[DataMember]
		public IDictionary<string, ePreIntakeStatus> IntakeStatuses { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E1 RID: 1505
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupStatusesResp
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0000DF3B File Offset: 0x0000C13B
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0000DF43 File Offset: 0x0000C143
		[DataMember]
		public IList<IntakeStatusDTO> IntakeStatuses { get; set; }
	}
}

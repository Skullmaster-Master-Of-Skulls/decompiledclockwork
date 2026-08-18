using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005D7 RID: 1495
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPendingIntakeEntryQueueItemsResp
	{
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0000DEA2 File Offset: 0x0000C0A2
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0000DEAA File Offset: 0x0000C0AA
		[DataMember]
		public IList<IntakeEntryQueueItemDTO> IntakeEntries { get; set; }
	}
}

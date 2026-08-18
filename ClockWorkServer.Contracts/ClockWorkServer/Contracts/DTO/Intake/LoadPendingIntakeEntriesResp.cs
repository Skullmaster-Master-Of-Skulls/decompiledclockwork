using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005D3 RID: 1491
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPendingIntakeEntriesResp
	{
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0000DE6F File Offset: 0x0000C06F
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0000DE77 File Offset: 0x0000C077
		[DataMember]
		public IList<IntakeEntryDTO> IntakeEntries { get; set; }
	}
}

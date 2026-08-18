using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000657 RID: 1623
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetExistingPerDateEntryResp
	{
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x0000F00A File Offset: 0x0000D20A
		// (set) Token: 0x06002107 RID: 8455 RVA: 0x0000F012 File Offset: 0x0000D212
		[DataMember]
		public PerDateEntryDTO PerDateEntry { get; set; }
	}
}

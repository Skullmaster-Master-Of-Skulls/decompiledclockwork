using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000663 RID: 1635
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPerDateEntriesResp
	{
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0000F191 File Offset: 0x0000D391
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x0000F199 File Offset: 0x0000D399
		[DataMember]
		public IList<PerDateEntryDTO> PerDateEntries { get; set; }
	}
}

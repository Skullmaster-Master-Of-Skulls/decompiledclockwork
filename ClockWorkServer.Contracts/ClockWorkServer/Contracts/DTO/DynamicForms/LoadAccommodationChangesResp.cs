using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000624 RID: 1572
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationChangesResp
	{
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0000E82A File Offset: 0x0000CA2A
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x0000E832 File Offset: 0x0000CA32
		[DataMember]
		public List<DynamicDataChangeDTO> Changes { get; set; }
	}
}

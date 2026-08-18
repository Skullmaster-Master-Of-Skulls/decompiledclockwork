using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000677 RID: 1655
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadListItemsResp
	{
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		[DataMember]
		public List<DynamicListItemDTO> Items { get; set; }
	}
}

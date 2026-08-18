using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200067B RID: 1659
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsByFormResp
	{
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0000F712 File Offset: 0x0000D912
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x0000F71A File Offset: 0x0000D91A
		[DataMember]
		public List<DynamicFieldDTO> Fields { get; set; }
	}
}

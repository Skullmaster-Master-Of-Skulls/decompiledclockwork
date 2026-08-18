using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200064D RID: 1613
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataResp
	{
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x0000EF0B File Offset: 0x0000D10B
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x0000EF13 File Offset: 0x0000D113
		[DataMember]
		public List<DynamicDataDTO> Data { get; set; }
	}
}

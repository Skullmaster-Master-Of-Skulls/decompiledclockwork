using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200064B RID: 1611
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataByFormResp
	{
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		[DataMember]
		public List<DynamicDataDTO> Data { get; set; }
	}
}

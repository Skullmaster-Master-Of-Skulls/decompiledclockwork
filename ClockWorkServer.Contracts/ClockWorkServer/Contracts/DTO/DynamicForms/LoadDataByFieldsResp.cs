using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000647 RID: 1607
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataByFieldsResp
	{
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x0000EE0C File Offset: 0x0000D00C
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x0000EE14 File Offset: 0x0000D014
		[DataMember]
		public List<DynamicDataDTO> Data { get; set; }
	}
}

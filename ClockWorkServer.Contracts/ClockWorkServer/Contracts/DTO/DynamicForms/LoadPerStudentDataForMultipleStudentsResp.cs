using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200064F RID: 1615
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPerStudentDataForMultipleStudentsResp
	{
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0000EF3E File Offset: 0x0000D13E
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x0000EF46 File Offset: 0x0000D146
		[DataMember]
		public List<DynamicDataSetDTO> Data { get; set; }
	}
}

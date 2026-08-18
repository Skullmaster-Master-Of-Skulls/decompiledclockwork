using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200064E RID: 1614
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPerStudentDataForMultipleStudentsReq : BaseMessageReq
	{
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0000EF1C File Offset: 0x0000D11C
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0000EF24 File Offset: 0x0000D124
		[DataMember]
		public List<int> PersonIds { get; set; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x0000EF2D File Offset: 0x0000D12D
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x0000EF35 File Offset: 0x0000D135
		[DataMember]
		public List<int> ControlIds { get; set; }
	}
}

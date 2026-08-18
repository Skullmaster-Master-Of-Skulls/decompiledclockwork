using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000664 RID: 1636
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPerDateEntriesReq : BaseMessageReq
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0000F1A2 File Offset: 0x0000D3A2
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x0000F1AA File Offset: 0x0000D3AA
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x0000F1B3 File Offset: 0x0000D3B3
		// (set) Token: 0x06002146 RID: 8518 RVA: 0x0000F1BB File Offset: 0x0000D3BB
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}

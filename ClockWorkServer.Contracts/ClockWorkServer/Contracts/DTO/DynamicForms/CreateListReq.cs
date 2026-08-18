using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200068C RID: 1676
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateListReq : BaseMessageReq
	{
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0000F822 File Offset: 0x0000DA22
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x0000F82A File Offset: 0x0000DA2A
		[DataMember]
		public DynamicListGroupDTO Group { get; set; }

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0000F833 File Offset: 0x0000DA33
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x0000F83B File Offset: 0x0000DA3B
		[DataMember]
		public IList<DynamicListItemDTO> ListItems { get; set; }
	}
}

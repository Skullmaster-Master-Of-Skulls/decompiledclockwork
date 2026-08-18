using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000676 RID: 1654
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadListItemsReq : BaseMessageReq
	{
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0000F69B File Offset: 0x0000D89B
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0000F6A3 File Offset: 0x0000D8A3
		[DataMember]
		public int LookupGroupId { get; set; }
	}
}

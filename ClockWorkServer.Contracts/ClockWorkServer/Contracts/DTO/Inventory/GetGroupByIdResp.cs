using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200052C RID: 1324
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetGroupByIdResp
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x0000CB72 File Offset: 0x0000AD72
		[DataMember]
		public InventoryGroupDTO Group { get; set; }
	}
}

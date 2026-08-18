using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000504 RID: 1284
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCatalogByNameReq : BaseMessageReq
	{
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		[DataMember]
		public string CatalogName { get; set; }
	}
}

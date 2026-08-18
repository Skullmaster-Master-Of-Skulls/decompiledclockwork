using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000516 RID: 1302
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTemplateCatalogByNameReq : BaseMessageReq
	{
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0000C9E3 File Offset: 0x0000ABE3
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0000C9EB File Offset: 0x0000ABEB
		[DataMember]
		public string TemplateName { get; set; }
	}
}

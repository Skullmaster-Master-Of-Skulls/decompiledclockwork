using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000514 RID: 1300
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportFromTemplateReq : BaseMessageReq
	{
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0000C99F File Offset: 0x0000AB9F
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x0000C9A7 File Offset: 0x0000ABA7
		[DataMember]
		public string TemplateName { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		[DataMember]
		public string CatalogName { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0000C9C1 File Offset: 0x0000ABC1
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x0000C9C9 File Offset: 0x0000ABC9
		[DataMember]
		public string CatalogDescription { get; set; }
	}
}

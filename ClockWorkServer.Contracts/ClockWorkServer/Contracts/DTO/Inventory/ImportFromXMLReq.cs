using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000512 RID: 1298
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportFromXMLReq : BaseMessageReq
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0000C95B File Offset: 0x0000AB5B
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x0000C963 File Offset: 0x0000AB63
		[DataMember]
		public string CatalogXml { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0000C96C File Offset: 0x0000AB6C
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0000C974 File Offset: 0x0000AB74
		[DataMember]
		public string CatalogName { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0000C97D File Offset: 0x0000AB7D
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x0000C985 File Offset: 0x0000AB85
		[DataMember]
		public string CatalogDescription { get; set; }
	}
}

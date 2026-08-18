using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000513 RID: 1299
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportFromXMLResp
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0000C98E File Offset: 0x0000AB8E
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0000C996 File Offset: 0x0000AB96
		[DataMember]
		public int CatalogId { get; set; }
	}
}

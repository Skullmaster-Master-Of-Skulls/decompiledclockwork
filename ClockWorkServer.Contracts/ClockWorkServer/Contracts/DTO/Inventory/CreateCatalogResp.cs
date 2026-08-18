using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200050B RID: 1291
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCatalogResp
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0000C8F5 File Offset: 0x0000AAF5
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		[DataMember]
		public int CatalogId { get; set; }
	}
}

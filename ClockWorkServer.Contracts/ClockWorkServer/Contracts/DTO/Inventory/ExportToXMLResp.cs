using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000511 RID: 1297
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportToXMLResp
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0000C94A File Offset: 0x0000AB4A
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0000C952 File Offset: 0x0000AB52
		[DataMember]
		public string CatalogXml { get; set; }
	}
}

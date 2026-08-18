using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000510 RID: 1296
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportToXMLReq : BaseMessageReq
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0000C939 File Offset: 0x0000AB39
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x0000C941 File Offset: 0x0000AB41
		[DataMember]
		public int CatalogId { get; set; }
	}
}

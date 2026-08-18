using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000521 RID: 1313
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCategoryByNameReq : BaseMessageReq
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		[DataMember]
		public string CategoryName { get; set; }
	}
}

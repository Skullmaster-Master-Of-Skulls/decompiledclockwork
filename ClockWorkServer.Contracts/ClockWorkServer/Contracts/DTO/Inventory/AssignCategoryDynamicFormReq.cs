using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200051D RID: 1309
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignCategoryDynamicFormReq : BaseMessageReq
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x0000CA73 File Offset: 0x0000AC73
		[DataMember]
		public string CategoryName { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x0000CA84 File Offset: 0x0000AC84
		[DataMember]
		public int DynamicFormId { get; set; }
	}
}

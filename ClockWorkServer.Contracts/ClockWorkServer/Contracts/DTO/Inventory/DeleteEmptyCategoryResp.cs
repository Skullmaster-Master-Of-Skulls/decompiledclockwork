using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000520 RID: 1312
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteEmptyCategoryResp
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0000CAAF File Offset: 0x0000ACAF
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x0000CAB7 File Offset: 0x0000ACB7
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000593 RID: 1427
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProductByBarCodeResp
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0000D866 File Offset: 0x0000BA66
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x0000D86E File Offset: 0x0000BA6E
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}

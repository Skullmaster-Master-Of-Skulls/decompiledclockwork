using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A9 RID: 1449
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProductStatusResp
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0000DAB7 File Offset: 0x0000BCB7
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x0000DABF File Offset: 0x0000BCBF
		[DataMember]
		public int ProductStatusId { get; set; }
	}
}

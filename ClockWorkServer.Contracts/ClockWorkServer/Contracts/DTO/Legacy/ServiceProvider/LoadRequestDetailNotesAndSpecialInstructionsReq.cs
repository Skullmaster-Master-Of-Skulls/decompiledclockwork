using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004CB RID: 1227
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestDetailNotesAndSpecialInstructionsReq : BaseMessageReq
	{
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0000BFED File Offset: 0x0000A1ED
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x0000BFF5 File Offset: 0x0000A1F5
		[DataMember]
		public int RequestId { get; set; }
	}
}

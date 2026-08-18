using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F1 RID: 753
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllSeatsResp
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00008183 File Offset: 0x00006383
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x0000818B File Offset: 0x0000638B
		[DataMember]
		public SeatCollectionDTO SeatCollection { get; set; }
	}
}

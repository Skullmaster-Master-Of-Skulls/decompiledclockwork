using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F2 RID: 754
	[DataContract(Namespace = "http://tpro.ca")]
	public class SeatAssetAccommodationDTO
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00008194 File Offset: 0x00006394
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x0000819C File Offset: 0x0000639C
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x000081A5 File Offset: 0x000063A5
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x000081AD File Offset: 0x000063AD
		[DataMember]
		public string Title { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F4 RID: 756
	[DataContract(Namespace = "http://tpro.ca")]
	public class SeatCollectionDTO
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0000820B File Offset: 0x0000640B
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x00008213 File Offset: 0x00006413
		public IList<SeatGroupDTO> AllSeatGroups { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0000821C File Offset: 0x0000641C
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x00008224 File Offset: 0x00006424
		public IList<SeatAssetDTO> AllAssets { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0000822D File Offset: 0x0000642D
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00008235 File Offset: 0x00006435
		public IList<SeatDTO> Seats { get; set; }
	}
}

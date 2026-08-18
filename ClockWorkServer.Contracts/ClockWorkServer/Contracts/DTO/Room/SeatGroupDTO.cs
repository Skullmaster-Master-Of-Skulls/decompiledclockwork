using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F6 RID: 758
	[DataContract(Namespace = "http://tpro.ca")]
	public class SeatGroupDTO
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0000829C File Offset: 0x0000649C
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x000082A4 File Offset: 0x000064A4
		[DataMember]
		public int SeatGroupId { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000082AD File Offset: 0x000064AD
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x000082B5 File Offset: 0x000064B5
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000082BE File Offset: 0x000064BE
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000082C6 File Offset: 0x000064C6
		[DataMember]
		public int PrimaryRoomPersonId { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000082CF File Offset: 0x000064CF
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x000082D7 File Offset: 0x000064D7
		[DataMember]
		public int ParentSeatGroupId { get; set; }
	}
}

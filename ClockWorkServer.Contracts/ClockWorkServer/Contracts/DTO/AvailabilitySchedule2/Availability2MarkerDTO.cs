using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2
{
	// Token: 0x020008D8 RID: 2264
	[DataContract(Namespace = "http://tpro.ca")]
	public class Availability2MarkerDTO
	{
		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x00015B0C File Offset: 0x00013D0C
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x00015B14 File Offset: 0x00013D14
		[DataMember]
		public int Availability2MarkerId { get; set; }

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x00015B1D File Offset: 0x00013D1D
		// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x00015B25 File Offset: 0x00013D25
		[DataMember]
		public string MarkerText { get; set; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x00015B2E File Offset: 0x00013D2E
		// (set) Token: 0x06002DD8 RID: 11736 RVA: 0x00015B36 File Offset: 0x00013D36
		[DataMember]
		public int? MarkerColourArgB { get; set; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x00015B3F File Offset: 0x00013D3F
		// (set) Token: 0x06002DDA RID: 11738 RVA: 0x00015B47 File Offset: 0x00013D47
		[DataMember]
		public int OrderNum { get; set; }
	}
}

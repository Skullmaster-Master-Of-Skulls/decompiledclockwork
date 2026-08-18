using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008B8 RID: 2232
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAvailabilityActionResultDTO
	{
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x000155CA File Offset: 0x000137CA
		// (set) Token: 0x06002D27 RID: 11559 RVA: 0x000155D2 File Offset: 0x000137D2
		[DataMember]
		public AvailabilityScheduleItemActionResultDTO Status { get; set; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000155DB File Offset: 0x000137DB
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x000155E3 File Offset: 0x000137E3
		[DataMember]
		public DateTime Date { get; set; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000155EC File Offset: 0x000137EC
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x000155F4 File Offset: 0x000137F4
		[DataMember]
		public AvailabilityScheduleTimeDTO Time { get; set; }
	}
}

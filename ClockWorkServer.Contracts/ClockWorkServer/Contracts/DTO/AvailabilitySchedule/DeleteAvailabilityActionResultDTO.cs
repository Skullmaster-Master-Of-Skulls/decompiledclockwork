using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008D6 RID: 2262
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailabilityActionResultDTO
	{
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000159B5 File Offset: 0x00013BB5
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x000159BD File Offset: 0x00013BBD
		[DataMember]
		public AvailabilityScheduleItemActionResultDTO Status { get; set; }

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000159C6 File Offset: 0x00013BC6
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000159CE File Offset: 0x00013BCE
		[DataMember]
		public DateTime Date { get; set; }

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000159D7 File Offset: 0x00013BD7
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x000159DF File Offset: 0x00013BDF
		[DataMember]
		public AvailabilityScheduleTimeDTO Time { get; set; }
	}
}

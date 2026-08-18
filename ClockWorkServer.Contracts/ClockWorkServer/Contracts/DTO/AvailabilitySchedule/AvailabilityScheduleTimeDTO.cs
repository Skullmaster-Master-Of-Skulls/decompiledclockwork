using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008D5 RID: 2261
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityScheduleTimeDTO
	{
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x00015993 File Offset: 0x00013B93
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x0001599B File Offset: 0x00013B9B
		[DataMember]
		public TimeSpan StartTime { get; set; }

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000159A4 File Offset: 0x00013BA4
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x000159AC File Offset: 0x00013BAC
		[DataMember]
		public TimeSpan EndTime { get; set; }
	}
}

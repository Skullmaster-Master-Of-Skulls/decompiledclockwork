using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200092E RID: 2350
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentWorkshopInfoDTO
	{
		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x00016E15 File Offset: 0x00015015
		// (set) Token: 0x06002FC9 RID: 12233 RVA: 0x00016E1D File Offset: 0x0001501D
		[DataMember]
		public int WorkshopId { get; set; }

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x00016E26 File Offset: 0x00015026
		// (set) Token: 0x06002FCB RID: 12235 RVA: 0x00016E2E File Offset: 0x0001502E
		[DataMember]
		public string WorkshopTitle { get; set; }

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x00016E37 File Offset: 0x00015037
		// (set) Token: 0x06002FCD RID: 12237 RVA: 0x00016E3F File Offset: 0x0001503F
		[DataMember]
		public int MaxAttendeeCount { get; set; }
	}
}

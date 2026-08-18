using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x0200003B RID: 59
	public class AddAvailabilityResult
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B060 File Offset: 0x00009260
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000B068 File Offset: 0x00009268
		public string PublicMessage { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B071 File Offset: 0x00009271
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000B079 File Offset: 0x00009279
		public AddAvailabilitiesActionResultDTO Result { get; set; }
	}
}

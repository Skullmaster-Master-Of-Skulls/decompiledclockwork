using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000938 RID: 2360
	[DataContract(Namespace = "http://tpro.ca")]
	public class FreeTimeSearchRecurringRuleDTO
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x00017A36 File Offset: 0x00015C36
		// (set) Token: 0x06003069 RID: 12393 RVA: 0x00017A3E File Offset: 0x00015C3E
		[DataMember]
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x00017A47 File Offset: 0x00015C47
		// (set) Token: 0x0600306B RID: 12395 RVA: 0x00017A4F File Offset: 0x00015C4F
		[DataMember]
		public TimeSpan StartTime { get; set; }

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x00017A58 File Offset: 0x00015C58
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x00017A60 File Offset: 0x00015C60
		[DataMember]
		public TimeSpan EndTime { get; set; }
	}
}

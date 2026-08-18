using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008BB RID: 2235
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityScheduleDateAndTimeDTO
	{
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x00015674 File Offset: 0x00013874
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x0001567C File Offset: 0x0001387C
		[DataMember]
		public DateTime Date { get; set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x00015685 File Offset: 0x00013885
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x0001568D File Offset: 0x0001388D
		[DataMember]
		public AvailabilityScheduleTimeDTO Time { get; set; }
	}
}

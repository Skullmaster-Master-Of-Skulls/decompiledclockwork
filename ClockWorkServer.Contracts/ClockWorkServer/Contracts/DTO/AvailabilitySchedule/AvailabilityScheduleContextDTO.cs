using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008BA RID: 2234
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityScheduleContextDTO
	{
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x00015652 File Offset: 0x00013852
		// (set) Token: 0x06002D39 RID: 11577 RVA: 0x0001565A File Offset: 0x0001385A
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x00015663 File Offset: 0x00013863
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x0001566B File Offset: 0x0001386B
		[DataMember]
		public int AvailabilityGroupId { get; set; }
	}
}

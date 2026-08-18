using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200087C RID: 2172
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobMonthlyScheduleDTO : ClockWorkServerJobScheduleDTO
	{
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x00014D11 File Offset: 0x00012F11
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x00014D19 File Offset: 0x00012F19
		[DataMember]
		public IList<int> DaysOfMonth { get; set; }

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x00014D22 File Offset: 0x00012F22
		// (set) Token: 0x06002BFD RID: 11261 RVA: 0x00014D2A File Offset: 0x00012F2A
		[DataMember]
		public IList<int> MonthsOfYear { get; set; }
	}
}

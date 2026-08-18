using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200087D RID: 2173
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobWeeklyScheduleDTO : ClockWorkServerJobScheduleDTO
	{
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x00014D3C File Offset: 0x00012F3C
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x00014D44 File Offset: 0x00012F44
		[DataMember]
		public bool AvoidWeekends { get; set; }

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x00014D4D File Offset: 0x00012F4D
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x00014D55 File Offset: 0x00012F55
		[DataMember]
		public IList<DayOfWeek> DaysOfWeek { get; set; }
	}
}

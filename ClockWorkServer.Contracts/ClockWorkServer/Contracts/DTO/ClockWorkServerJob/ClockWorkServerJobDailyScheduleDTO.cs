using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200087E RID: 2174
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobDailyScheduleDTO : ClockWorkServerJobScheduleDTO
	{
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x00014D5E File Offset: 0x00012F5E
		// (set) Token: 0x06002C05 RID: 11269 RVA: 0x00014D66 File Offset: 0x00012F66
		[DataMember]
		public bool AvoidWeekends { get; set; }
	}
}

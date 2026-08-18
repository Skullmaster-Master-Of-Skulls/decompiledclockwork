using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005EB RID: 1515
	[DataContract(Namespace = "http://tpro.ca")]
	public class IntakeEntryQueueItemDTO : IntakeEntryDTO
	{
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0000E08F File Offset: 0x0000C28F
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0000E097 File Offset: 0x0000C297
		[DataMember]
		public int SelectedDepartmentValue { get; set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		[DataMember]
		public string SelectedDepartmentTitle { get; set; }
	}
}

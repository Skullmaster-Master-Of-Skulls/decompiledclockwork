using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment
{
	// Token: 0x020004E6 RID: 1254
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentModifiedHistoryItemDTO
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0000C42D File Offset: 0x0000A62D
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0000C435 File Offset: 0x0000A635
		[DataMember]
		public string Action { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0000C43E File Offset: 0x0000A63E
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x0000C446 File Offset: 0x0000A646
		[DataMember]
		public DateTime ActionDate { get; set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0000C44F File Offset: 0x0000A64F
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x0000C457 File Offset: 0x0000A657
		[DataMember]
		public PersonBaseDTO ActionBy { get; set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0000C460 File Offset: 0x0000A660
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x0000C468 File Offset: 0x0000A668
		[DataMember]
		public string ActionDetails { get; set; }
	}
}

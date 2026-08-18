using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000929 RID: 2345
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentForNotificationDTO
	{
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x00016B5E File Offset: 0x00014D5E
		// (set) Token: 0x06002F8E RID: 12174 RVA: 0x00016B66 File Offset: 0x00014D66
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x00016B6F File Offset: 0x00014D6F
		// (set) Token: 0x06002F90 RID: 12176 RVA: 0x00016B77 File Offset: 0x00014D77
		[DataMember]
		public int[] AttendeePersonIds { get; set; }

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x00016B80 File Offset: 0x00014D80
		// (set) Token: 0x06002F92 RID: 12178 RVA: 0x00016B88 File Offset: 0x00014D88
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x00016B91 File Offset: 0x00014D91
		// (set) Token: 0x06002F94 RID: 12180 RVA: 0x00016B99 File Offset: 0x00014D99
		[DataMember]
		public DateTime EndDateTime { get; set; }
	}
}

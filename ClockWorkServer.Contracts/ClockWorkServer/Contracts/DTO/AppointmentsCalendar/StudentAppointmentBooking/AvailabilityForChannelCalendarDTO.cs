using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking
{
	// Token: 0x02000AFD RID: 2813
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityForChannelCalendarDTO
	{
		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x0001CE9C File Offset: 0x0001B09C
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x0001CEAD File Offset: 0x0001B0AD
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x0001CEB5 File Offset: 0x0001B0B5
		[DataMember]
		public int AvailabilityGroupId { get; set; }

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x0001CEBE File Offset: 0x0001B0BE
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x0001CEC6 File Offset: 0x0001B0C6
		[DataMember]
		public string AvailabilityTitle { get; set; }

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x0001CECF File Offset: 0x0001B0CF
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x0001CED7 File Offset: 0x0001B0D7
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		[DataMember]
		public DateTime EndDateTime { get; set; }
	}
}

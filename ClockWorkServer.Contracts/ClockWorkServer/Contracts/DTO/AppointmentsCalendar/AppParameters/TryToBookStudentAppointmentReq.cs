using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000AFF RID: 2815
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookStudentAppointmentReq : BaseMessageReq
	{
		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x0001CF13 File Offset: 0x0001B113
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x0001CF1B File Offset: 0x0001B11B
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x0001CF24 File Offset: 0x0001B124
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x0001CF2C File Offset: 0x0001B12C
		[DataMember]
		public string ChannelId { get; set; }

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x0001CF35 File Offset: 0x0001B135
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x0001CF3D File Offset: 0x0001B13D
		[DataMember]
		public int AvailabilityGroupId { get; set; }

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x0001CF46 File Offset: 0x0001B146
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x0001CF4E File Offset: 0x0001B14E
		[DataMember]
		public string CalendarTitle { get; set; }

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x0001CF57 File Offset: 0x0001B157
		// (set) Token: 0x06003B86 RID: 15238 RVA: 0x0001CF5F File Offset: 0x0001B15F
		[DataMember]
		public DateTime Start { get; set; }

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x0001CF68 File Offset: 0x0001B168
		// (set) Token: 0x06003B88 RID: 15240 RVA: 0x0001CF70 File Offset: 0x0001B170
		[DataMember]
		public DateTime End { get; set; }
	}
}

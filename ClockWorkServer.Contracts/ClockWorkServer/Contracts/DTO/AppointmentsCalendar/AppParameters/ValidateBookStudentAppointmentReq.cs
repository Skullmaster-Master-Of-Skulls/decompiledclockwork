using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B01 RID: 2817
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidateBookStudentAppointmentReq : BaseMessageReq
	{
		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x0001CF8A File Offset: 0x0001B18A
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x0001CF92 File Offset: 0x0001B192
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x0001CF9B File Offset: 0x0001B19B
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x0001CFA3 File Offset: 0x0001B1A3
		[DataMember]
		public DateTime? Date { get; set; }

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x0001CFAC File Offset: 0x0001B1AC
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x0001CFB4 File Offset: 0x0001B1B4
		[DataMember]
		public TimeSpan? StartTime { get; set; }

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x0001CFBD File Offset: 0x0001B1BD
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x0001CFC5 File Offset: 0x0001B1C5
		[DataMember]
		public TimeSpan? EndTime { get; set; }
	}
}

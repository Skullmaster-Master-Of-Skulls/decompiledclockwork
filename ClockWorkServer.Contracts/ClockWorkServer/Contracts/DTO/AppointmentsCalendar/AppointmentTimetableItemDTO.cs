using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar
{
	// Token: 0x02000AFB RID: 2811
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentTimetableItemDTO
	{
		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x0001CDFE File Offset: 0x0001AFFE
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x0001CE06 File Offset: 0x0001B006
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x0001CE0F File Offset: 0x0001B00F
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x0001CE17 File Offset: 0x0001B017
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x0001CE20 File Offset: 0x0001B020
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x0001CE28 File Offset: 0x0001B028
		[DataMember]
		public LookupTimetableItemDTO TimetableItem { get; set; }
	}
}

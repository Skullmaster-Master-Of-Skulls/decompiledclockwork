using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA9 RID: 2729
	[DataContract(Namespace = "http://tpro.ca")]
	public class TimeTableItemDTO
	{
		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x0001C26C File Offset: 0x0001A46C
		// (set) Token: 0x060039F9 RID: 14841 RVA: 0x0001C274 File Offset: 0x0001A474
		[DataMember]
		public DateTime CourseStartDate { get; set; }

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x0001C27D File Offset: 0x0001A47D
		// (set) Token: 0x060039FB RID: 14843 RVA: 0x0001C285 File Offset: 0x0001A485
		[DataMember]
		public DateTime CourseEndDate { get; set; }

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x0001C28E File Offset: 0x0001A48E
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x0001C296 File Offset: 0x0001A496
		[DataMember]
		public int TimetableId { get; set; }

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x0001C29F File Offset: 0x0001A49F
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x0001C2A7 File Offset: 0x0001A4A7
		[DataMember]
		public int StartMinutes { get; set; }

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
		// (set) Token: 0x06003A01 RID: 14849 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		[DataMember]
		public int EndMinutes { get; set; }

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x0001C2C1 File Offset: 0x0001A4C1
		// (set) Token: 0x06003A03 RID: 14851 RVA: 0x0001C2C9 File Offset: 0x0001A4C9
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x0001C2D2 File Offset: 0x0001A4D2
		// (set) Token: 0x06003A05 RID: 14853 RVA: 0x0001C2DA File Offset: 0x0001A4DA
		[DataMember]
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x0001C2E3 File Offset: 0x0001A4E3
		// (set) Token: 0x06003A07 RID: 14855 RVA: 0x0001C2EB File Offset: 0x0001A4EB
		[DataMember]
		public int LuCourseId { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009AB RID: 2475
	[DataContract(Namespace = "http://tpro.ca")]
	public class BasicTestDTO : BaseBasicAppointmentDTO
	{
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x0001863C File Offset: 0x0001683C
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x00018644 File Offset: 0x00016844
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x0001864D File Offset: 0x0001684D
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x00018655 File Offset: 0x00016855
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x0001865E File Offset: 0x0001685E
		// (set) Token: 0x0600323F RID: 12863 RVA: 0x00018666 File Offset: 0x00016866
		[DataMember]
		public DateTime? ClassStartDateTime { get; set; }

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x0001866F File Offset: 0x0001686F
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x00018677 File Offset: 0x00016877
		[DataMember]
		public DateTime? ClassEndDateTime { get; set; }

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x00018680 File Offset: 0x00016880
		// (set) Token: 0x06003243 RID: 12867 RVA: 0x00018688 File Offset: 0x00016888
		[DataMember]
		public eClassTestType ExamType { get; set; }

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x00018691 File Offset: 0x00016891
		// (set) Token: 0x06003245 RID: 12869 RVA: 0x00018699 File Offset: 0x00016899
		[DataMember]
		public LookupCourseBaseDTO CourseBase { get; set; }
	}
}

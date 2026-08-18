using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest
{
	// Token: 0x02000A8C RID: 2700
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestForEdit2DTO : BaseExtendedAppointmentDTO
	{
		// Token: 0x0600389D RID: 14493 RVA: 0x0001B776 File Offset: 0x00019976
		public TestForEdit2DTO()
		{
			this.BookingSpecificInfo = new TestForEditBookingSpecificDTO();
			this.ClassTestDefinitionSpecificInfo = new TestForEditClassDefinitionSpecificDTO();
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x0001B798 File Offset: 0x00019998
		// (set) Token: 0x0600389F RID: 14495 RVA: 0x0001B7A0 File Offset: 0x000199A0
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x0001B7A9 File Offset: 0x000199A9
		// (set) Token: 0x060038A1 RID: 14497 RVA: 0x0001B7B1 File Offset: 0x000199B1
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x0001B7BA File Offset: 0x000199BA
		// (set) Token: 0x060038A3 RID: 14499 RVA: 0x0001B7C2 File Offset: 0x000199C2
		[DataMember]
		public TestForEditBookingSpecificDTO BookingSpecificInfo { get; set; }

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x0001B7CB File Offset: 0x000199CB
		// (set) Token: 0x060038A5 RID: 14501 RVA: 0x0001B7D3 File Offset: 0x000199D3
		[DataMember]
		public TestForEditClassDefinitionSpecificDTO ClassTestDefinitionSpecificInfo { get; set; }

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x0001B7DC File Offset: 0x000199DC
		// (set) Token: 0x060038A7 RID: 14503 RVA: 0x0001B7E4 File Offset: 0x000199E4
		[DataMember]
		public SittingDTO Sitting { get; set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x0001B7ED File Offset: 0x000199ED
		// (set) Token: 0x060038A9 RID: 14505 RVA: 0x0001B7F5 File Offset: 0x000199F5
		[DataMember]
		public IList<ExamFileDTO> ExamFiles { get; set; }

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x0001B7FE File Offset: 0x000199FE
		// (set) Token: 0x060038AB RID: 14507 RVA: 0x0001B806 File Offset: 0x00019A06
		[DataMember]
		public int BreakTimeMinutes { get; set; }
	}
}

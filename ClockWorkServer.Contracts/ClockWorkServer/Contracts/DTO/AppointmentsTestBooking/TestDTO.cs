using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009DD RID: 2525
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestDTO : BaseExtendedAppointmentDTO
	{
		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x00019AB1 File Offset: 0x00017CB1
		// (set) Token: 0x060034B1 RID: 13489 RVA: 0x00019AB9 File Offset: 0x00017CB9
		[DataMember]
		public ClassTestBaseDTO ClassTestInfo { get; set; }

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x00019AC2 File Offset: 0x00017CC2
		// (set) Token: 0x060034B3 RID: 13491 RVA: 0x00019ACA File Offset: 0x00017CCA
		[DataMember]
		public StudentClassTestBaseDTO StudentClassTestInfo { get; set; }

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x00019AD3 File Offset: 0x00017CD3
		// (set) Token: 0x060034B5 RID: 13493 RVA: 0x00019ADB File Offset: 0x00017CDB
		[DataMember]
		public int BreakTimeMinutes { get; set; }
	}
}

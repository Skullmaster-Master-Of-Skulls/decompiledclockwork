using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009DB RID: 2523
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentClassTestBaseDTO
	{
		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x00019A07 File Offset: 0x00017C07
		// (set) Token: 0x0600349F RID: 13471 RVA: 0x00019A0F File Offset: 0x00017C0F
		[DataMember]
		public virtual int AppointmentCourseId { get; set; }

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x00019A18 File Offset: 0x00017C18
		// (set) Token: 0x060034A1 RID: 13473 RVA: 0x00019A20 File Offset: 0x00017C20
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A0E RID: 2574
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq : BaseMessageReq
	{
		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x00019EAD File Offset: 0x000180AD
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x00019EB5 File Offset: 0x000180B5
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x00019EBE File Offset: 0x000180BE
		// (set) Token: 0x0600355C RID: 13660 RVA: 0x00019EC6 File Offset: 0x000180C6
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x0600355D RID: 13661 RVA: 0x00019ECF File Offset: 0x000180CF
		// (set) Token: 0x0600355E RID: 13662 RVA: 0x00019ED7 File Offset: 0x000180D7
		[DataMember]
		public int AlternateContactId { get; set; }
	}
}

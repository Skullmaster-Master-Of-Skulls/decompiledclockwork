using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009DC RID: 2524
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentClassTestDTO : StudentClassTestBaseDTO
	{
		// Token: 0x060034A3 RID: 13475 RVA: 0x00019A29 File Offset: 0x00017C29
		public StudentClassTestDTO()
		{
			this.TestNote = "";
			base.Course = new LookupCourseBaseDTO();
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x060034A4 RID: 13476 RVA: 0x00019A4B File Offset: 0x00017C4B
		// (set) Token: 0x060034A5 RID: 13477 RVA: 0x00019A53 File Offset: 0x00017C53
		[DataMember]
		public DateTime? StudentReportedClassStartDateTime { get; set; }

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x00019A5C File Offset: 0x00017C5C
		// (set) Token: 0x060034A7 RID: 13479 RVA: 0x00019A64 File Offset: 0x00017C64
		[DataMember]
		public DateTime? StudentReportedClassEndDateTime { get; set; }

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x00019A6D File Offset: 0x00017C6D
		// (set) Token: 0x060034A9 RID: 13481 RVA: 0x00019A75 File Offset: 0x00017C75
		[DataMember]
		public string TestNote { get; set; }

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x00019A7E File Offset: 0x00017C7E
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x00019A86 File Offset: 0x00017C86
		[DataMember]
		public string BookingNote { get; set; }

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x00019A8F File Offset: 0x00017C8F
		// (set) Token: 0x060034AD RID: 13485 RVA: 0x00019A97 File Offset: 0x00017C97
		[DataMember]
		public string PrivateNote { get; set; }

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x00019AA0 File Offset: 0x00017CA0
		// (set) Token: 0x060034AF RID: 13487 RVA: 0x00019AA8 File Offset: 0x00017CA8
		[DataMember]
		public string ExtendedProperties { get; set; }
	}
}

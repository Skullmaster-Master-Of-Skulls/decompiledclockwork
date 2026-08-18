using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData
{
	// Token: 0x020004B9 RID: 1209
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeTestBookingDTO
	{
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0000BE88 File Offset: 0x0000A088
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x0000BE90 File Offset: 0x0000A090
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0000BE99 File Offset: 0x0000A099
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		[DataMember]
		public int PersonId { get; set; }
	}
}

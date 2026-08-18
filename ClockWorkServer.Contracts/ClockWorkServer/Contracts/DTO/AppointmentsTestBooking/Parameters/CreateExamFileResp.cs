using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A4E RID: 2638
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateExamFileResp
	{
		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x0001AF74 File Offset: 0x00019174
		// (set) Token: 0x06003770 RID: 14192 RVA: 0x0001AF7C File Offset: 0x0001917C
		[DataMember]
		public int ExamFileId { get; set; }
	}
}

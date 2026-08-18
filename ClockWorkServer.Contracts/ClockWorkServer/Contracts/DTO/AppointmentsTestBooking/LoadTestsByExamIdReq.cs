using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A20 RID: 2592
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsByExamIdReq : BaseMessageReq
	{
		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x0001A045 File Offset: 0x00018245
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x0001A04D File Offset: 0x0001824D
		[DataMember]
		public int ExamId { get; set; }
	}
}

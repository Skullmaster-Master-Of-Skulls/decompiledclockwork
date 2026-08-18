using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A4D RID: 2637
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateExamFileReq : BaseMessageReq
	{
		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x0001AF63 File Offset: 0x00019163
		// (set) Token: 0x0600376D RID: 14189 RVA: 0x0001AF6B File Offset: 0x0001916B
		[DataMember]
		public ExamFileDTO ExamFile { get; set; }
	}
}

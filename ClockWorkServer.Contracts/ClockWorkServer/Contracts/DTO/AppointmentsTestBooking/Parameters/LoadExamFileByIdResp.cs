using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A4C RID: 2636
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFileByIdResp
	{
		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x0001AF52 File Offset: 0x00019152
		// (set) Token: 0x0600376A RID: 14186 RVA: 0x0001AF5A File Offset: 0x0001915A
		[DataMember]
		public ExamFileDTO ExamFile { get; set; }
	}
}

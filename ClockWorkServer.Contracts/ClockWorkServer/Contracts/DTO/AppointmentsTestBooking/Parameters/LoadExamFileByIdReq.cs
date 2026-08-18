using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A4B RID: 2635
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFileByIdReq : BaseMessageReq
	{
		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x0001AF41 File Offset: 0x00019141
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x0001AF49 File Offset: 0x00019149
		[DataMember]
		public int ExamFileId { get; set; }
	}
}

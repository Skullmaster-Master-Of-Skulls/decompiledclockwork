using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A82 RID: 2690
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestExamRowResp
	{
		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x0001B54C File Offset: 0x0001974C
		// (set) Token: 0x06003854 RID: 14420 RVA: 0x0001B554 File Offset: 0x00019754
		[DataMember]
		public TestExamRowDTO TestExamRow { get; set; }
	}
}

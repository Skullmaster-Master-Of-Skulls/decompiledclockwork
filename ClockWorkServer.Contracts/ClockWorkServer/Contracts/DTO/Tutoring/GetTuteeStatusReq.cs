using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200018E RID: 398
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTuteeStatusReq : BaseReportMessageReq
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0000432E File Offset: 0x0000252E
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00004336 File Offset: 0x00002536
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}

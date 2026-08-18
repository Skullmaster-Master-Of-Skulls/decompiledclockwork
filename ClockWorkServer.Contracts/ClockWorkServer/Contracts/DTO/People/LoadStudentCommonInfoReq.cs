using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B9 RID: 953
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentCommonInfoReq : BaseMessageReq
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00009F61 File Offset: 0x00008161
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x00009F69 File Offset: 0x00008169
		[DataMember]
		public int PersonId { get; set; }
	}
}

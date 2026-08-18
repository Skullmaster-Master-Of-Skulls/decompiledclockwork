using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003AD RID: 941
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAssignedAdvisorSignatureDataReq : BaseMessageReq
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00009DC9 File Offset: 0x00007FC9
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x00009DD1 File Offset: 0x00007FD1
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}

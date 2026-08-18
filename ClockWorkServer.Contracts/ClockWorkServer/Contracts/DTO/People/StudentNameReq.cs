using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000375 RID: 885
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentNameReq : BaseMessageReq
	{
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00009951 File Offset: 0x00007B51
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x00009959 File Offset: 0x00007B59
		[DataMember]
		public PersonBaseDTO Person { get; set; }
	}
}

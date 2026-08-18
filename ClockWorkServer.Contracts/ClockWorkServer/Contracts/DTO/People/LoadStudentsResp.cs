using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000387 RID: 903
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsResp
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00009A72 File Offset: 0x00007C72
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x00009A7A File Offset: 0x00007C7A
		[DataMember]
		public List<PersonBaseDTO> People { get; set; }
	}
}

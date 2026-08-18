using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000389 RID: 905
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRoomsResp
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00009A83 File Offset: 0x00007C83
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x00009A8B File Offset: 0x00007C8B
		[DataMember]
		public List<PersonBaseDTO> People { get; set; }
	}
}

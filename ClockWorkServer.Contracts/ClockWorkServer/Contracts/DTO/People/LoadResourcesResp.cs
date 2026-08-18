using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200038B RID: 907
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadResourcesResp
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00009A94 File Offset: 0x00007C94
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x00009A9C File Offset: 0x00007C9C
		[DataMember]
		public List<PersonBaseDTO> People { get; set; }
	}
}

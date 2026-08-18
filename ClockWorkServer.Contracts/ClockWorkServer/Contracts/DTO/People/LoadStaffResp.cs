using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000385 RID: 901
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffResp
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00009A61 File Offset: 0x00007C61
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00009A69 File Offset: 0x00007C69
		[DataMember]
		public List<PersonBaseDTO> People { get; set; }
	}
}

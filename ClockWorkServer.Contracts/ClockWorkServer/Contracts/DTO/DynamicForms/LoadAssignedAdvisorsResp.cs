using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000670 RID: 1648
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAssignedAdvisorsResp
	{
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0000F318 File Offset: 0x0000D518
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x0000F320 File Offset: 0x0000D520
		[DataMember]
		public IList<BasicPersonDTO> AssignedAdvisors { get; set; }
	}
}

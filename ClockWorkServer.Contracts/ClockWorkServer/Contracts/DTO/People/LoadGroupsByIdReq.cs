using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000396 RID: 918
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupsByIdReq : BaseMessageReq
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00009BC7 File Offset: 0x00007DC7
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00009BCF File Offset: 0x00007DCF
		[DataMember]
		public IList<int> GroupIds { get; set; }
	}
}

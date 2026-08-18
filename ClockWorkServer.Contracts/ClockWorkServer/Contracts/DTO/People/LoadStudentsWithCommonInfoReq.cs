using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003BE RID: 958
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsWithCommonInfoReq : BaseMessageReq
	{
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0000A02D File Offset: 0x0000822D
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x0000A035 File Offset: 0x00008235
		[DataMember]
		public IList<int> PersonIds { get; set; }
	}
}

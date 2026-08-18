using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000369 RID: 873
	[DataContract(Namespace = "http://tpro.ca")]
	public class GroupContainerDTO
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x000095C8 File Offset: 0x000077C8
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x000095D0 File Offset: 0x000077D0
		[DataMember]
		public string FullDescription { get; set; }
	}
}

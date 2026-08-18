using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C1A RID: 3098
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPublisherByNameReq : BaseMessageReq
	{
		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		// (set) Token: 0x06004109 RID: 16649 RVA: 0x0001FDD0 File Offset: 0x0001DFD0
		[DataMember]
		public string MediaPublisherName { get; set; }
	}
}

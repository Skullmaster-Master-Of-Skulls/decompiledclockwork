using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C19 RID: 3097
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPublisherByIdResp
	{
		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
		// (set) Token: 0x06004106 RID: 16646 RVA: 0x0001FDBF File Offset: 0x0001DFBF
		[DataMember]
		public MediaPublisherDTO MediaPublisher { get; set; }
	}
}

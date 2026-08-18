using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C1D RID: 3101
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllPublishersResp
	{
		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x0001FDEA File Offset: 0x0001DFEA
		// (set) Token: 0x06004110 RID: 16656 RVA: 0x0001FDF2 File Offset: 0x0001DFF2
		[DataMember]
		public IList<MediaPublisherDTO> MediaPublishers { get; set; }
	}
}

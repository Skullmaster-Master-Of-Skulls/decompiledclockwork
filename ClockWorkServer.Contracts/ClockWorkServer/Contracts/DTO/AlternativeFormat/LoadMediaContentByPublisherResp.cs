using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B73 RID: 2931
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByPublisherResp
	{
		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x0001E730 File Offset: 0x0001C930
		// (set) Token: 0x06003E12 RID: 15890 RVA: 0x0001E738 File Offset: 0x0001C938
		[DataMember]
		public IList<MediaContentDTO> MediaContents { get; set; }
	}
}

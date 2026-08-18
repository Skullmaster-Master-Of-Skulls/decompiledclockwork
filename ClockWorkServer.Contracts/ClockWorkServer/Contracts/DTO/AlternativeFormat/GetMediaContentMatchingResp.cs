using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B69 RID: 2921
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentMatchingResp
	{
		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x0001E686 File Offset: 0x0001C886
		// (set) Token: 0x06003DF4 RID: 15860 RVA: 0x0001E68E File Offset: 0x0001C88E
		[DataMember]
		public IList<MediaContentDTO> MediaContents { get; set; }
	}
}

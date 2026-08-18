using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C38 RID: 3128
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllApprovedMediaRequestResp
	{
		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x00020070 File Offset: 0x0001E270
		// (set) Token: 0x06004177 RID: 16759 RVA: 0x00020078 File Offset: 0x0001E278
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

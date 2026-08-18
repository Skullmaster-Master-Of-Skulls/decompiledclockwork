using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C39 RID: 3129
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllToBeApprovedMediaRequestResp
	{
		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x00020081 File Offset: 0x0001E281
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x00020089 File Offset: 0x0001E289
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

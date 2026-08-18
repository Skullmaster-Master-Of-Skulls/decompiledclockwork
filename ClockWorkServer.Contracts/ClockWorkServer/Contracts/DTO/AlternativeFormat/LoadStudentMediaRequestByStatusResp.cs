using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C37 RID: 3127
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentMediaRequestByStatusResp
	{
		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0002005F File Offset: 0x0001E25F
		// (set) Token: 0x06004174 RID: 16756 RVA: 0x00020067 File Offset: 0x0001E267
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

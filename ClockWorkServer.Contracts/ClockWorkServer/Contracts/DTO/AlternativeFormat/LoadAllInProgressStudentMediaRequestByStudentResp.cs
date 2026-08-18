using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C3F RID: 3135
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllInProgressStudentMediaRequestByStudentResp
	{
		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x00020109 File Offset: 0x0001E309
		// (set) Token: 0x06004190 RID: 16784 RVA: 0x00020111 File Offset: 0x0001E311
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

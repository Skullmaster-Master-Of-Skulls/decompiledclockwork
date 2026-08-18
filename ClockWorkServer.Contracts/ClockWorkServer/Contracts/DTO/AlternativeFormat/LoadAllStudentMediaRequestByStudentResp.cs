using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C42 RID: 3138
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllStudentMediaRequestByStudentResp
	{
		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x0002013C File Offset: 0x0001E33C
		// (set) Token: 0x06004199 RID: 16793 RVA: 0x00020144 File Offset: 0x0001E344
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

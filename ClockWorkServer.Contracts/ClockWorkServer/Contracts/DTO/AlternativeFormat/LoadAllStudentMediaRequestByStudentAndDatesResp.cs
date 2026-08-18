using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C44 RID: 3140
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllStudentMediaRequestByStudentAndDatesResp
	{
		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x00020180 File Offset: 0x0001E380
		// (set) Token: 0x060041A3 RID: 16803 RVA: 0x00020188 File Offset: 0x0001E388
		[DataMember]
		public IList<MediaContentRequestedInfoExtendedDTO> StudentMediaRequests { get; set; }
	}
}

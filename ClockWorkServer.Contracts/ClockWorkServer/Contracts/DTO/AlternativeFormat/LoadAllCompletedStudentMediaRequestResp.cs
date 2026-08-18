using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C40 RID: 3136
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCompletedStudentMediaRequestResp
	{
		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x0002011A File Offset: 0x0001E31A
		// (set) Token: 0x06004193 RID: 16787 RVA: 0x00020122 File Offset: 0x0001E322
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

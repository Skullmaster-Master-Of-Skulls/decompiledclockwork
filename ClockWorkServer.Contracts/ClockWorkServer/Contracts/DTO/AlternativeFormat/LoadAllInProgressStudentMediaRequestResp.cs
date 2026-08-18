using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C3D RID: 3133
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllInProgressStudentMediaRequestResp
	{
		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x000200D6 File Offset: 0x0001E2D6
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x000200DE File Offset: 0x0001E2DE
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> StudentMediaRequests { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C55 RID: 3157
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaRequestInfoPreApprovalResp
	{
		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x00020307 File Offset: 0x0001E507
		// (set) Token: 0x060041E2 RID: 16866 RVA: 0x0002030F File Offset: 0x0001E50F
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}

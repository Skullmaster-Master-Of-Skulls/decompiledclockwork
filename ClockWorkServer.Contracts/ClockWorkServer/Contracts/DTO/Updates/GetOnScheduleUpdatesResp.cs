using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x0200016D RID: 365
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetOnScheduleUpdatesResp
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00003F9A File Offset: 0x0000219A
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00003FA2 File Offset: 0x000021A2
		[DataMember]
		public IList<UpdateFileInfoDTO> UpdatesInfo { get; set; }
	}
}
